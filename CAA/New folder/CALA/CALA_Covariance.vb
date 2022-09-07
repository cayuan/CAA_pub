'? Version : 20210511
'?               20210513 : pre and post DP (data purge) file i/o (csv and json)




Namespace CALA
    Public Module CALA_Covariance_Kernel_General

        '! **The Help between the Kernel Type And Parameter**
        ' **Only stationary kernal .**
        ' **This Is a re-written from the GP_Kernal_function.vb <-- it will be abandon in the future.**
        '?                                 			    ,P1,    P2,     P3,        P4,     P5
        'SE_Squared_Exponential      ,    l,	       ,         ,              ,	
        'SEm_Squared_Exponential_mu,l,	Sigma_n,	,	,	
        'OU_Ornstein_Uhlenbeck           ,l,	    ,	        ,	,	
        'P_Periodic                                 ,l,	,p	,Sigma	,	
        'LP_Locally_Periodic                  ,l,	,p	,Sigma	,	
        'LP_Locally_Periodic                  ,l,	,Alpha	,Sigma	,	


        Public Enum CALA_Kernel_Type
            SE_Squared_Exponential
            SEm_Squared_Exponential_mu
            OU_Ornstein_Uhlenbeck
            P_Periodic
            LP_Locally_Periodic
            RQ_Rational_Quadratic
        End Enum

        Public Structure CALA_Kernel_Parameters
            Dim P1 As Double
            Dim P2 As Double
            Dim P3 As Double
            Dim P4 As Double
            Dim P5 As Double
        End Structure




        Public Function CALA_Kernel_Parameter_and_type_Help() As String
            Dim out As String = ""
            out += "The Help between the Kernel Type and Parameter" + vbNewLine
            out += "Only stationary kernal ." + vbNewLine
            out += "This is a re-written from the GP_Kernal_function.vb <-- it will be abandon in the future. " + vbNewLine

            out += vbTab + vbTab + vbTab + ",P1,P2,P3,P4,P5"
            out += vbNewLine
            'SE_Squared_Exponential
            out += CALA_Kernel_Type.SE_Squared_Exponential.ToString +
                       "," + vbTab + "l," + vbTab + "," + vbTab + "," + vbTab + "," + vbTab
            out += vbNewLine
            'SEm_Squared_Exponential_mu
            out += CALA_Kernel_Type.SEm_Squared_Exponential_mu.ToString +
                        "," + vbTab + "l," + vbTab + "Sigma_n," + vbTab + "," + vbTab + "," + vbTab
            out += vbNewLine
            'OU_Ornstein_Uhlenbeck
            out += CALA_Kernel_Type.OU_Ornstein_Uhlenbeck.ToString +
                        "," + vbTab + "l," + vbTab + "," + vbTab + "," + vbTab + "," + vbTab
            out += vbNewLine
            'P_Periodic
            out += CALA_Kernel_Type.P_Periodic.ToString +
                    "," + vbTab + "l," + vbTab + ",p" + vbTab + ",Sigma" + vbTab + "," + vbTab
            out += vbNewLine
            'LP_Locally_Periodic
            out += CALA_Kernel_Type.LP_Locally_Periodic.ToString +
                    "," + vbTab + "l," + vbTab + ",p" + vbTab + ",Sigma" + vbTab + "," + vbTab
            out += vbNewLine
            'RQ_Rational_Quadratic
            out += CALA_Kernel_Type.LP_Locally_Periodic.ToString +
                    "," + vbTab + "l," + vbTab + ",Alpha" + vbTab + ",Sigma" + vbTab + "," + vbTab
            out += vbNewLine
            Return out
        End Function

        Public Structure CALA_Kernel_Inp
            Dim x() As Double 'This is **ONLY** for 1 vector
            Dim xp() As Double  'This is **ONLY** for 1 vector
            Dim Kernel_Para As CALA_Kernel_Parameters
            Dim Kernel_Type As CALA_Kernel_Type
        End Structure

        Public Const CALA_Covariance_Kernel_epsilon = 0.00001
        Public Const CALA_Covariance_P1_Optimazation_delta = 0.001


    End Module

    Public Class CALA_Covariance_output_format
        Public Name As String
        Public Kernel_Type As CALA_Kernel_Type
        Public Kernel_Para As CALA_Kernel_Parameters
        Public Matrix As CALA_Matrix_output_format
    End Class
    Public Module CALA_Kernel_Functions
        Public Function CALA_Kernel(KerInp As CALA_Kernel_Inp) As Double

            If KerInp.Kernel_Para.P1 = 0 Then Return Nothing

            Dim dist As Double = CALA_Dist_X_Xp(KerInp.x, KerInp.xp)
            Dim out As Double = 0

            Select Case KerInp.Kernel_Type
                Case CALA_Kernel_Type.SE_Squared_Exponential
                    out = Math.Exp(-0.5 * dist ^ 2 / KerInp.Kernel_Para.P1 ^ 2)

                Case CALA_Kernel_Type.SEm_Squared_Exponential_mu
                    out = KerInp.Kernel_Para.P2 ^ 2 * Math.Exp(-0.5 * dist ^ 2 / KerInp.Kernel_Para.P1 ^ 2)

                Case CALA_Kernel_Type.OU_Ornstein_Uhlenbeck
                    out = Math.Exp(-dist / KerInp.Kernel_Para.P1)

                Case CALA_Kernel_Type.P_Periodic
                    Dim tt As Double = 2 * (Math.Sin(Math.PI * dist / KerInp.Kernel_Para.P2)) ^ 2
                    out = KerInp.Kernel_Para.P3 ^ 2 * Math.Exp(-1 * tt / KerInp.Kernel_Para.P2 ^ 2)

                Case CALA_Kernel_Type.LP_Locally_Periodic
                    Dim tt As Double = 2 * (Math.Sin(Math.PI * dist / KerInp.Kernel_Para.P2)) ^ 2
                    tt = KerInp.Kernel_Para.P3 ^ 2 * Math.Exp(-1 * tt / KerInp.Kernel_Para.P2 ^ 2)
                    out = Math.Exp(-0.5 * dist ^ 2 / KerInp.Kernel_Para.P1 ^ 2) * tt

                Case CALA_Kernel_Type.RQ_Rational_Quadratic
                    Dim tt As Double = 1 + dist ^ 2 / (2 * KerInp.Kernel_Para.P2 * KerInp.Kernel_Para.P1 ^ 2)
                    out = KerInp.Kernel_Para.P3 ^ 2 * (tt) ^ (-KerInp.Kernel_Para.P2)

            End Select

            Return out
        End Function

        Public Function CALA_Dist_X_Xp(x() As Double, xp() As Double) As Double
            Dim out As Double = 0

            For ii As Integer = 0 To x.Count - 1
                out += (x(ii) - xp(ii)) ^ 2
            Next
            out = out / x.Count

            If out >= 0 Then
                Return Math.Sqrt(out)
            Else
                Return Nothing
            End If

        End Function





    End Module

    Module CALA_Covariance_Functions

        Public Structure CALA_Covariance_pre_DataPurge_input
            Dim matXX As CALA_Matrix_FullDense
            Dim matYY As CALA_Matrix_FullDense
        End Structure
        Public Structure CALA_Covariance_Data_Purge_Output
            Dim matX As CALA_Matrix_FullDense
            Dim matY As CALA_Matrix_FullDense
            Dim matS As CALA_Matrix_FullDense
            Dim vecN() As Integer
        End Structure

        '===R
        '===R
        '! **Below CALA_Covariance_Data_Purge **
        '===G
        '===G
        Public Function CALA_Covariance_Read_Trsio_file_return_preDP(strFilePath As String) _
            As CALA_Covariance_pre_DataPurge_input
            Dim preDP As CALA_Covariance_pre_DataPurge_input
            '? **Define Varibles**

            Dim x_para_count As Integer = 0
            Dim y_para_count As Integer = 0

            Dim x_exp_count As Long = 0  ' **This should be equal to y_exp_count and s_exp_count**


            '? **1.Locate the file and open**
            '?     check the the number of x and y parameter m and mp (see the devt ppt)
            If Not (IO.File.Exists(strFilePath)) Then Return Nothing

            Dim fr As New IO.StreamReader(strFilePath)
            Dim sr As String = ""
            Dim sp() As String


            '       Read Line 1 for the x count
            sr = fr.ReadLine
            x_para_count = _count_how_many_numbers_in_string(sr)

            '       Read Line 2 for the y count
            sr = fr.ReadLine
            y_para_count = _count_how_many_numbers_in_string(sr)

            '       Rewind
            fr.DiscardBufferedData()
            fr.BaseStream.Seek(0, IO.SeekOrigin.Begin)

            Dim lstX As New List(Of Double())
            Dim lstY As New List(Of Double())

            Do Until (sr Is Nothing)
                sr = fr.ReadLine
                If sr IsNot Nothing Then
                    lstX.Add(_convert_string_2_double_array(sr))
                End If
                sr = fr.ReadLine
                If sr IsNot Nothing Then
                    lstY.Add(_convert_string_2_double_array(sr))
                End If
            Loop

            fr.Close()
            fr.Dispose()



            '? put into 
            preDP.matXX =
                New CALA.CALA_Matrix_FullDense("matXX", lstX.Count,
                                                                        x_para_count, IO.Path.GetDirectoryName(strFilePath))
            preDP.matYY =
                New CALA.CALA_Matrix_FullDense("matYY", lstY.Count,
                                                                        y_para_count, IO.Path.GetDirectoryName(strFilePath))

            For ii As Integer = 0 To lstX.Count - 1
                For jj As Integer = 0 To x_para_count - 1
                    preDP.matXX.a(ii, jj) = lstX(ii)(jj)
                Next

                For jj As Integer = 0 To y_para_count - 1
                    preDP.matYY.a(ii, jj) = lstY(ii)(jj)
                Next

            Next

            Return preDP
        End Function


        Public Function CALA_Covariance_Data_Purge_Matrix(preDP As CALA_Covariance_pre_DataPurge_input) _
                                            As CALA_Covariance_Data_Purge_Output
            Return CALA_Covariance_Data_Purge_Matrix(preDP.matXX, preDP.matYY)
        End Function
        Public Function CALA_Covariance_Data_Purge_Matrix(MatXX As CALA_Matrix_FullDense, MatYY As CALA_Matrix_FullDense) _
                                            As CALA_Covariance_Data_Purge_Output
            '? it is a similar copy from CALA_Covariance_Data_Purge

            '? **Define Varibles**

            Dim x_para_count As Integer = MatXX.ColCount
            Dim y_para_count As Integer = MatYY.ColCount

            Dim x_exp_count As Long = MatXX.RowCount   ' **This should be equal to y_exp_count and s_exp_count**

            Dim _zeroSigmaVector() As Double : ReDim _zeroSigmaVector(MatYY.ColCount - 1)
            For ii As Integer = 0 To _zeroSigmaVector.Count - 1 : _zeroSigmaVector(ii) = 0 : Next


            '?2. rewind to the begining, setup 3 lst format x, y, s (s for sigma) and n (for count) 

            Dim lstX As New List(Of Double())
            Dim lstY As New List(Of Double())      'save the avg of y
            Dim lstY_1 As New List(Of Double())  'save the sum of y
            Dim lstY_2 As New List(Of Double())  'save the sum of y^2
            Dim lstS As New List(Of Double())
            Dim lstN As New List(Of Integer)

            '?    put the first data into lst data set
            lstX.Add(MatXX.row_vec(0))
            lstY.Add(MatYY.row_vec(0))
            lstY_1.Add(MatYY.row_vec(0))
            lstY_2.Add(_convert_array_2_array_power2(MatYY.row_vec(0)))
            lstS.Add(_zeroSigmaVector)
            lstN.Add(1)


            Dim do_until_condition As Boolean = False
            Dim cc As Long = 1

            Do Until (do_until_condition)

                'SR = fr.ReadLine : sr2 = fr.ReadLine
                'If ((SR IsNot Nothing) And (sr2 IsNot Nothing)) Then
                '    If _count_how_many_numbers_in_string(SR) > 0 And
                '            _count_how_many_numbers_in_string(sr2) > 0 Then

                '    End If
                'End If


                Dim x() As Double = MatXX.row_vec(cc)
                Dim y() As Double = MatYY.row_vec(cc)



                '?    use the dist function to compute the x and xp
                Dim bolRepeated As Boolean = False
                Dim indexRepeated As Integer = -1
                For ii As Integer = 0 To lstX.Count - 1
                    Dim dist As Double = CALA_Dist_X_Xp(x, lstX(ii))
                    If dist <= CALA_constants.small_number Then
                        bolRepeated = True
                        indexRepeated = ii
                        GoTo tag_exit_dist_computation
                    End If
                Next

