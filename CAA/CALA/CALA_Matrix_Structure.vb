'? version: 20210514, Matrix FullDense 



Imports MathNet.Numerics.LinearAlgebra.Double


Namespace CALA

    Public Module CALA_Matrix_Structure_General
        Enum CALA_Matrix_Type
            FullDense
            Symmetry                'The data would be stored as a Lower Triangular. In computation, it would be a symm matrix
            LowerTriangular     'This is only Triangular
            UpperTriangular     'Only triangular
            TridiagonalSym
        End Enum
        Public Class CALA_Matrix_output_format
            Public Name As String
            Public MType As CALA_Matrix_Type
            Public m_Row_Count As Long
            Public n_Col_Count As Long
            Public a() As Double
        End Class

    End Module

    Public Class CALA_Matrix_FullDense
            '? varibles
            Dim _name As String
            Dim _a() As Double
            Dim _m As Long   'the count of Row
            Dim _n As Long    'the count of Column

            Dim _basedir As String   'this learves to future
            Dim _type As CALA_Matrix_Type


            '?properties
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
                    Return _basedir
                End Get
                Set(value As String)
                    _basedir = value
                End Set
            End Property
            Property MType As CALA_Matrix_Type
                Get
                    Return Me._type
                End Get
                Set(value As CALA_Matrix_Type)
                    Me._type = value
                End Set
            End Property
            ReadOnly Property a_count As Long
                Get
                    Return _a.Count
                End Get
            End Property

            Property m As Long
                Get
                    Return _m
                End Get
                Set(value As Long)
                    _m = value
                    RedefineMatrix_Preserve(_m, _n)
                End Set
            End Property
            Property RowCount As Long
                Get
                    Return _m
                End Get
                Set(value As Long)
                    _m = value
                    RedefineMatrix_Preserve(_m, _n)
                End Set
            End Property
            Property n As Long
                Get
                    Return _n
                End Get
                Set(value As Long)
                    _n = value
                    RedefineMatrix_Preserve(_m, _n)
                End Set
            End Property
            Property ColCount As Long
                Get
                    Return _n
                End Get
                Set(value As Long)
                    _n = value
                    RedefineMatrix_Preserve(_m, _n)
                End Set
            End Property

            Property a(i As Long, j As Long) As Double
                Get
                'Dim Index As Long = i + _m * j
                Dim Index As Long = _convertIndex(i, j)
                If Index > _a.Count - 1 Then
                    Return 0
                End If
                If Index > -1 Then
                        Return _a(Index)
                    Else
                        Return 0
                    End If

                End Get
                Set(value As Double)
                    'Dim Index As Long = i + _m * j
                    Dim Index As Long = _convertIndex(i, j)
                    If Index > _a.Count - 1 Then
                        Throw New System.Exception("Matrix dimension error.  Current dimension is (" +
                                                   _m.ToString + "," + _n.ToString + ", but now Index is " + Index.ToString)
                    Else
                        If Index > -1 Then
                            _a(Index) = value
                        End If

                    End If
                End Set
            End Property
        ReadOnly Property row_vec(ii As Integer) As Double()
            Get
                Dim xx() As Double : ReDim xx(Me.ColCount - 1)
                For jj As Integer = 0 To Me.ColCount - 1
                    xx(jj) = Me.a(ii, jj)
                Next
                Return xx
            End Get
        End Property

        ReadOnly Property col_vec(jj As Integer) As Double()
            Get
                Dim yy() As Double : ReDim yy(Me.RowCount - 1)
                For ii As Integer = 0 To Me.RowCount - 1
                    yy(ii) = Me.a(ii, jj)
                Next
                Return yy
            End Get
        End Property

        ReadOnly Property row_max(ii As Integer) As Double
            Get
                Dim xx() As Double : ReDim xx(Me.ColCount - 1)
                For jj As Integer = 0 To Me.ColCount - 1
                    xx(jj) = Me.a(ii, jj)
                Next
                Return _find_Max_in_vector_array(xx)
            End Get
        End Property
        ReadOnly Property row_min(ii As Integer) As Double
            Get
                Dim xx() As Double : ReDim xx(Me.ColCount - 1)
                For jj As Integer = 0 To Me.ColCount - 1
                    xx(jj) = Me.a(ii, jj)
                Next
                Return _find_Min_in_vector_array(xx)
            End Get
        End Property
        ReadOnly Property col_max(ii As Integer) As Double
            Get
                Dim xx() As Double : ReDim xx(Me.RowCount - 1)
                For jj As Integer = 0 To Me.RowCount - 1
                    xx(jj) = Me.a(jj, ii)
                Next
                Return _find_Max_in_vector_array(xx)
            End Get
        End Property
        ReadOnly Property col_min(ii As Integer) As Double
            Get
                Dim xx() As Double : ReDim xx(Me.RowCount - 1)
                For jj As Integer = 0 To Me.RowCount - 1
                    xx(jj) = Me.a(jj, ii)
                Next
                Return _find_Min_in_vector_array(xx)
            End Get
        End Property
        ReadOnly Property me_max As Double
            Get
                Dim out As Double = Me.a(0, 0)
                For ii As Integer = 0 To Me.RowCount - 1
                    For jj As Integer = 0 To Me.ColCount - 1
                        If out < Me.a(ii, jj) Then
                            out = Me.a(ii, jj)
                        End If
                    Next
                Next
                Return out
            End Get
        End Property
        ReadOnly Property me_min As Double
            Get
                Dim out As Double = Me.a(0, 0)
                For ii As Integer = 0 To Me.RowCount - 1
                    For jj As Integer = 0 To Me.ColCount - 1
                        If out > Me.a(ii, jj) Then
                            out = Me.a(ii, jj)
                        End If
                    Next
                Next
                Return out
            End Get
        End Property
        ReadOnly Property me_abs_max As Double
            Get
                Dim out As Double = Math.Abs(Me.a(0, 0))
                For ii As Integer = 0 To Me.RowCount - 1
                    For jj As Integer = 0 To Me.ColCount - 1
                        If out < Math.Abs(Me.a(ii, jj)) Then
                            out = Math.Abs(Me.a(ii, jj))
                        End If
                    Next
                Next
                Return out
            End Get
        End Property
        ReadOnly Property me_abs_min As Double
            Get
                Dim out As Double = Math.Abs(Me.a(0, 0))
                For ii As Integer = 0 To Me.RowCount - 1
                    For jj As Integer = 0 To Me.ColCount - 1
                        If out > Math.Abs(Me.a(ii, jj)) Then
                            out = Math.Abs(Me.a(ii, jj))
                        End If
                    Next
                Next
                Return out
            End Get
        End Property
        ReadOnly Property me_diag As Double()
            Get
                Dim w() As Double : ReDim w(Math.Min(Me.RowCount, Me.ColCount) - 1)
                For ii As Integer = 0 To Math.Min(Me.RowCount, Me.ColCount) - 1

                    w(ii) = Me.a(ii, ii)
                Next
                Return w
            End Get
        End Property


        '? **new**
        Public Sub New(Optional baseDir As String = "c:\temp\")
                Setup(, , , baseDir)
            End Sub
            Public Sub New(m As Long, n As Long, Optional baseDir As String = "c:\temp\")
                Setup(, m, n, baseDir)
            End Sub

            Public Sub New(n As Long, Optional baseDir As String = "c:\temp\")
                Setup(, n, n, baseDir)
            End Sub

            Public Sub New(Name As String, m As Long, n As Long, Optional baseDir As String = "c:\temp\")
                Setup(Name, m, n, baseDir)
            End Sub

            Public Sub New(Name As String, n As Long, Optional baseDir As String = "c:\temp\")

                Setup(Name, n, n, baseDir)
            End Sub


            '?subroutine
            Overridable Sub Setup(Optional Name As String = "",
                                         Optional m As Long = 1,
                                         Optional n As Long = 1,
                                         Optional baseDir As String = "c:\temp")
                Me.Name = Trim(Name)
                If m <> n Then
                    Call RedefineMatrix(m, n)
                Else
                    Call RedefineMatrix(m)
                End If
                _basedir = baseDir
                Me._type = CALA_Matrix_Type.FullDense

            End Sub
            Public Sub RedefineMatrix(m As Long, n As Long)
                If m > 0 And n > 0 Then
                    _m = m : _n = n
                    ReDim _a(Me._convertTotalIndex)
                End If
            End Sub
            Public Sub RedefineMatrix(n As Long)
                If n > 0 Then
                    _m = n : _n = n
                    ReDim _a(Me._convertTotalIndex2)
                End If
            End Sub
            Public Sub RedefineMatrix_Preserve(m As Long, n As Long)
                If m > 0 And n > 0 Then
                    _m = m : _n = n
                    ReDim Preserve _a(Me._convertTotalIndex)
                End If
            End Sub
            Public Sub RedefineMatrix_Preserve(n As Long)
                If n > 0 Then
                    _m = n : _n = n
                    ReDim Preserve _a(Me._convertTotalIndex2)
                End If
            End Sub

            '!+ **converting function**
            '? convertng Index
            Overridable Function _convertIndex(i As Long, j As Long) As Long
                Return i + Me._m * j
            End Function
            Overridable Function _convertTotalIndex() As Long
                Return Me._m * Me._n - 1
            End Function
            Overridable Function _convertTotalIndex2() As Long
                Return Me._n * Me._n - 1
            End Function
            '? Convert between MathNet
            Public Sub MathNet2Me(MM As DenseMatrix)

                RedefineMatrix(MM.RowCount, MM.ColumnCount)
                For i As Integer = 0 To MM.RowCount - 1
                    For j As Integer = 0 To MM.ColumnCount - 1
                        Me.a(i, j) = MM.At(i, j)
                    Next
                Next
            End Sub

            Public Function Me2MathNet() As DenseMatrix
                Dim out As New DenseMatrix(_m, _n)
                For i As Integer = 0 To _m - 1
                    For j As Integer = 0 To _n - 1
                        out.At(i, j, Me.a(i, j))
                    Next
                Next
                Return out
            End Function

        '?Conver  me to standard matrix output 

        ' **CALA_Matrix_output_format**

        Public Sub _CALAMatrixOutput2Me(CALAmatrix As CALA_Matrix_output_format)
            With CALAmatrix
                RedefineMatrix(.m_Row_Count, .n_Col_Count)
                Me.Name = .Name
                Me._type = .MType
                For i As Integer = 0 To .m_Row_Count - 1
                    For j As Integer = 0 To .n_Col_Count - 1
                        Dim index As Long = i + .m_Row_Count * j

                        Me.a(i, j) = CALAmatrix.a(index)
                    Next
                Next
            End With

        End Sub

        Public Function _Me2CALAMatrixOutput() As CALA_Matrix_output_format
            Dim out As New CALA_Matrix_output_format
            With out
                .Name = Me.Name
                .MType = Me._type
                .m_Row_Count = Me.RowCount
                .n_Col_Count = Me.ColCount
                ReDim .a(.m_Row_Count * .n_Col_Count - 1)


                For i As Integer = 0 To Me.RowCount - 1
                    For j As Integer = 0 To Me.ColCount - 1
                        Dim index As Long = i + .m_Row_Count * j
                        out.a(index) = Me.a(i, j)
                    Next
                Next
            End With

            Return out

        End Function


        '? Convert between json
        Public Sub Json2Me(file As String)
                '_convert file to CALA_mat format _ 

                Dim s_path As String =
                    CALA_convert_basedir_file(Me._basedir, file, CALA_Output_file_Type.json)
                Dim CALAMat As CALA_Matrix_output_format =
                    read_json2obj(s_path, New CALA_Matrix_output_format)

                '_convert to me 

                '?+ **This is only for Full Dense. The others should be care on Mtype**
                'If CALAMat.MType = CALA_Matrix_Type.FullDense Then

                'End If
                Call _CALAMatrixOutput2Me(CALAMat)
            End Sub

            Public Sub JsonString2Me(str As String)

            End Sub

            Public Sub Me2Json(file As String)
                ' _convert me to CALA format_
                Dim CALAMat As CALA_Matrix_output_format _
                    = Me._Me2CALAMatrixOutput

                ' _convert CALAformat to json_
                Dim s_path As String =
                    CALA_convert_basedir_file(Me._basedir, file, CALA_Output_file_Type.json)
                CALA.write_obj2json(CALAMat, s_path)
            End Sub



        '? Convert between EXCEL (csv)
        Public Sub CSV2Me(file As String)
            Dim s_path As String = ""

            If IO.Path.GetDirectoryName(file) <> "" Then
                If IO.File.Exists(file) Then
                    s_path = file
                End If
            Else
                s_path = CALA_convert_basedir_file(Me.BaseDir, file, CALA_Output_file_Type.csv)
            End If


            'Dim s_path As String =
            '    CALA_convert_basedir_file(Me.BaseDir, file, CALA_Output_file_Type.csv)

            Dim CALA_mat As CALA_Matrix_output_format =
                CALA_import_CSV_2_Matrix_format(s_path)

            Me._CALAMatrixOutput2Me(CALA_mat)

        End Sub

        Public Sub Me2CSV(file As String)
                Dim s_path As String =
                    CALA_convert_basedir_file(Me.BaseDir, file, CALA_Output_file_Type.csv)

                Dim CALA_Mat As CALA_Matrix_output_format =
                    Me._Me2CALAMatrixOutput

                CALA_export_Matrix_output_2_CSV(CALA_Mat, s_path)


            End Sub

            '? Convert me 2 String output
            Public Function Me2String() As String
                Dim out As String = ""
                For jj As Integer = 0 To Me._n - 1

                    For ii As Integer = 0 To Me._m - 1
                        out += Me.a(ii, jj).ToString + ","
                    Next
                    out = Left(out, Len(out) - 1)
                    out += vbNewLine
                Next
                Return out
            End Function

            Public Function Me2String_info() As String
                Dim out As String = ""
                out += "Name=" + Me.Name+vbNewLine
                out += "Type=" + Me.MType.ToString + vbNewLine
                out += "RowCount=" + Me.RowCount.ToString + "  ColCount=" + Me.ColCount.ToString + vbNewLine
                out += Me2String()
                Return out
            End Function


        Private Function _find_Max_in_vector_array(w() As Double) As Double
            Dim out As Double = w(0)
            For ii As Integer = 0 To w.Count - 1
                If out < w(ii) Then
                    out = w(ii)
                End If
            Next
            Return out
        End Function
        Private Function _find_Min_in_vector_array(w() As Double) As Double
            Dim out As Double = w(0)
            For ii As Integer = 0 To w.Count - 1
                If out > w(ii) Then
                    out = w(ii)
                End If
            Next
            Return out
        End Function
    End Class

    Public Class CALA_Matrix_LowerTriangular
            Inherits CALA_Matrix_FullDense

            '? **new**
            Public Sub New(Optional baseDir As String = "c:\temp\")
                Setup(, , , baseDir)
            End Sub
            Public Sub New(m As Long, n As Long, Optional baseDir As String = "c:\temp\")
                Setup(, m, n, baseDir)
            End Sub

            Public Sub New(n As Long, Optional baseDir As String = "c:\temp\")
                Setup(, n, n, baseDir)
            End Sub

            Public Sub New(Name As String, m As Long, n As Long, Optional baseDir As String = "c:\temp\")
                Setup(Name, m, n, baseDir)
            End Sub

            Public Sub New(Name As String, n As Long, Optional baseDir As String = "c:\temp\")
                Setup(Name, n, n, baseDir)
            End Sub
            Overrides Sub Setup(Optional Name As String = "",
                                         Optional m As Long = 1,
                                         Optional n As Long = 1,
                                         Optional baseDir As String = "c:\temp")
                Me.Name = Trim(Name)
                If m <> n Then
                    Call RedefineMatrix(m, n)
                Else
                    Call RedefineMatrix(m)
                End If
                Me.BaseDir = baseDir
                Me.MType = CALA_Matrix_Type.LowerTriangular

            End Sub

            '!+ **converting function**
            '? converting the index
            Overrides Function _convertIndex(i As Long, j As Long) As Long
                If i >= j Then
                    Dim Ind As Long = 0
                    Dim ColCount As Long = 0
                    Dim Index As Long = 0
                    For tt As Long = 0 To j - 1
                        Ind += Me.RowCount - tt
                        ColCount += 1
                    Next

                    Index = Ind + (i - ColCount)

                    If Index > Me.a_count - 1 Then
                        Throw New System.Exception("Matrix dimension error.  Current dimension is (" +
                                               Me.RowCount.ToString + "," + Me.ColCount.ToString + ", but now Index is " + Index.ToString)
                    End If
                    Return Index
                Else
                    Return -1
                End If
            End Function

            Overrides Function _convertTotalIndex() As Long
                Dim cc As Long = 0 ' this is the count
                For ii As Integer = Me.RowCount To 1 Step -1
                    cc += ii
                Next
                Return cc - 1
            End Function

            Overrides Function _convertTotalIndex2() As Long
                Return _convertTotalIndex()
            End Function


        End Class


    Public Class CALA_Matrix_UpperTriangular
        Inherits CALA_Matrix_FullDense

        '? **new**
        Public Sub New(Optional baseDir As String = "c:\temp\")
            Setup(, , , baseDir)
        End Sub
        Public Sub New(m As Long, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(, m, n, baseDir)
        End Sub

        Public Sub New(n As Long, Optional baseDir As String = "c:\temp\")
            Setup(, n, n, baseDir)
        End Sub

        Public Sub New(Name As String, m As Long, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(Name, m, n, baseDir)
        End Sub

        Public Sub New(Name As String, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(Name, n, n, baseDir)
        End Sub
        Overrides Sub Setup(Optional Name As String = "",
                                     Optional m As Long = 1,
                                     Optional n As Long = 1,
                                     Optional baseDir As String = "c:\temp")
            Me.Name = Trim(Name)
            If m <> n Then
                Call RedefineMatrix(m, n)
            Else
                Call RedefineMatrix(m)
            End If
            Me.BaseDir = baseDir
            Me.MType = CALA_Matrix_Type.UpperTriangular

        End Sub

        '!+ **converting function**
        '? converting the index
        Overrides Function _convertIndex(i As Long, j As Long) As Long
            If i <= j Then
                Dim Ind As Long = 0
                Dim ColCount As Long = 0
                Dim Index As Long = 0
                For tt As Long = 0 To j - 1
                    Ind += Me.RowCount - tt
                    ColCount += 1
                Next

                Index = Ind + (i - ColCount)

                If Index > Me.a_count - 1 Then
                    Throw New System.Exception("Matrix dimension error.  Current dimension is (" +
                                           Me.RowCount.ToString + "," + Me.ColCount.ToString + ", but now Index is " + Index.ToString)
                End If
                Return Index
            Else
                Return -1
            End If
        End Function

        Overrides Function _convertTotalIndex() As Long
            Dim cc As Long = 0 ' this is the count
            For ii As Integer = Me.RowCount To 1 Step -1
                cc += ii
            Next
            Return cc - 1
        End Function

        Overrides Function _convertTotalIndex2() As Long
            Return _convertTotalIndex()
        End Function


    End Class



    Public Class CALA_Matrix_Symmetry
        Inherits CALA_Matrix_FullDense

        '? **new**
        Public Sub New(Optional baseDir As String = "c:\temp\")
            Setup(, , , baseDir)
        End Sub
        Public Sub New(m As Long, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(, m, n, baseDir)
        End Sub

        Public Sub New(n As Long, Optional baseDir As String = "c:\temp\")
            Setup(, n, n, baseDir)
        End Sub

        Public Sub New(Name As String, m As Long, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(Name, m, n, baseDir)
        End Sub

        Public Sub New(Name As String, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(Name, n, n, baseDir)
        End Sub
        Overrides Sub Setup(Optional Name As String = "",
                                         Optional m As Long = 1,
                                         Optional n As Long = 1,
                                         Optional baseDir As String = "c:\temp")
            Me.Name = Trim(Name)
            If m <> n Then
                Call RedefineMatrix(m, n)
            Else
                Call RedefineMatrix(m)
            End If
            Me.BaseDir = baseDir
            Me.MType = CALA_Matrix_Type.Symmetry

        End Sub

        '!+ **converting function**
        '? converting the index
        Overrides Function _convertIndex(i As Long, j As Long) As Long
            Dim ii As Long = Math.Max(i, j)
            Dim jj As Long = Math.Min(i, j)

            Dim Ind As Long = 0
            Dim ColCount As Long = 0
            Dim Index As Long = 0
            For tt As Long = 0 To jj - 1
                Ind += Me.RowCount - tt
                ColCount += 1
            Next

            Index = Ind + (ii - ColCount)
            If Index > Me.a_count - 1 Then
                Throw New System.Exception("Matrix dimension error.  Current dimension is (" +
                                           Me.RowCount.ToString + "," + Me.ColCount.ToString + ", but now Index is " + Index.ToString)
            End If
            Return Index

        End Function

        Overrides Function _convertTotalIndex() As Long
            Dim cc As Long = 0 ' this is the count
            For ii As Integer = Me.RowCount To 1 Step -1
                cc += ii
            Next
            Return cc - 1
        End Function

        Overrides Function _convertTotalIndex2() As Long
            Return _convertTotalIndex()
        End Function

    End Class
    Public Class CALA_Matrix_TridiagonalSym
        Inherits CALA_Matrix_FullDense

        '? **new**
        Public Sub New(Optional baseDir As String = "c:\temp\")
            Setup(, , , baseDir)
        End Sub
        Public Sub New(m As Long, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(, m, n, baseDir)
        End Sub

        Public Sub New(n As Long, Optional baseDir As String = "c:\temp\")
            Setup(, n, n, baseDir)
        End Sub

        Public Sub New(Name As String, m As Long, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(Name, m, n, baseDir)
        End Sub

        Public Sub New(Name As String, n As Long, Optional baseDir As String = "c:\temp\")
            Setup(Name, n, n, baseDir)
        End Sub
        Overrides Sub Setup(Optional Name As String = "",
                                         Optional m As Long = 1,
                                         Optional n As Long = 1,
                                         Optional baseDir As String = "c:\temp")
            Me.Name = Trim(Name)
            If m <> n Then
                Call RedefineMatrix(m, n)
            Else
                Call RedefineMatrix(m)
            End If
            Me.BaseDir = baseDir
            Me.MType = CALA_Matrix_Type.TridiagonalSym

        End Sub

        '!+ **converting function**
        '? converting the index
        Overrides Function _convertIndex(i As Long, j As Long) As Long
            Dim Index As Long = 0

            If i = j Then
                Index = i
            End If

            If Math.Abs(i - j) >= 2 Then
                Index = -1
            End If

            If Math.Abs(i - j) = 1 Then
                Index = Me.RowCount + Math.Min(i, j)
            End If

            If Index > Me.a_count - 1 Then
                Throw New System.Exception("Matrix dimension error.  Current dimension is (" +
                                           Me.RowCount.ToString + "," + Me.ColCount.ToString + ", but now Index is " + Index.ToString)
            End If
            Return Index

        End Function

        Overrides Function _convertTotalIndex() As Long

            Return Me.RowCount + (Me.RowCount - 1) - 1
        End Function

        Overrides Function _convertTotalIndex2() As Long
            Return _convertTotalIndex()
        End Function
    End Class

    Module CALA_Matrix_Structure
    End Module

    Module CALA_Matrix_Public_Functions


        Public Function CALA_convert_basedir_file(basedir As String, file As String,
                                                  output_type As CALA.CALA_Output_file_Type) As String

            If file <> "" And IO.Path.GetDirectoryName(file) <> "" Then
                Return file
            End If

            Dim out As String = basedir
            If Right(out, 1) <> "\" Then out += "\"
            If IO.Directory.Exists(out) Then
                Select Case output_type
                    Case CALA_Output_file_Type.json
                        If Right(file, 5).ToLower <> ".json" Then
                            out += file + ".json"
                        Else
                            out += file
                        End If
                    Case CALA_Output_file_Type.csv
                        If Right(file, 4).ToLower <> ".csv" Then
                            out += file + ".csv"
                        Else
                            out += file
                        End If
                End Select

            Else
                Return ""
            End If

            Return out
        End Function

        Public Sub CALA_export_Matrix_output_2_CSV(CALA_Mat As CALA_Matrix_output_format,
                                                        full_path As String)
            Dim fw As New IO.StreamWriter(full_path)
            Dim sout As String = ""

            '? line 1: Name, <Name>, Type, <Type>
            'line 2: ,,0,1,2,  (n)
            'line 3: ,<i>,a(i,j)

            ' L1
            sout = ""
            sout = "Name, " + CALA_Mat.Name + ","
            sout += "Type," + CALA_Mat.MType.ToString
            fw.WriteLine(sout)

            'L2
            sout = ""
            sout += ","
            For ii As Integer = 0 To CALA_Mat.n_Col_Count - 1
                sout += ii.ToString + ","
            Next

            sout = Left(sout, Len(sout) - 1)
            fw.WriteLine(sout)

            'L3 and the rest

            sout = ""
            For ii As Integer = 0 To CALA_Mat.m_Row_Count - 1
                sout = ""
                sout += ii.ToString + ","
                For jj As Integer = 0 To CALA_Mat.n_Col_Count - 1
                    Dim index As Long = ii + jj * CALA_Mat.m_Row_Count
                    sout += CALA_Mat.a(index).ToString + ","
                Next
                sout = Left(sout, Len(sout) - 1)
                fw.WriteLine(sout)
            Next

            fw.Close()
            fw.Dispose()


        End Sub

        Public Function CALA_import_CSV_2_Matrix_format(full_path As String) As CALA_Matrix_output_format
            If Not (IO.File.Exists(full_path)) Then Return Nothing

            Dim CALA_Mat As New CALA_Matrix_output_format


            '? *The concept*
            'read name
            'read type: you can find the Enum converting method here
            'read m
            'read n 
            'rewind
            'read the matrix


            Dim fr As New IO.StreamReader(full_path)
            Dim sr As String
            Dim s() As String

            ' Read Line 1
            sr = fr.ReadLine : s = Split(sr, ",")
            CALA_Mat.Name = Trim(s(1))


            Dim Enumval As Integer =
                System.Enum.Parse(GetType(CALA_Matrix_Type), Trim(s(3)))
            CALA_Mat.MType = CType(Enumval, CALA_Matrix_Type)

            'read line 2 (skip)
            ' read line 3
            sr = fr.ReadLine : sr = fr.ReadLine : s = Split(sr, ",")

            Dim colcount As Long = -1
            For ij As Integer = 1 To s.Count - 1
                If IsNumeric(s(ij)) Then
                    Try
                        Dim d As Double = CDbl(s(ij))
                        colcount += 1
                    Catch ex As Exception
                    End Try
                End If
            Next
            CALA_Mat.n_Col_Count = colcount + 1

            Dim rowcount As Long = 1
            Do Until (sr Is Nothing)

                rowcount += 1
                sr = fr.ReadLine
            Loop
            CALA_Mat.m_Row_Count = rowcount - 1

            ReDim CALA_Mat.a(CALA_Mat.m_Row_Count * CALA_Mat.n_Col_Count - 1)

            fr.DiscardBufferedData()
            fr.BaseStream.Seek(0, IO.SeekOrigin.Begin)
            fr.ReadLine()
            fr.ReadLine() ' skip 2 lines

            Dim ii As Long = 0
            sr = ""  '! **This is required! because the previous sr is nothing, it will trigger the next Do loop**

            Do Until (sr Is Nothing)
                sr = fr.ReadLine

                s = Split(sr, ",")
                If ii <= CALA_Mat.m_Row_Count - 1 Then
                    For jj As Integer = 0 To CALA_Mat.n_Col_Count - 1
                        Dim index = ii + CALA_Mat.m_Row_Count * jj
                        CALA_Mat.a(index) = CDbl(s(jj + 1))
                    Next
                End If

                ii += 1
            Loop


            fr.Close()
            fr.Dispose()


            Return CALA_Mat

        End Function



    End Module



End Namespace

