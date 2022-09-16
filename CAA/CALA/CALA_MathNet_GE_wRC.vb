
'?=============================
'CALA _MathNet_GE_wRC.vb
'CALA Mathnet Gaussian Elimination with row check


'Version 1.0.0.0
'Date:20210416

'?=============================
'Module CALA_MathNet_GE_wRC_structure
'Module CALA_MathNet_GE_wRC_function
'Class CALA_MathNet_GE_wRC_object




Imports MathNet.Numerics.LinearAlgebra.Double



Namespace CALA
    Public Module CALA_MathNet_GE_wRC_structure
        Const Version As String = "1.0.0.0"

        Structure CALA_MathNet_GE_wRC_Execuation_Parameters
            Dim smallNumber As Double
            Dim largeNumber As Double
            Dim RowCheck As Boolean
        End Structure

        Structure CALA_MathNet_GE_wRC_Input
            Dim MatrixA As DenseMatrix
            Dim VectorB As DenseVector
            Dim para As CALA_MathNet_GE_wRC_Execuation_Parameters
        End Structure

        Structure CALA_MathNet_GE_wRC_Output
            Dim vectorX As DenseVector
            Dim paraSequence() As Integer
        End Structure


        Structure CALA_MathNet_GE_Int_Int_Vec
            Dim intt As Integer
            Dim vec() As Integer
        End Structure

    End Module

    Public Module CALA_MathNet_GE_wRC_function

        Function RowConvDenseMatrix(vec() As Double, rowCount As Integer) As DenseMatrix
            Dim outMatrix As New DenseMatrix(rowCount)
            If vec.Count <> rowCount ^ 2 Then Return Nothing
            For ii As Integer = 0 To rowCount - 1
                For jj As Integer = 0 To rowCount - 1
                    Dim ind As Integer = ii * (rowCount) + jj
                    outMatrix.At(ii, jj, vec(ind))
                Next
            Next
            Return outMatrix
        End Function

        Function ColumnConvDenseMatrix(vec() As Double, colCount As Integer) As DenseMatrix
            Dim outMatrix As New DenseMatrix(colCount)
            If vec.Count <> colCount ^ 2 Then Return Nothing

            For ii As Integer = 0 To colCount - 1
                For jj As Integer = 0 To colCount - 1
                    Dim ind As Integer = ii * (colCount) + jj
                    outMatrix.At(jj, ii, vec(ind))
                Next
            Next

            Return outMatrix
        End Function

        Function GE_EXE(inp As CALA_MathNet_GE_wRC_Input) _
            As CALA_MathNet_GE_wRC_Output


            '! 1. check input
            If inp.MatrixA.RowCount <> inp.MatrixA.ColumnCount Then Return Nothing
            If inp.MatrixA.RowCount <> inp.VectorB.Count Then Return Nothing
            If inp.MatrixA.RowCount < 1 Then Return Nothing


            '! 2. Define parameters
            Dim GE_A As New DenseMatrix(inp.MatrixA.RowCount)
            Dim GE_B As New DenseVector(inp.MatrixA.RowCount)
            Dim GE_Seq() As Integer : ReDim GE_Seq(inp.MatrixA.RowCount - 1)


            'GE_A = inp.MatrixA : GE_B = inp.VectorB

            For ii As Integer = 0 To inp.MatrixA.RowCount - 1
                For jj As Integer = 0 To inp.MatrixA.RowCount - 1
                    GE_A.At(ii, jj, inp.MatrixA.At(ii, jj))
                Next
            Next


            For ii As Integer = 0 To inp.MatrixA.RowCount - 1
                GE_B(ii) = inp.VectorB(ii)
            Next

            For ii As Integer = 0 To GE_A.RowCount - 1 : GE_Seq(ii) = ii : Next ii

            '? GE
            For iR As Integer = 0 To GE_A.RowCount - 1
                If iR > GE_A.RowCount - 1 Then
                    GoTo GE_GE_loop
                End If
                Dim consDiag As Double = GE_A.At(iR, iR)

                'make first term to 1
                For iC As Integer = 0 To GE_A.RowCount - 1
                    GE_A.At(iR, iC, GE_A.At(iR, iC) / consDiag)
                Next
                GE_B(iR) = GE_B(iR) / consDiag


                'remove the rest
                Dim vecBase As New DenseVector(1)
                vecBase = GE_A.Row(iR)

                For iRR As Integer = iR + 1 To GE_A.RowCount - 1
                    Dim headItem As Double = GE_A.At(iRR, iR)


                    Dim vecRow As New DenseVector(1)
                    vecRow = GE_A.Row(iRR) - vecBase.Multiply(headItem)
                    GE_A.SetRow(iRR, vecRow)

                    GE_B(iRR) = GE_B(iRR) - GE_B(iR) * headItem
                Next

                '? Pivot check
                Dim pivotCheck(GE_A.RowCount - 1) As Boolean
                '?   compute the zero rows
                For iRR As Integer = iR + 1 To GE_A.RowCount - 1
                    Dim row_pivotCheck As New DenseVector(1) : row_pivotCheck = GE_A.Row(iRR)
                    pivotCheck(iRR) = _checkZeroVector(row_pivotCheck, inp.para.smallNumber)
                Next


                '?   if there is a zero rows, enter the pivot and removal process
                If _checkBooleanArray_for_ALL_False(pivotCheck) Then

                    '? generate a pivot strategy : A GE_seq() brothers
                    Dim _pivotPlan As CALA.CALA_MathNet_GE_Int_Int_Vec =
                        _sort_pivotCheck(pivotCheck)

                    '? pivot GE_A, GE_B

                    Dim lstPP As New List(Of Integer)
                    lstPP = _pivotPlan.vec.ToList
                    For Row As Integer = 0 To _pivotPlan.intt - 1
                        If Row <> lstPP(Row) Then
                            Dim changeRow As Integer = lstPP.FindIndex(Function(value As Integer)
                                                                           Return value = Row
                                                                       End Function)
                            'Debug.Print("chageRow=" + changeRow.ToString)

                            Dim rowOld As DenseVector = GE_A.Row(Row)
                            Dim rowNew As DenseVector = GE_A.Row(changeRow)

                            GE_A.SetRow(changeRow, rowOld)
                            GE_A.SetRow(Row, rowNew)

                            Dim valueOld As Double = GE_B(Row)
                            Dim valueNew As Double = GE_B(changeRow)

                            GE_B(Row) = valueNew
                            GE_B(changeRow) = valueOld
                        End If
                    Next

                    '? remove GE_A, GE_B
                    Dim currentMatrixSize As Integer = GE_A.RowCount
                    For ii As Integer = 0 To (currentMatrixSize - _pivotPlan.intt) - 1
                        Dim rc As Integer = GE_A.RowCount
                        GE_A = GE_A.SubMatrix(0, rc - 1, 0, rc - 1)
                        GE_B = GE_B.SubVector(0, rc - 1)
                    Next

                    'modify GE_Seq

                    GE_Seq = _pivotPlan.vec

                End If
            Next


            '? compute back