tag_exit_dist_computation:

                If bolRepeated Then
                    '! this is a repeated output, purge!
                    Dim inp As New _computeAvgSTD_structure
                    With inp
                        .y = y
                        .lstY = lstY(indexRepeated)
                        .lstY_1 = lstY_1(indexRepeated)
                        .lstY_2 = lstY_2(indexRepeated)
                        .s = lstS(indexRepeated)
                        .n = lstN(indexRepeated)
                    End With
                    Dim outAS As _computeAvgSTD_structure =
                        _computeAvgSTD_for_repeated_X(inp)

                    lstY(indexRepeated) = outAS.y
                    lstY_1(indexRepeated) = outAS.lstY_1
                    lstY_2(indexRepeated) = outAS.lstY_2
                    lstS(indexRepeated) = outAS.s
                    lstN(indexRepeated) += 1

                Else
                    '! this is a clean, add directly
                    lstX.Add(x)
                    lstY.Add(y)
                    lstY_1.Add(y)
                    lstY_2.Add(_convert_array_2_array_power2(y))
                    lstS.Add(_zeroSigmaVector)
                    lstN.Add(1)
                End If


                cc += 1
                If cc = MatXX.RowCount Then
                    do_until_condition = True
                End If
            Loop



            '?3. output
            Dim DPout As CALA_Covariance_Data_Purge_Output

            'X 

            DPout.matX = New CALA_Matrix_FullDense(lstX.Count, x_para_count, MatXX.BaseDir)
            With DPout.matX
                .Name = "matX"
                For ii As Integer = 0 To lstX.Count - 1
                    Dim xx() As Double = lstX(ii)
                    For jj As Integer = 0 To x_para_count - 1
                        .a(ii, jj) = xx(jj)
                    Next
                Next
            End With

            'Y
            DPout.matY = New CALA_Matrix_FullDense(lstY.Count, y_para_count, MatXX.BaseDir)
            With DPout.matY
                .Name = "matY"
                For ii As Integer = 0 To lstY.Count - 1
                    Dim yy() As Double = lstY(ii)
                    For jj As Integer = 0 To y_para_count - 1
                        .a(ii, jj) = yy(jj)
                    Next
                Next
            End With

            'S
            DPout.matS = New CALA_Matrix_FullDense(lstS.Count, y_para_count, MatXX.BaseDir)
            With DPout.matS
                .Name = "matS"
                For ii As Integer = 0 To lstS.Count - 1
                    Dim ss() As Double = lstS(ii)
                    For jj As Integer = 0 To y_para_count - 1
                        .a(ii, jj) = ss(jj)
                    Next
                Next
            End With
            'N
            With DPout
                ReDim .vecN(lstX.Count - 1)
                For ii As Integer = 0 To lstN.Count - 1
                    .vecN(ii) = lstN(ii)
                Next
            End With

            Return DPout

        End Function
        Public Function CALA_Covariance_Data_Purge(full_Path As String) As CALA_Covariance_Data_Purge_Output
            'This read trsio_ini.csv and compute x_i^j and y_bar and Sigma
            '? **Define Varibles**

            Dim x_para_count As Integer = 0
            Dim y_para_count As Integer = 0

            Dim x_exp_count As Long = 0  ' **This should be equal to y_exp_count and s_exp_count**





            '? **1.Locate the file and open**
            '?     check the the number of x and y parameter m and mp (see the devt ppt)
            If Not (IO.File.Exists(full_Path)) Then Return Nothing

            Dim fr As New IO.StreamReader(full_Path)
            Dim sr As String = ""
            Dim sp() As String


            '       Read Line 1 for the x count
            sr = fr.ReadLine
            x_para_count = _count_how_many_numbers_in_string(sr)

            '       Read Line 2 for the y count
            sr = fr.ReadLine
            y_para_count = _count_how_many_numbers_in_string(sr)

            '       Rewind
            fr.DiscardBufferedData()

            fr.BaseStream.Seek(0, IO.SeekOrigin.Begin)

            '?2. rewind to the begining, setup 3 lst format x, y, s (s for sigma) and n (for count) 

            Dim lstX As New List(Of Double())
            Dim lstY As New List(Of Double())      'save the avg of y
            Dim lstY_1 As New List(Of Double())  'save the sum of y
            Dim lstY_2 As New List(Of Double())  'save the sum of y^2
            Dim lstS As New List(Of Double())
            Dim lstN As New List(Of Integer)

            '?    put the first data into lst data set
            sr = fr.ReadLine : lstX.Add(_convert_string_2_double_array(sr))
            sr = fr.ReadLine : lstY.Add(_convert_string_2_double_array(sr))
            lstY_1.Add(_convert_string_2_double_array(sr))
            lstY_2.Add(_convert_string_2_double_array_power2(sr))
            lstS.Add(_convert_string_2_double_zero_array(sr))
            lstN.Add(1)

            sr = ""
            Dim sr2 As String = ""

            Do Until ((sr Is Nothing) Or (sr2 Is Nothing))

                sr = fr.ReadLine : sr2 = fr.ReadLine
                If ((sr IsNot Nothing) And (sr2 IsNot Nothing)) Then
                    If _count_how_many_numbers_in_string(sr) > 0 And
                            _count_how_many_numbers_in_string(sr2) > 0 Then
                        Dim x() As Double = _convert_string_2_double_array(sr)
                        Dim y() As Double = _convert_string_2_double_array(sr2)



                        '?    use the dist function to compute the x and xp
                        Dim bolRepeated As Boolean = False
                        Dim indexRepeated As Integer = -1
                        For ii As Integer = 0 To lstX.Count - 1
                            Dim dist As Double = CALA_Dist_X_Xp(x, lstX(ii))
                            If dist <= CALA_constants.small_number Then
                                bolRepeated = True
                                indexRepeated = ii
                                GoTo tag_exit_dist_computation
                            End If
                        Next

