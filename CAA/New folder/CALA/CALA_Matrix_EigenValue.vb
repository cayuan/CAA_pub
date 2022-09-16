Imports MathNet.Numerics.LinearAlgebra.Double


Namespace CALA

    Module CALA_Matrix_EigenValue_general

    End Module


    Module CALA_Matrix_EigenValue_Lanczos

        '===R
        '===R
        '---B
        '!+  **Lanczos for Symm**  --> Output *TriDiagonal*
        Public Structure Lanczos_symm_output
            Dim matT As CALA_Matrix_TridiagonalSym
            Dim matB As CALA_Matrix_FullDense
        End Structure
        Public Function Lanczos_symm(matA As CALA_Matrix_FullDense, Optional Lanczos2All As Boolean = True) _
            As Lanczos_symm_output
            Dim out As Lanczos_symm_output
            out.matT = New CALA_Matrix_TridiagonalSym("T", matA.RowCount, matA.BaseDir)
            out.matB = New CALA_Matrix_FullDense("B", matA.RowCount, matA.BaseDir)
            Dim b_zero() As Double : ReDim b_zero(matA.RowCount - 1) : b_zero(0) = 1
            out = Lanczos_symm(matA, b_zero, Lanczos2All)

            Return out
        End Function

        Public Function Lanczos_symm(matA As CALA_Matrix_FullDense, b_zero() As Double,
                                      Optional Lanczos2All As Boolean = True) _
            As Lanczos_symm_output


            Dim out As Lanczos_symm_output
            out.matT = New CALA_Matrix_TridiagonalSym("T", matA.RowCount, matA.BaseDir)
            out.matB = New CALA_Matrix_FullDense("B", matA.RowCount, matA.BaseDir)

            Dim alpha0 As Double : Dim beta0 As Double


            Dim A As New DenseMatrix(matA.RowCount) : A = matA.Me2MathNet
            Dim B0 As New DenseVector(1) : B0 = b_zero
            Dim B As New DenseMatrix(matA.RowCount)

            B.SetColumn(0, B0)
            alpha0 = B0.DotProduct(A * B0) / B0.DotProduct(B0)
            out.matT.a(0, 0) = alpha0
            'Debug use:
            'matA.Me2CSV("A")

            Dim B1 As New DenseVector(1) : B1 = A * B0 - alpha0 * B0
            Dim B_temp As New DenseVector(1)
            Dim alpha As Double : Dim beta As Double

            Dim condition As Boolean = False

            Dim cc As Integer = 1
            'Do Until (cc = matA.RowCount Or B1.L2Norm <= CALA_constants.small_number)
            Do Until (condition)

                B.SetColumn(cc, B1)
                alpha = B1.DotProduct(A * B1) / (B1.DotProduct(B1))
                beta = B0.DotProduct(A * B1) / (B0.DotProduct(B0))

                out.matT.a(cc, cc) = alpha : out.matT.a(cc, cc - 1) = beta

                B_temp = A * B1 - alpha * B1 - beta * B0
                B0 = B1
                B1 = B_temp
                cc += 1


                If Lanczos2All Then
                    If cc = matA.RowCount Then
                        condition = True
                    End If
                Else
                    If cc = matA.RowCount Or
                        B1.L2Norm <= CALA_constants.small_number Then
                        condition = True
                    End If
                End If


            Loop

            If cc < matA.RowCount Then
                Dim matTemp As New DenseMatrix(matA.RowCount)
                matTemp = out.matT.Me2MathNet

                out.matT.RedefineMatrix(cc)

                For ii As Integer = 0 To cc - 1
                    out.matT.a(ii, ii) = matTemp.At(ii, ii)
                    If ii > 0 Then
                        out.matT.a(ii, ii - 1) = matTemp.At(ii, ii - 1)
                    End If
                Next

                out.matB.RedefineMatrix(B.RowCount, B.ColumnCount)
                out.matB.MathNet2Me(B)
            Else
                out.matB.MathNet2Me(B)
            End If

            Return out
        End Function
        '---B
        '===R
        '===R



        '===R
        '===R
        '---B
        '!+  QR for Hessenberg (TriDigonal)  --> output Q and R
        Public Structure CALA_QR_Hessenberg_output
            Dim Q As CALA_Matrix_FullDense
            Dim R As CALA_Matrix_FullDense
        End Structure

        Public Structure CALA_QR_Hessenberg_Iteration_output
            Dim QR_H As CALA_QR_Hessenberg_output
            Dim EigenValue() As Double
            Dim Iteration As Long
            Dim L2Norm As Double
        End Structure


        Public Function CALA_QR_Hessenberg(matT As CALA_Matrix_TridiagonalSym,
                                                                                     Optional QR_Cut As Double = 0,
                                           Optional QR_IterationLimit As Integer = 100,
                                           Optional QR_limit As Double = CALA_constants.small_number) As CALA_QR_Hessenberg_Iteration_output


            Dim oout As New CALA_QR_Hessenberg_Iteration_output

            Dim out As CALA_QR_Hessenberg_output
            out.Q = New CALA_Matrix_FullDense("Q_acc", matT.RowCount, matT.BaseDir)
            out.R = New CALA_Matrix_FullDense("R", matT.RowCount, matT.BaseDir)

            Dim R_old As New CALA_Matrix_FullDense("R_old", matT.RowCount, matT.BaseDir)
            Dim Q_EV As New CALA_Matrix_FullDense("Q_EigenVector", matT.RowCount, matT.BaseDir)

            Dim QR_condition As Boolean = False
            Dim diff_norm As Double = 0

            '? move matT to T
            Dim T As New CALA_Matrix_TridiagonalSym("T", matT.RowCount, matT.BaseDir)
            For ii As Integer = 0 To matT.RowCount - 1
                T.a(ii, ii) = matT.a(ii, ii)
                If ii > 0 Then
                    T.a(ii, ii - 1) = matT.a(ii, ii - 1)
                End If
            Next


            '? clean the Q_Ev
            For ii As Integer = 0 To Q_EV.RowCount - 1
                Q_EV.a(ii, ii) = 1
            Next


            Dim cycle As Integer = 0  ' total QR cycle

            Do Until QR_condition


                '? compute QR
                out = CALA_QR_Hessenberg_Single(T, QR_Cut, QR_limit)

                '? saving the trueQ
                Q_EV.MathNet2Me(Q_EV.Me2MathNet * out.Q.Me2MathNet)

                '? forming new T
                T.MathNet2Me(out.R.Me2MathNet * out.Q.Me2MathNet)


                '? if cycle =0 (the first one) use cut
                If cycle = 0 Then
                    For ii As Integer = 0 To T.RowCount - 1
                        T.a(ii, ii) -= QR_Cut
                    Next
                End If


                '?check R_diag_l2norm

                diff_norm = CALA_compare_R_diagonal_change_L2Norm(out.R, R_old)


                '? copy R_old
                For ii As Integer = 0 To out.R.RowCount - 1
                    For jj As Integer = 0 To out.R.ColCount - 1
                        R_old.a(ii, jj) = out.R.a(ii, jj)
                    Next
                Next


                cycle += 1

                If cycle = QR_IterationLimit Or
                        diff_norm < QR_limit Then
                    QR_condition = True
                End If

                'for debug use
                'If cycle = 10 Then
                '    QR_condition = True
                'End If

            Loop

            oout.QR_H.Q = Q_EV
            oout.QR_H.R = out.R
            For ii As Integer = 0 To T.RowCount - 1
                oout.QR_H.R.a(ii, ii) += QR_Cut
            Next

            oout.Iteration = cycle
            oout.L2Norm = diff_norm
            ReDim oout.EigenValue(Q_EV.RowCount - 1)
            For ii As Integer = 0 To out.R.RowCount - 1
                oout.EigenValue(ii) = out.R.a(ii, ii)
            Next



            Return oout
        End Function

        Private Function CALA_compare_R_diagonal_change_L2Norm(R_new As CALA_Matrix_FullDense, R_old As CALA_Matrix_FullDense) As Double

            Dim dbl As Double

            For ii As Integer = 0 To R_new.RowCount - 1
                dbl += (R_new.a(ii, ii) - R_old.a(ii, ii)) ^ 2
            Next

            Return Math.Sqrt(dbl / R_new.RowCount)

        End Function


        Private Function CALA_QR_Hessenberg_Single(matT As CALA_Matrix_TridiagonalSym,
                                                   Optional QR_Cut As Double = 0,
                                           Optional QR_limit As Double = CALA_constants.small_number) As CALA_QR_Hessenberg_output

            Dim out As CALA_QR_Hessenberg_output
            out.Q = New CALA_Matrix_FullDense("Q_acc", matT.RowCount, matT.BaseDir)
            out.R = New CALA_Matrix_FullDense("R", matT.RowCount, matT.BaseDir)
            Dim MIn As New DenseMatrix(matT.RowCount)  ' a unit matrix
            Dim checkRdiagonal As Boolean = False

            'check if Q diag is zero, if so, use cut method
            Dim R23 As New DenseMatrix(2, 3)


            'copy the original T to R
            For ii As Integer = 0 To matT.RowCount - 1
                out.R.a(ii, ii) = matT.a(ii, ii)
                If ii > 0 Then
                    out.R.a(ii, ii - 1) = matT.a(ii, ii - 1)
                    out.R.a(ii - 1, ii) = matT.a(ii, ii - 1)
                End If
            Next


            'make Q as I (single)
            For ii As Integer = 0 To matT.RowCount - 1
                out.Q.a(ii, ii) = 1
                MIn.At(ii, ii, 1)
            Next



            For ii As Integer = 0 To matT.RowCount - 2
                'For ii As Integer = 0 To 1
                R23.Clear()
                For jj As Integer = 0 To 1
                    If ii <> matT.RowCount - 2 Then
                        For kk As Integer = 0 To 2
                            R23.At(jj, kk, out.R.a(jj + ii, kk + ii))
                        Next
                    Else
                        For kk As Integer = 0 To 1
                            R23.At(jj, kk, out.R.a(jj + ii, kk + ii))
                        Next

                        R23.At(jj, 2, 0)

                    End If

                Next
                Dim mid As CALA_QR_internal_Q22_R23 =
                    CALA_QR_Hessenberg_Rotation_matrix(R23)


                '? store to Q and R
                ' store R
                For jj As Integer = 0 To 1
                    If ii <> matT.RowCount - 2 Then
                        For kk As Integer = 0 To 2
                            out.R.a(jj + ii, kk + ii) = mid.R23.At(jj, kk)
                        Next
                    Else
                        For kk As Integer = 0 To 1
                            out.R.a(jj + ii, kk + ii) = mid.R23.At(jj, kk)
                        Next
                    End If

                Next

                'store Q
                Dim M1 As New DenseMatrix(out.Q.RowCount)
                M1 = out.Q.Me2MathNet

                Dim M2 As New DenseMatrix(out.Q.RowCount)
                For jj As Integer = 0 To M2.RowCount - 1        'Use this way, to 100% sure to distangle M2 and Min
                    For kk As Integer = 0 To M2.RowCount - 1
                        M2.At(jj, kk, MIn.At(jj, kk))
                    Next
                Next

                For jj = 0 To 1
                    For kk = 0 To 1
                        M2.At(jj + ii, kk + ii, mid.Q22.At(jj, kk))
                    Next
                Next

                Dim MM As DenseMatrix = M1 * (M2.Transpose)
                out.Q.MathNet2Me(MM)

                '!check the diagonal 
                If checkRdiagonal Then
                    'this is from previous step
                    For jj As Integer = 0 To out.R.RowCount - 1
                        out.R.a(jj, jj) -= 2 * CALA_constants.small_number
                    Next
                    checkRdiagonal = False
                End If


                For jj As Integer = 0 To out.R.RowCount - 1
                    If Math.Abs(out.R.a(jj, jj)) < CALA_constants.small_number Then
                        checkRdiagonal = True
                    End If
                Next

                If checkRdiagonal Then
                    For jj As Integer = 0 To out.R.RowCount - 1
                        out.R.a(jj, jj) += 2 * CALA_constants.small_number
                    Next
                End If


            Next

            'double check out
            'check R
            For ii As Integer = 0 To out.R.RowCount - 1
                For jj As Integer = 0 To out.R.ColCount - 1
                    If Math.Abs(out.R.a(ii, jj)) < CALA_constants.small_number Then
                        out.R.a(ii, jj) = 0
                    End If
                Next
            Next

            'check Q
            For ii As Integer = 0 To out.Q.RowCount - 1
                For jj As Integer = 0 To out.Q.ColCount - 1
                    If Math.Abs(out.Q.a(ii, jj)) < CALA_constants.small_number Then
                        out.Q.a(ii, jj) = 0
                    End If
                Next
            Next

            Return out
        End Function

        Private Structure CALA_QR_internal_Q22_R23
            Dim Q22 As DenseMatrix
            Dim R23 As DenseMatrix
        End Structure

        Private Function CALA_QR_Hessenberg_Rotation_matrix(R23 As DenseMatrix) As CALA_QR_internal_Q22_R23
            Dim out As CALA_QR_internal_Q22_R23
            out.R23 = New DenseMatrix(2, 3)
            out.Q22 = New DenseMatrix(2, 2)


            Dim theta As Double = Math.Atan(R23.At(1, 0) / R23.At(0, 0))
            out.Q22.SetRow(0, {Math.Cos(theta), Math.Sin(theta)})
            out.Q22.SetRow(1, {-Math.Sin(theta), Math.Cos(theta)})

            out.R23 = out.Q22 * R23
            out.R23.At(1, 0, 0)

            Return out
        End Function


        '---B
        '===R
        '===R



        '===R
        '===R
        '---B
        '!+  Automatic Lanczos QR
        Public Function CALA_Lanczos_Hessenberg_QR(matA As CALA_Matrix_FullDense,
                                                   Optional Lanczos2All As Boolean = True,
                                                   Optional QR_Cut As Double = 0,
                                                   Optional QR_IterationLimit As Integer = 100,
                                                   Optional QR_limit As Double = CALA_constants.small_number) _
                                                   As CALA_QR_Hessenberg_Iteration_output

            Dim LanczosTriS As CALA.Lanczos_symm_output =
            CALA.CALA_Matrix_EigenValue_Lanczos.Lanczos_symm(matA, Lanczos2All)

            Dim QR_Hess As CALA.CALA_QR_Hessenberg_Iteration_output =
                CALA.CALA_QR_Hessenberg(LanczosTriS.matT, QR_Cut, QR_IterationLimit, QR_limit)

            Return QR_Hess
        End Function





    End Module
















End Namespace