GE_GE_loop:

            Dim GE_X As New DenseVector(GE_A.RowCount)

            GE_X(GE_A.RowCount - 1) = GE_B(GE_A.RowCount - 1)

            For ii As Integer = GE_A.RowCount - 2 To 0 Step -1
                Dim temp As Double = 0
                Dim vecRow As New DenseVector(1)
                vecRow = GE_A.Row(ii)

                For jj As Integer = ii + 1 To GE_A.RowCount - 1
                    temp += GE_X(jj) * vecRow(jj)
                Next
                GE_X(ii) = GE_B(ii) - temp
            Next

            '? output
            Dim output As CALA_MathNet_GE_wRC_Output
            With output
                .vectorX = GE_X
                .paraSequence = GE_Seq
            End With
            Return output
        End Function

        Function GE_EXE(MatrixA As DenseMatrix, VectorB As DenseVector, parameter As CALA_MathNet_GE_wRC_Execuation_Parameters) _
            As CALA_MathNet_GE_wRC_Output

            Dim inp As CALA_MathNet_GE_wRC_Input
            With inp
                .MatrixA = MatrixA
                .VectorB = VectorB
                .para = parameter
            End With

            Dim output As CALA_MathNet_GE_wRC_Output =
                GE_EXE(inp)

            Return output

        End Function


        Private Function _checkZeroVector(vec As DenseVector, smallnumber As Double) As Boolean
            Dim result As Boolean = True
            For ii As Integer = 0 To vec.Count - 1
                If (vec(ii) > smallnumber) Or (vec(ii) < (-1) * smallnumber) Then
                    result = False
                    Exit For
                End If
            Next

            Return result
        End Function

        Private Function _checkBooleanArray_for_ALL_False(bolarray() As Boolean) As Boolean
            '! all false, return false
            '! if there is one true, return true

            Dim result As Boolean = False
            For ii As Integer = 0 To bolarray.Count - 1
                If bolarray(ii) = True Then
                    result = True
                    Exit For
                End If
            Next

            Return result
        End Function


        Private Function _sort_pivotCheck(pivCC() As Boolean) As CALA.CALA_MathNet_GE_Int_Int_Vec

            If pivCC.Count <= 0 Then Return Nothing
            Dim pivC() As Boolean : ReDim pivC(pivCC.Count - 1)
            For ii As Integer = 0 To pivCC.Count - 1 : pivC(ii) = pivCC(ii) : Next ii
            Dim ret() As Integer : ReDim ret(pivC.Count - 1)
            For ii As Integer = 0 To ret.Count - 1 : ret(ii) = ii : Next ii

            For ii As Integer = 0 To ret.Count - 1

                If pivC(ii) Then
                    For jj As Integer = ii + 1 To ret.Count - 1

                        If jj = ret.Count - 1 Then
                            '? there is all true (means, all zero rows) no more pivot possibility, 
                            GoTo _sort_pivotCheck_label2
                        Else
                            If Not (pivC(jj)) Then
                                'exchange pivC
                                pivC(ii) = False : pivC(jj) = True

                                'exchange ret
                                Dim exchange As Integer
                                exchange = ret(ii) : ret(ii) = ret(jj) : ret(jj) = exchange

                                'quit to label
                                GoTo _sort_pivotCheck_label
                            End If
                        End If
                    Next
                End If

_sort_pivotCheck_label:

            Next

_sort_pivotCheck_label2:


            '? check how many false (means no zero rows)
            Dim cc As Integer = 0  ' a counter 
            For ii As Integer = 0 To ret.Count - 1
                If Not (pivC(ii)) Then
                    cc += 1
                Else
                    GoTo _sort_pivotCheck_label3
                End If
            Next

_sort_pivotCheck_label3:

            Dim out As CALA.CALA_MathNet_GE_Int_Int_Vec
            With out
                .vec = ret
                .intt = cc
            End With

            Return out
        End Function


    End Module



    Public Class CALA_MathNet_GE_wRC_object

        Sub New()

        End Sub

    End Class




End Namespace