tag_exit_dist_computation:

                        If bolRepeated Then
                            '! this is a repeated output, purge!
                            Dim inp As New _computeAvgSTD_structure
                            With inp
                                .y = y
                                .lstY = lstY(indexRepeated)
                                .lstY_1 = lstY_1(indexRepeated)
                                .lstY_2 = lstY_2(indexRepeated)
                                .s = lstS(indexRepeated)
                                .n = lstN(indexRepeated)
                            End With
                            Dim outAS As _computeAvgSTD_structure =
                                _computeAvgSTD_for_repeated_X(inp)

                            lstY(indexRepeated) = outAS.y
                            lstY_1(indexRepeated) = outAS.lstY_1
                            lstY_2(indexRepeated) = outAS.lstY_2
                            lstS(indexRepeated) = outAS.s
                            lstN(indexRepeated) += 1

                        Else
                            '! this is a clean, add directly
                            lstX.Add(x)
                            lstY.Add(y)
                            lstY_1.Add(y)
                            lstY_2.Add(_convert_string_2_double_array_power2(sr2))
                            lstS.Add(_convert_string_2_double_zero_array(sr2))
                            lstN.Add(1)
                        End If
                    End If
                End If
            Loop

            fr.Close()
            fr.Dispose()


            '?3. output
            Dim DPout As CALA_Covariance_Data_Purge_Output

            'X 
            Dim path As String = IO.Path.GetDirectoryName(full_Path)
            DPout.matX = New CALA_Matrix_FullDense(lstX.Count, x_para_count, path)
            With DPout.matX
                .Name = "matX"
                For ii As Integer = 0 To lstX.Count - 1
                    Dim xx() As Double = lstX(ii)
                    For jj As Integer = 0 To x_para_count - 1
                        .a(ii, jj) = xx(jj)
                    Next
                Next
            End With

            'Y
            DPout.matY = New CALA_Matrix_FullDense(lstY.Count, y_para_count, path)
            With DPout.matY
                .Name = "matY"
                For ii As Integer = 0 To lstY.Count - 1
                    Dim yy() As Double = lstY(ii)
                    For jj As Integer = 0 To y_para_count - 1
                        .a(ii, jj) = yy(jj)
                    Next
                Next
            End With

            'S
            DPout.matS = New CALA_Matrix_FullDense(lstS.Count, y_para_count, path)
            With DPout.matS
                .Name = "matS"
                For ii As Integer = 0 To lstS.Count - 1
                    Dim ss() As Double = lstS(ii)
                    For jj As Integer = 0 To y_para_count - 1
                        .a(ii, jj) = ss(jj)
                    Next
                Next
            End With
            'N
            With DPout
                ReDim .vecN(lstX.Count - 1)
                For ii As Integer = 0 To lstN.Count - 1
                    .vecN(ii) = lstN(ii)
                Next
            End With

            Return DPout

        End Function


        '===G
        '===G
        '!  **Above the DataPurge
        '===R
        '===R

        Public Function _count_how_many_numbers_in_string(str As String) As Long
            Dim out As Long

            Dim sp() As String = Split(str, ",")

            Dim cc As Integer = 0 ' a counter

            For ii As Integer = 0 To sp.Count - 1
                If IsNumeric(sp(ii)) Then
                    Try
                        Dim tt As Double = CDbl(sp(ii))
                        cc += 1
                    Catch ex As Exception
                    End Try
                End If
            Next

            Return cc
        End Function

        Public Function _convert_string_2_double_array(str As String) As Double()
            Dim x() As Double : ReDim x(_count_how_many_numbers_in_string(str) - 1)
            Dim sp() As String = Split(str, ",")
            For ii As Integer = 0 To x.Count - 1
                x(ii) = CDbl(sp(ii))
            Next
            Return x
        End Function
        Private Function _convert_string_2_double_array_power2(str As String) As Double()
            Dim x() As Double : ReDim x(_count_how_many_numbers_in_string(str) - 1)
            Dim sp() As String = Split(str, ",")
            For ii As Integer = 0 To x.Count - 1
                x(ii) = CDbl(sp(ii)) ^ 2
            Next
            Return x
        End Function
        Private Function _convert_string_2_double_zero_array(str As String) As Double()
            Dim x() As Double : ReDim x(_count_how_many_numbers_in_string(str) - 1)
            Dim sp() As String = Split(str, ",")
            For ii As Integer = 0 To x.Count - 1
                x(ii) = 0
            Next
            Return x
        End Function

        Private Function _convert_array_2_array_power2(x() As Double) As Double()
            Dim o() As Double : ReDim o(x.Count - 1)
            For ii As Integer = 0 To x.Count - 1
                o(ii) = x(ii) ^ 2
            Next
            Return o
        End Function


        Private Structure _computeAvgSTD_structure
            Dim y() As Double
            Dim lstY() As Double
            Dim lstY_1() As Double
            Dim lstY_2() As Double
            Dim s() As Double
            Dim n As Integer
        End Structure

        Private Function _computeAvgSTD_for_repeated_X(inp As _computeAvgSTD_structure) As _computeAvgSTD_structure
            Dim out As New _computeAvgSTD_structure
            ReDim out.y(inp.y.Count - 1)
            ReDim out.lstY_1(inp.y.Count - 1)
            ReDim out.lstY_2(inp.y.Count - 1)
            ReDim out.s(inp.s.Count - 1)

            For ii As Integer = 0 To inp.y.Count - 1
                out.y(ii) = (inp.n * inp.lstY(ii) + inp.y(ii)) / (inp.n + 1)


                'compute lstY_1
                out.lstY_1(ii) += out.y(ii)
                'compute lstY_2
                out.lstY_2(ii) += out.y(ii) ^ 2

                Dim tt As Double = (inp.n + 1) * out.lstY_2(ii) - out.lstY_1(ii) ^ 2
                tt = tt / ((inp.n + 1) * (inp.n))
                out.s(ii) = Math.Sqrt(tt)
            Next
            Return out
        End Function

        '===G
        '===G
        '!  **Above the function of CALA
        '===R
        '===R




        '===R
        '===R
        '! **Below the Kernel optimization**
        '===G
        '===G
        Public Structure Kernel_Optimiation_Output
            Dim KernelType As CALA_Kernel_Type
            Dim KernelPara As CALA_Kernel_Parameters
            Dim Iteration As Long
            Dim Log_Marginal_LikeliHood As Double

        End Structure


        Public Event CALA_Covariance_Kernel_Opt_Event(koo As Kernel_Optimiation_Output)

        Public Sub CALA_Covariance_Kernel_SE_Optimiation_MT(CovK As CALA_Cov,
                                                              matX As CALA_Matrix_FullDense,
                                                              matY As CALA_Matrix_FullDense, y_ind As Integer,
                                                              matS As CALA_Matrix_FullDense,
                                                              Optional LearningRate As Double = 0.5,
                                                              Optional deltaP1 As Double = CALA_Covariance_P1_Optimazation_delta,
                                                              Optional MaxIteration As Integer = 100)

            Dim tt As New Threading.Thread(Sub() CALA_Covariance_Kernel_SE_Optimiation(CovK, matX, matY, y_ind, matS, LearningRate, deltaP1, MaxIteration))
            tt.Priority = Threading.ThreadPriority.AboveNormal
            tt.Start()

        End Sub

        Public Sub CALA_Covariance_Kernel_SE_Optimiation(CovK As CALA_Cov,
                                                              matX As CALA_Matrix_FullDense,
                                                              matY As CALA_Matrix_FullDense, y_ind As Integer,
                                                              matS As CALA_Matrix_FullDense,
                                                              Optional LearningRate As Double = 0.5,
                                                              Optional deltaP1 As Double = CALA_Covariance_P1_Optimazation_delta,
                                                              Optional MaxIteration As Integer = 100)
            '!+ **Can be run after the forming of the CovK**
            Dim koo As Kernel_Optimiation_Output

            koo.KernelType = CovK.KernelType

            Dim kp As CALA_Kernel_Parameters
            kp.P1 = CovK.KernelParameters.P1 : kp.P2 = CovK.KernelParameters.P2
            kp.P3 = CovK.KernelParameters.P3 : kp.P4 = CovK.KernelParameters.P4
            kp.P5 = CovK.KernelParameters.P5


            Dim LML As Double  'Log Marginal Likelihood
            'LML = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)

            'Debug.Print("original LML=" + LML.ToString)



            Dim cc As Integer = 0
            Dim LML1 As Double = 0  'Log Marginal Likelihood
            Dim LML2 As Double = 1 'Log Marginal Likelihood

            Do Until (cc > MaxIteration Or (Math.Abs(LML1 - LML2) < CALA_constants.small_number))

                'positive

                kp.P1 += deltaP1
                CovK.KernelParameters = kp
                CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
                LML1 = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)

                'negative

                kp.P1 -= 2 * deltaP1
                CovK.KernelParameters = kp
                CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
                LML2 = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)

                'new P1
                kp.P1 = kp.P1 + LearningRate * (LML1 - LML2) / (2 * deltaP1)
                'CovK.KernelParameters = kp
                'CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
                'LML = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)
                'Debug.Print("ii:" + cc.ToString + " LML=" + LML.ToString + "  p1=" + kp.P1.ToString)

                cc += 1

            Loop

            koo.KernelPara = kp
            CovK.KernelParameters = kp
            CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
            LML = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)
            koo.Iteration = cc
            koo.Log_Marginal_LikeliHood = LML


            RaiseEvent CALA_Covariance_Kernel_Opt_Event(koo)
        End Sub

        Public Function CALA_Covariance_Kernel_SE_Optimiation_Single(CovK As CALA_Cov,
                                                              matX As CALA_Matrix_FullDense,
                                                              matY As CALA_Matrix_FullDense, y_ind As Integer,
                                                              matS As CALA_Matrix_FullDense,
                                                              Optional LearningRate As Double = 0.5,
                                                              Optional deltaP1 As Double = CALA_Covariance_P1_Optimazation_delta,
                                                              Optional MaxIteration As Integer = 100) As Kernel_Optimiation_Output
            '!+ **Can be run after the forming of the CovK**
            Dim koo As Kernel_Optimiation_Output

            koo.KernelType = CovK.KernelType

            Dim kp As CALA_Kernel_Parameters
            kp.P1 = CovK.KernelParameters.P1
            kp.P2 = CovK.KernelParameters.P2
            kp.P3 = CovK.KernelParameters.P3
            kp.P4 = CovK.KernelParameters.P4
            kp.P5 = CovK.KernelParameters.P5


            Dim LML As Double  'Log Marginal Likelihood
            LML = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)

            'Debug.Print("original LML=" + LML.ToString)



            Dim cc As Integer = 0
            Dim LML1 As Double = 0  'Log Marginal Likelihood
            Dim LML2 As Double = 1 'Log Marginal Likelihood
            Dim LML_avg As Double = 0
            Dim p1_previous As Double = 0

            Dim do_condition As Boolean = False

            Do Until (do_condition)

                'positive

                kp.P1 += deltaP1
                CovK.KernelParameters = kp
                CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
                LML1 = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)

                'negative

                kp.P1 -= 2 * deltaP1
                CovK.KernelParameters = kp
                CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
                LML2 = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)

                'new P1
                'kp.p1 back to normal
                kp.P1 += deltaP1

                'If LML1 < 0 Then
                '    kp.P1 = kp.P1 - LearningRate * (LML1 - LML2) / (2 * deltaP1)
                'Else
                '    kp.P1 = kp.P1 + LearningRate * (LML1 - LML2) / (2 * deltaP1)
                'End If

                Dim delta = LearningRate * (LML2 - LML1) / (2 * deltaP1)

                If Math.Abs(delta) > 1 And Math.Abs(delta) <= 10 Then
                    delta = delta / 10
                End If

                If Math.Abs(delta) > 10 And Math.Abs(delta) <= 100 Then
                    delta = delta / 100
                End If

                If Math.Abs(delta) > 100 And Math.Abs(delta) <= 1000 Then
                    delta = delta / 1000
                End If

                If Math.Abs(delta) > 1000 And Math.Abs(delta) <= 10000 Then
                    delta = delta / 10000
                End If

                If Math.Abs(delta) > 10000 And Math.Abs(delta) <= 100000 Then
                    delta = delta / 100000
                End If

                kp.P1 -= delta



                If Math.Abs(kp.P1) < CALA_constants.small_number Then
                    kp.P1 = CALA_constants.small_number
                End If

                If kp.P1 < 0 Then kp.P1 = -kp.P1


                'CovK.KernelParameters = kp
                'CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
                'LML = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)
                'Debug.Print("ii:" + cc.ToString + " LML=" + LML.ToString + "  p1=" + kp.P1.ToString)





                If ((LML_avg > 0) And ((LML1 + LML2) / 2 < 0)) Then
                    kp.P1 = p1_previous
                End If





                If cc > MaxIteration Or
                    (Math.Abs(LML1 - LML2) < CALA_constants.small_number) Or
                    ((LML_avg > 0) And ((LML1 + LML2) / 2 < 0)) Then

                    do_condition = True

                End If

                LML_avg = (LML1 + LML2) / 2
                p1_previous = kp.P1
                cc += 1



            Loop

            CovK.KernelParameters = kp
            CovK.CovK_Generation_Exp_Kxx(matX, matX, matS, y_ind)
            LML = CovK.Compute_Log_Marginal_likelihood(matY, y_ind)
            koo.Iteration = cc
            koo.Log_Marginal_LikeliHood = LML
            koo.KernelPara = kp


            Return koo
        End Function



        Public Function CALA_Covariance_pre_GP_Xs_forming(
                                                         matXs As CALA_Matrix_FullDense,
                                                         density As Integer) As CALA_Matrix_FullDense

            Dim mat_pre_GP_Xs As New CALA_Matrix_FullDense(matXs.BaseDir)
            mat_pre_GP_Xs.RedefineMatrix((matXs.RowCount - 1) * density, matXs.ColCount)

            For para As Integer = 0 To matXs.ColCount - 1
                For exp As Integer = 1 To matXs.RowCount - 1
                    Dim min As Double = matXs.a(exp - 1, para)
                    Dim max As Double = matXs.a(exp, para)
                    For kk As Integer = 0 To density - 1
                        If exp > 1 Then
                            mat_pre_GP_Xs.a((exp - 1) * density + kk, para) = min + (max - min) / (density) * (kk + 1)
                        Else
                            mat_pre_GP_Xs.a((exp - 1) * density + kk, para) = min + (max - min) / (density - 1) * kk
                        End If

                    Next
                Next
            Next

            Return mat_pre_GP_Xs

        End Function


    End Module


    Public Class CALA_Cov
        '?Varible 
        Dim _name As String
        Dim _mtype As CALA_Matrix_Type  'this can be symm or just a fulldense. **I also use this to define the CovMatrix** But for the being, i only use FullDense


        Dim _KerInp As CALA_Kernel_Inp  'maybe I will remove it, 

        Dim _KerType As CALA_Kernel_Type
        Dim _KerPara As CALA_Kernel_Parameters



        'Dim _matX As CALA_Matrix_FullDense     'row is exp, col is parameter, refer to design doc or devt ppt
        'Dim _matXP As CALA_Matrix_FullDense
        Dim _matCovFD As CALA_Matrix_FullDense
        Dim _baseDir As String

        '?Properties
        Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property
        Property BaseDir As String
            Get
                Return _baseDir
            End Get
            Set(value As String)
                _baseDir = value
            End Set
        End Property
        Property MType As CALA_Matrix_Type
            Get
                Return Me._mtype
            End Get
            Set(value As CALA_Matrix_Type)
                Me._mtype = value
            End Set
        End Property
        Property KernelType As CALA_Kernel_Type
            Get
                Return Me._KerType
            End Get
            Set(value As CALA_Kernel_Type)
                Me._KerType = value
            End Set
        End Property
        Property KernelParameters As CALA_Kernel_Parameters
            Get
                Return Me._KerPara
            End Get
            Set(value As CALA_Kernel_Parameters)
                Me._KerPara = value
            End Set
        End Property
        ReadOnly Property K As CALA_Matrix_FullDense
            Get
                Return _matCovFD
            End Get
        End Property

        'New 

        Public Sub New(Optional Name As String = "",
                                        Optional KernelType As CALA_Kernel_Type = CALA_Kernel_Type.SE_Squared_Exponential,
                                        Optional KernelPara As CALA_Kernel_Parameters = Nothing,
                                        Optional baseDir As String = "c:\temp")

            Call _Setup(Name, KernelType, KernelPara, baseDir)

        End Sub

        Private Sub _Setup(Optional Name As String = "",
                                        Optional KernelType As CALA_Kernel_Type = CALA_Kernel_Type.SE_Squared_Exponential,
                                        Optional KernelPara As CALA_Kernel_Parameters = Nothing,
                                        Optional baseDir As String = "c:\temp")
            Me.Name = Name
            Me.KernelType = KernelType
            If KernelPara.P1 = 0 Then
                Dim kp As CALA_Kernel_Parameters
                kp.P1 = 1
                Me.KernelParameters = kp
            Else
                Me.KernelParameters = KernelPara
            End If
            Me.BaseDir = baseDir
            Me._matCovFD = New CALA_Matrix_FullDense(baseDir)
            Me._matCovFD.Name = Me.Name + "_Mat"

        End Sub



        '! **CovK Genenerating function**  (GP way)
        Public Sub CovK_Generation_Exp_Kxx(matX As CALA_Matrix_FullDense, matXP As CALA_Matrix_FullDense,
                                           Optional matS As CALA_Matrix_FullDense = Nothing,
                                           Optional y_ind As Integer = 0,
                                           Optional epsilon As Double = CALA_Covariance_Kernel_epsilon, Optional Sigma_option As Integer = 0)

            Call CovK_Generation_Exp_Kxx(matX, matXP, Me.KernelType, Me.KernelParameters, matS, y_ind, epsilon, Sigma_option)
        End Sub
        Public Sub CovK_Generation_Exp_Kxx(matX As CALA_Matrix_FullDense, matXP As CALA_Matrix_FullDense,
                                                       KernelType As CALA_Kernel_Type,
                                                       KernelPara As CALA_Kernel_Parameters,
                                            Optional matS As CALA_Matrix_FullDense = Nothing,
                                           Optional y_ind As Integer = 0,
                                           Optional epsilon As Double = CALA_Covariance_Kernel_epsilon,
                                           Optional Sigma_option As Integer = 0)

            Call CovK_Generation_Exp(matX, matXP, KernelType, KernelParameters)

            If matS IsNot Nothing Then
                If matS.ColCount - 1 >= y_ind Then
                    Dim yy() As Double = matS.col_vec(y_ind)
                    For ii As Integer = 0 To matX.RowCount - 1
                        If Sigma_option = 0 Then
                            Me._matCovFD.a(ii, ii) += yy(ii) ^ 2
                        ElseIf Sigma_option = 1 Then
                            Me._matCovFD.a(ii, ii) += yy(ii)
                        ElseIf Sigma_option = 2 Then
                            Me._matCovFD.a(ii, ii) += Math.Sqrt(yy(ii))
                        End If

                    Next
                End If
            End If

            For ii As Integer = 0 To matX.RowCount - 1
                Me._matCovFD.a(ii, ii) += epsilon
            Next

        End Sub


        Public Sub CovK_Generation_Exp(matX As CALA_Matrix_FullDense, matXP As CALA_Matrix_FullDense)
            Call CovK_Generation_Exp(matX, matXP, Me.KernelType, Me.KernelParameters)
        End Sub
        Public Sub CovK_Generation_Exp(matX As CALA_Matrix_FullDense, matXP As CALA_Matrix_FullDense,
                                                       KernelType As CALA_Kernel_Type,
                                                       KernelPara As CALA_Kernel_Parameters)
            '! 1. Put the Kernel Info
            Me.KernelType = KernelType
            If KernelPara.P1 <> 0 Then
                Me.KernelParameters = KernelPara
            End If


            '!  2. forming K
            '   2.1 Forming the _matcovFD
            Me._matCovFD.RedefineMatrix(matX.RowCount, matXP.RowCount)

            '   2.2 compute each elememt of _matcovfd
            For ii As Integer = 0 To matX.RowCount - 1
                For jj As Integer = 0 To matXP.RowCount - 1
                    Dim inp As CALA_Kernel_Inp
                    With inp
                        .x = matX.row_vec(ii)
                        .xp = matXP.row_vec(jj)
                        .Kernel_Type = Me.KernelType
                        .Kernel_Para = Me.KernelParameters
                    End With
                    Me._matCovFD.a(ii, jj) = CALA_Kernel(inp)
                Next
            Next
        End Sub


        '! compute logp(y|X)
        Public Function Compute_Log_Marginal_likelihood(matY As CALA_Matrix_FullDense, y_ind As Integer) As Double
            If Me._matCovFD.RowCount = 0 Or
                    Me._matCovFD.ColCount = 0 Then Return -1
            If _matCovFD.RowCount <> _matCovFD.ColCount Then Return -1
            If matY.RowCount <> _matCovFD.RowCount Then Return -1
            If y_ind > matY.a_count Then Return -1


            'compute -0.5 yT K_y-1y 
            Dim cala_para As CALA.CALA_MathNet_GE_wRC_Execuation_Parameters
            With cala_para
                .RowCheck = True
                .smallNumber = CALA_constants.small_number
                .largeNumber = CALA_constants.large_number
            End With

            Dim ge_in As CALA.CALA_MathNet_GE_wRC_Input
            With ge_in
                .MatrixA = _matCovFD.Me2MathNet
                .VectorB = matY.col_vec(y_ind)
                .para = cala_para
            End With

            Dim cala_out As CALA.CALA_MathNet_GE_wRC_Output =
            CALA.CALA_MathNet_GE_wRC_function.GE_EXE(ge_in)

            Dim dblP1 As Double = -0.5 * cala_out.vectorX.DotProduct(ge_in.VectorB)


            'compute sigma log Lii
            Dim cho As MathNet.Numerics.LinearAlgebra.Factorization.Cholesky(Of Double)
            cho = _matCovFD.Me2MathNet.Cholesky
            Dim dblP2 As Double = 0
            For ii As Integer = 0 To _matCovFD.RowCount - 1
                dblP2 -= Math.Log10(cho.Factor.At(ii, ii))
            Next


            ' compute n/2 log 2pi
            Dim dblP3 = -1 * Me._matCovFD.RowCount * Math.Log10(2 * Math.PI)

            Return dblP1 + dblP2 + dblP3

        End Function



        '! **CovK Genenerating function**  (PCA4DR way)
        '! I can not compute log p(y|X) , y does not exists!
        '! only use K+epsilon 
        Public Sub CovK_Generation_Para_PCA4DR(matX As CALA_Matrix_FullDense,
                                               Optional epsilon As Double = CALA_Covariance_Kernel_epsilon)
            Call CovK_Generation_Para_PCA4DR(matX, Me.KernelType, Me.KernelParameters, epsilon)
        End Sub
        Public Sub CovK_Generation_Para_PCA4DR(matX As CALA_Matrix_FullDense,
                                                       KernelType As CALA_Kernel_Type,
                                                       KernelPara As CALA_Kernel_Parameters,
                                               Optional epsilon As Double = CALA_Covariance_Kernel_epsilon)
            '! 1. Put the Kernel Info
            Me.KernelType = KernelType
            If KernelPara.P1 <> 0 Then
                Me.KernelParameters = KernelPara
            End If


            '!  2. forming K
            '   2.1 Forming the _matcovFD
            Me._matCovFD.RedefineMatrix(matX.ColCount, matX.ColCount)

            '   2.2 compute each elememt of _matcovfd
            For ii As Integer = 0 To matX.ColCount - 1
                For jj As Integer = 0 To matX.ColCount - 1
                    Dim inp As CALA_Kernel_Inp
                    With inp
                        .x = matX.col_vec(ii)
                        .xp = matX.col_vec(jj)
                        .Kernel_Type = Me.KernelType
                        .Kernel_Para = Me.KernelParameters
                    End With
                    Me._matCovFD.a(ii, jj) = CALA_Kernel(inp)
                Next
            Next

            '! 3. add epsilon
            For ii As Integer = 0 To matX.ColCount - 1
                Me._matCovFD.a(ii, ii) += epsilon
            Next

        End Sub






        '! **Coverting function**


        'Me2OutputFormat
        Public Function Me2Output() As CALA_Covariance_output_format
            Dim out As New CALA_Covariance_output_format

            With out
                .Name = Me.Name
                .Kernel_Para = Me._KerPara
                .Kernel_Type = Me.KernelType
                .Matrix = Me._matCovFD._Me2CALAMatrixOutput
            End With

            Return out
        End Function

        Public Sub Output2Me(ccof As CALA_Covariance_output_format)
            With ccof
                Me.Name = .Name
                Me.KernelParameters = .Kernel_Para
                Me.KernelType = .Kernel_Type
                Me._matCovFD._CALAMatrixOutput2Me(ccof.Matrix)
            End With
        End Sub

        '? Me2Json
        Public Sub Me2Json(file As String)
            Dim s_path As String =
                    CALA_convert_basedir_file(Me._baseDir, file, CALA_Output_file_Type.json)
            Dim output As CALA_Covariance_output_format = Me2Output()
            CALA.write_obj2json(output, s_path)

        End Sub


        '? Json2Me
        Public Sub Json2Me(file As String)
            Dim s_path As String =
                    CALA_convert_basedir_file(Me._baseDir, file, CALA_Output_file_Type.json)
            Dim coof As New CALA_Covariance_output_format
            coof = read_json2obj(s_path, New CALA_Covariance_output_format)
            Output2Me(coof)

        End Sub



        '? Matrix file file converting 
        'convert from trsio.csv

        '! Kernal Opt function


    End Class



    Public Class CALA_preDP_json_format
        Public matXX As CALA_Matrix_output_format
        Public matYY As CALA_Matrix_output_format
    End Class

    Public Class CALA_postDP_json_format
        Public matX As CALA_Matrix_output_format
        Public matY As CALA_Matrix_output_format
        Public matS As CALA_Matrix_output_format
        Public vecN() As Integer
    End Class





    Module CALA_DP_Covariance_json_RW_functions
        '---R
        '! **The preDP**
        Public Sub CALA_preDP_2_json(preDB As CALA_Covariance_pre_DataPurge_input, dir_fullpath As String)
            Dim goPreDP As New CALA_preDP_json_format
            goPreDP.matXX = preDB.matXX._Me2CALAMatrixOutput
            goPreDP.matYY = preDB.matYY._Me2CALAMatrixOutput

            Dim filepath As String = CALA_convert_basedir_file(dir_fullpath,
                                                               CALA_file_name.pre_data_purge_X_and_Y + "_" + attachDateTimeSurfix(),
                                                               CALA_Output_file_Type.json)

            CALA.write_obj2json(goPreDP, filepath)
        End Sub

        Public Function CALA_preDP_from_json(file_fullpath As String) As CALA_Covariance_pre_DataPurge_input
            If Not (IO.File.Exists(file_fullpath)) Then Return Nothing

            Dim goPreDP As CALA_preDP_json_format =
                read_json2obj(file_fullpath, New CALA_preDP_json_format)

            Dim preDP As New CALA_Covariance_pre_DataPurge_input
            preDP.matXX = New CALA_Matrix_FullDense : preDP.matYY = New CALA_Matrix_FullDense
            preDP.matXX._CALAMatrixOutput2Me(goPreDP.matXX)
            preDP.matYY._CALAMatrixOutput2Me(goPreDP.matYY)

            Return preDP
        End Function

        Public Function CALA_preDP_2_matX_and_matY_json(preDB As CALA_Covariance_pre_DataPurge_input,
                                                   workDir As String) As String
            Dim fil1 As String = CALA_convert_basedir_file(workDir,
                                            CALA_file_name.pre_data_purge_matX + "_" + attachDateTimeSurfix(),
                                            CALA_Output_file_Type.json)

            Dim fil2 As String = CALA_convert_basedir_file(workDir,
                                            CALA_file_name.pre_data_purge_matY + "_" + attachDateTimeSurfix(),
                                            CALA_Output_file_Type.json)


            preDB.matXX.Me2Json(fil1)
            preDB.matYY.Me2Json(fil2)
            Return fil1 + "," + fil2

        End Function

        Public Function CALA_preDP_2_matX_and_matY_csv(preDB As CALA_Covariance_pre_DataPurge_input,
                                                   workDir As String) As String
            Dim fil1 As String = CALA_convert_basedir_file(workDir,
                                            CALA_file_name.pre_data_purge_matX + "_" + attachDateTimeSurfix(),
                                            CALA_Output_file_Type.csv)

            Dim fil2 As String = CALA_convert_basedir_file(workDir,
                                            CALA_file_name.pre_data_purge_matY + "_" + attachDateTimeSurfix(),
                                            CALA_Output_file_Type.csv)


            preDB.matXX.Me2CSV(fil1)
            preDB.matYY.Me2CSV(fil2)
            Return fil1 + "," + fil2

        End Function

        Public Function CALA_preDP_from_MatXX_MatYY_json_file(file() As String) As CALA_Covariance_pre_DataPurge_input
            If (IO.File.Exists(file(0))) And
                    (IO.File.Exists(file(1))) Then

                Dim preDP As CALA_Covariance_pre_DataPurge_input
                preDP.matXX = New CALA_Matrix_FullDense : preDP.matYY = New CALA_Matrix_FullDense
                preDP.matXX.CSV2Me(file(0)) : preDP.matYY.CSV2Me(file(1))
                Return preDP
            Else
                Return Nothing
            End If
        End Function






        Public Function CALA_postDP_2_json(postDP As CALA_Covariance_Data_Purge_Output, dir_fullpath As String) As String
            Dim goPostDP As New CALA_postDP_json_format
            goPostDP.matX = postDP.matX._Me2CALAMatrixOutput
            goPostDP.matY = postDP.matY._Me2CALAMatrixOutput
            goPostDP.matS = postDP.matS._Me2CALAMatrixOutput
            ReDim goPostDP.vecN(postDP.vecN.Count - 1)

            For ii As Integer = 0 To postDP.vecN.Count - 1
                goPostDP.vecN(ii) = postDP.vecN(ii)
            Next

            Dim filepath As String = CALA_convert_basedir_file(dir_fullpath,
                                                               CALA_file_name.post_data_purge_X_and_Y_and_S + "_" + attachDateTimeSurfix(),
                                                               CALA_Output_file_Type.json)

            CALA.write_obj2json(goPostDP, filepath)

            Return filepath
        End Function

        Public Function CALA_postDB_from_json(fullfilePath As String) As CALA_Covariance_Data_Purge_Output
            If Not (IO.File.Exists(fullfilePath)) Then Return Nothing

            Dim gopostDP As CALA_postDP_json_format =
                read_json2obj(fullfilePath, New CALA_postDP_json_format)

            Dim postDP As New CALA_Covariance_Data_Purge_Output
            postDP.matX = New CALA_Matrix_FullDense : postDP.matY = New CALA_Matrix_FullDense : postDP.matS = New CALA_Matrix_FullDense
            ReDim postDP.vecN(gopostDP.vecN.Count - 1)

            postDP.matX._CALAMatrixOutput2Me(gopostDP.matX)
            postDP.matY._CALAMatrixOutput2Me(gopostDP.matY)
            postDP.matS._CALAMatrixOutput2Me(gopostDP.matS)

            For ii As Integer = 0 To postDP.vecN.Count - 1
                postDP.vecN(ii) = postDP.vecN(ii)
            Next

            Return postDP

        End Function

        Public Function CALA_postDP_2_csv(postDP As CALA_Covariance_Data_Purge_Output, workDir As String) As String()

            Dim fil1 As String = CALA_convert_basedir_file(workDir,
                                            CALA_file_name.post_data_purge_matX + "_" + attachDateTimeSurfix(),
                                            CALA_Output_file_Type.csv)


            Dim fil2 As String = CALA_convert_basedir_file(workDir,
                                                        CALA_file_name.post_data_purge_matY + "_" + attachDateTimeSurfix(),
                                                        CALA_Output_file_Type.csv)

            Dim fil3 As String = CALA_convert_basedir_file(workDir,
                                                        CALA_file_name.post_data_purge_matS + "_" + attachDateTimeSurfix(),
                                                        CALA_Output_file_Type.csv)

            postDP.matX.Me2CSV(fil1)
            postDP.matY.Me2CSV(fil2)
            postDP.matS.Me2CSV(fil3)

            Return {fil1, fil2, fil3}
        End Function


        '---R
    End Module

End Namespace


