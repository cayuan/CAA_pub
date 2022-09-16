Imports MathNet.Numerics.LinearAlgebra.Double

Namespace CALA
    Module CALA_Gaussian_Processes_General

    End Module

    Module CALA_Gaussian_Processes_Function
        Public Structure CALA_GP_output
            Dim matX As CALA_Matrix_FullDense
            Dim CovKss As CALA_Matrix_FullDense
            Dim ys() As Double
            Dim Sigma() As Double
            Dim outYandSigma As CALA_Matrix_FullDense
        End Structure

        Public Structure CALA_GP_input
            Dim extra_ratio As Double '=0.2  it menas x_range_max(0)+extra
            Dim splits As Integer '=30
            Dim range_max As Double '= 0.9
            Dim range_min As Double '=-0.9

            Dim x_range_max() As Double
            Dim x_range_min() As Double
        End Structure

        Private Structure CALA_para_max_min
            Dim max As Double
            Dim min As Double
        End Structure



        Public Function CALA_GP_all_CovK_and_y(
                                              ByRef CovK As CALA_Cov,
                                              ByRef CovKs As CALA_Cov,
                                              ByRef CovKss As CALA.CALA_Cov,
                                              ByVal postDP As CALA_Covariance_Data_Purge_Output,
                                              ByVal mat_pre_GP_Xs As CALA_Matrix_FullDense,
                                              ByVal KType As CALA_Kernel_Type, kp As CALA_Kernel_Parameters, epsilon As Double,
                                              Optional y_ind As Integer = 0) As CALA_GP_output


            Dim workDir As String = CovK.K.BaseDir

            CovK = New CALA_Cov("Kxx", KType, kp, CovK.K.BaseDir)
            CovK.CovK_Generation_Exp_Kxx(postDP.matX, postDP.matX, postDP.matS, y_ind, epsilon)

            CovKs = New CALA_Cov("Kxs", KType, kp, workDir)
            CovKs.CovK_Generation_Exp(postDP.matX, mat_pre_GP_Xs)

            CovKss = New CALA_Cov("Kss", KType, kp, workDir)
            CovKss.CovK_Generation_Exp(mat_pre_GP_Xs, mat_pre_GP_Xs)


            '?compute y*
            Dim ys() As Double : ReDim ys(postDP.matY.RowCount - 1)
            Dim sumY As Double = 0
            For ii As Integer = 0 To ys.Count - 1
                sumY += postDP.matY.a(ii, y_ind)
            Next
            sumY = sumY / ys.Count
            For ii As Integer = 0 To ys.Count - 1
                ys(ii) = postDP.matY.a(ii, y_ind) - sumY
            Next

            '? make computation 
            'y*
            Dim Ky As DenseMatrix = CovK.K.Me2MathNet
            Dim Kxs As DenseMatrix = CovKs.K.Me2MathNet
            Dim Kss As DenseMatrix = CovKss.K.Me2MathNet
            Dim Ky_1 As DenseMatrix = Ky.Inverse

            Dim yss As DenseVector = Kxs.Transpose * (Ky_1 * ys)


            'cov*
            Dim K As DenseMatrix = Kss - Kxs.Transpose * (Ky_1 * Kxs)

            'output
            Dim out As CALA_GP_output
            out.matX = New CALA_Matrix_FullDense("matX",
                                                 mat_pre_GP_Xs.RowCount,
                                                 mat_pre_GP_Xs.ColCount,
                                                 mat_pre_GP_Xs.BaseDir)
            For ii As Integer = 0 To mat_pre_GP_Xs.RowCount - 1
                For jj As Integer = 0 To mat_pre_GP_Xs.ColCount - 1
                    out.matX.a(ii, jj) = mat_pre_GP_Xs.a(ii, jj)
                Next
            Next
            'out.matX = mat_pre_GP_Xs
            out.CovKss = New CALA_Matrix_FullDense(mat_pre_GP_Xs.BaseDir)
            out.CovKss.MathNet2Me(K)

            ReDim out.ys(yss.Count - 1)
            For ii As Integer = 0 To yss.Count - 1
                out.ys(ii) = yss(ii) + sumY
            Next

            ReDim out.Sigma(yss.Count - 1)
            For ii As Integer = 0 To yss.Count - 1
                out.Sigma(ii) = K.At(ii, ii)
            Next

            out.outYandSigma = New CALA_Matrix_FullDense(mat_pre_GP_Xs.BaseDir)
            out.outYandSigma.RedefineMatrix(yss.Count, 2)
            For ii As Integer = 0 To yss.Count - 1
                out.outYandSigma.a(ii, 0) = out.ys(ii)
                out.outYandSigma.a(ii, 1) = out.Sigma(ii)
            Next



            Return out




        End Function


        Public Function CALA_GP(DP As CALA_Covariance_Data_Purge_Output,
                                 KernelType As CALA_Kernel_Type,
                                 kernelPara As CALA_Kernel_Parameters,
                                 ginp As CALA_GP_input,
                                 Optional y_ind As Integer = 0) As CALA_GP_output


            'modify matX to new range
            Dim matX_new As New CALA_Matrix_FullDense("matXNew",
                                                       DP.matX.RowCount, DP.matX.ColCount, DP.matX.BaseDir)

            For para As Integer = 0 To DP.matX.ColCount - 1
                For exp As Integer = 1 To DP.matX.RowCount - 1
                    matX_new.a(exp, para) =
                        (DP.matX.a(exp, para) - ginp.x_range_min(para) - ginp.extra_ratio) * (ginp.range_max - ginp.range_min) / (ginp.x_range_max(para) - ginp.x_range_min(para) + 2 * ginp.extra_ratio) + ginp.range_min
                Next
            Next


            '? forming matXP using ginp

            Dim matXP As New CALA_Matrix_FullDense(ginp.splits, ginp.x_range_max.Count, DP.matX.BaseDir)

            For para As Integer = 0 To ginp.x_range_max.Count - 1
                For exp As Integer = 0 To ginp.splits - 1
                    Dim rang As Double = Math.Abs(ginp.x_range_max(para) - ginp.x_range_min(para) + 2 * ginp.extra_ratio)
                    Dim s_range As Double = rang / (ginp.splits - 1)
                    Dim real As Double = ginp.x_range_min(para) - ginp.extra_ratio + exp * s_range

                    matXP.a(exp, para) = (real - ginp.x_range_min(para) + ginp.extra_ratio) * (ginp.range_max - ginp.range_min) / (ginp.x_range_max(para) - ginp.x_range_min(para) + 2 * ginp.extra_ratio) + ginp.range_min
                Next
            Next



            '? form Kxx, Kxs, Kss

            'find the best Kernel parameters
            Dim kp As CALA.CALA_Kernel_Parameters
            kp.P1 = 1
            kp.P2 = 1
            kp.P3 = 1

            Dim covK As New CALA.CALA_Cov("Kxx", KernelType, kernelPara, DP.matX.BaseDir)
            covK.CovK_Generation_Exp_Kxx(matX_new, matX_new, DP.matS, 0)

            Dim koo As CALA.Kernel_Optimiation_Output =
            CALA.CALA_Covariance_Kernel_SE_Optimiation_Single(covK, matX_new, DP.matY, y_ind, DP.matS, 0.4,, 250)
            covK.KernelParameters = koo.KernelPara
            covK.CovK_Generation_Exp_Kxx(matX_new, matX_new, DP.matS, 0)

            Dim covKxs As New CALA.CALA_Cov("Kxs", koo.KernelType, koo.KernelPara, DP.matX.BaseDir)
            covKxs.CovK_Generation_Exp(matX_new, matXP)


            Dim covKss As New CALA.CALA_Cov("Kss", koo.KernelType, koo.KernelPara, DP.matX.BaseDir)
            covKss.CovK_Generation_Exp(matXP, matXP)


            '?compute y*
            Dim ys() As Double : ReDim ys(DP.matY.RowCount - 1)
            Dim sumY As Double = 0
            For ii As Integer = 0 To ys.Count - 1
                sumY += DP.matY.a(ii, y_ind)
            Next
            sumY = sumY / ys.Count
            For ii As Integer = 0 To ys.Count - 1
                ys(ii) = DP.matY.a(ii, y_ind) - sumY
            Next


            '? make computation 
            'y*
            Dim Ky As DenseMatrix = covK.K.Me2MathNet
            Dim Kxs As DenseMatrix = covKxs.K.Me2MathNet
            Dim Kss As DenseMatrix = covKss.K.Me2MathNet
            Dim Ky_1 As DenseMatrix = Ky.Inverse

            Dim yss As DenseVector = Kxs.Transpose * (Ky_1 * ys)


            'cov*
            Dim K As DenseMatrix = Kss - Kxs.Transpose * (Ky_1 * Kxs)


            'convert back to original x*
            For para As Integer = 0 To ginp.x_range_max.Count - 1
                For exp As Integer = 0 To ginp.splits - 1
                    matXP.a(exp, para) =
                    (matXP.a(exp, para) - ginp.range_min) / (ginp.range_max - ginp.range_min) * (ginp.x_range_max(para) - ginp.x_range_min(para) + 2 * ginp.extra_ratio) + ginp.x_range_min(para) - ginp.extra_ratio
                Next
            Next

            'output
            Dim out As CALA_GP_output
            out.matX = matXP
            out.CovKss = New CALA_Matrix_FullDense(matXP.BaseDir)
            out.CovKss.MathNet2Me(K)

            ReDim out.ys(yss.Count - 1)
            For ii As Integer = 0 To yss.Count - 1
                out.ys(ii) = yss(ii) + sumY
            Next

            ReDim out.Sigma(yss.Count - 1)
            For ii As Integer = 0 To yss.Count - 1
                out.Sigma(ii) = K.At(ii, ii)
            Next

            out.outYandSigma = New CALA_Matrix_FullDense(matXP.BaseDir)
            out.outYandSigma.RedefineMatrix(yss.Count, 2)
            For ii As Integer = 0 To yss.Count - 1
                out.outYandSigma.a(ii, 0) = out.ys(ii)
                out.outYandSigma.a(ii, 1) = out.Sigma(ii)
            Next



            Return out

        End Function

    End Module



End Namespace


