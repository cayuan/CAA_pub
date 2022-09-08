


Imports CAA.CAA




Namespace CAA
    Public Module CAA_Weighting_Generate_Template

        Public Function Gen_one_Wei(FullFilePath As String, Name As String) As Integer
            If IO.File.Exists(FullFilePath) Then
                Try
                    IO.File.Delete(FullFilePath)
                Catch ex As Exception
                    Return 0
                End Try
            End If

            Dim fw As New IO.StreamWriter(FullFilePath)

            'first line : Name only
            fw.WriteLine(Trim(Name) + ",")

            'second line: title 
            Dim ti As String = ""
            ti += "," + "level," + "Weighting,"
            fw.WriteLine(ti)

            'Third line: data (1 line to show the example)
            ti = "0,0.1,99"
            fw.WriteLine(ti)


            fw.Flush()
            fw.Close()
            fw.Dispose()

            Return 1
        End Function

        Public Function Gen_two_Wei(FullFilePath As String, Name As String) As Integer
            If IO.File.Exists(FullFilePath) Then
                Try
                    IO.File.Delete(FullFilePath)
                Catch ex As Exception
                    Return 0
                End Try
            End If

            Dim fw As New IO.StreamWriter(FullFilePath)

            'first line : Name only
            fw.WriteLine(Trim(Name) + ",")

            'second line: title 
            Dim ti As String = ""
            ti += "," + "level_a," + "level_b," + "Weighting,"
            fw.WriteLine(ti)

            'Third line: data (1 line to show the example)
            ti = "0,0.1,0.3, 99"
            fw.WriteLine(ti)


            fw.Flush()
            fw.Close()
            fw.Dispose()

            Return 1
        End Function

        Public Function Gen_three_Wei(FullFilePath As String, Name As String) As Integer
            If IO.File.Exists(FullFilePath) Then
                Try
                    IO.File.Delete(FullFilePath)
                Catch ex As Exception
                    Return 0
                End Try
            End If

            Dim fw As New IO.StreamWriter(FullFilePath)

            'first line : Name only
            fw.WriteLine(Trim(Name) + ",")

            'second line: title 
            Dim ti As String = ""
            ti += "," + "level_1," + "level_2," + "level_3," + "Weighting,"
            fw.WriteLine(ti)

            'Third line: data (1 line to show the example)
            ti = "0,1,0,1, 99"
            fw.WriteLine(ti)


            fw.Flush()
            fw.Close()
            fw.Dispose()

            Return 1
        End Function




    End Module






    Public Class CAA_WT
        Public Name As String
        Public FileName As String
        Public BaseFolder As String
        Public ID As Integer
        Public lstCondition As List(Of Double())
        Public lstWei As List(Of Double)
        Public WeiType As CAA_Weighting_Format



#Region "file i/o"
        Sub New()

            lstCondition = New List(Of Double())
            lstWei = New List(Of Double)

        End Sub

        Sub New(basefolder As String,
                ID As Integer,
                Type As CAA_Weighting_Format,
                Name As String,
                fileName As String)
            lstCondition = New List(Of Double())
            lstWei = New List(Of Double)
            defineMefromFile(basefolder, ID, Type, Name, fileName)

        End Sub


        Public Function defineMefromFile(USER As CAA_USER, ID As Integer)
            Return defineMefromFile(USER.Base_Data_Folder,
                                    ID,
                                    USER.WeightingFile_Type(ID),
                                    USER.WeightingFile_Title(ID),
                                    USER.WeightingFile_Name(ID))

        End Function

        Public Function defineMefromFile(basefolder As String,
                                         ID As Integer,
                                         Type As CAA_Weighting_Format,
                                         Name As String,
                                         FileName As String) As Integer

            Dim fi As String = basefolder + FileName
            If Not (IO.Directory.Exists(basefolder)) Then Return 0
            If Not (IO.File.Exists(fi)) Then Return 0

            Me.BaseFolder = basefolder
            Me.FileName = FileName
            Me.ID = ID
            Me.Name = Name
            Me.FileName = FileName
            Me.WeiType = Type


            Dim cond() As Double
            Dim wei As Double

            Select Case Me.WeiType
                Case CAA_Weighting_Format.one
                    ReDim cond(0)
                Case CAA_Weighting_Format.two
                    ReDim cond(1)
                Case CAA_Weighting_Format.three
                    ReDim cond(2)
                Case Else
                    ReDim cond(1)
            End Select


            lstCondition = New List(Of Double())
            lstWei = New List(Of Double)


            'read from file. 
            Dim fr As IO.StreamReader
            Try
                fr = New IO.StreamReader(fi)
            Catch ex As Exception
                Return 0
            End Try


            Dim rl As String = ""

            rl = fr.ReadLine()
            rl = fr.ReadLine()


            rl = fr.ReadLine()
            Do Until (rl Is Nothing)
                Dim sp() As String = Split(rl, ",")


                For ii As Integer = 1 To cond.Count  'because the first is an index
                    cond(ii - 1) = CDbl(sp(ii))
                Next

                wei = CDbl(sp(cond.Count + 1))

                lstCondition.Add(cond)
                lstWei.Add(wei)

                rl = fr.ReadLine
            Loop

            fr.Close()
            fr.Dispose()
            Return 1
        End Function


        Public Function defineMefromObject(wt As CAA_WT)


            Me.BaseFolder = wt.BaseFolder
            Me.FileName = wt.FileName
            Me.ID = wt.ID
            Me.Name = wt.Name
            Me.FileName = wt.FileName
            Me.WeiType = wt.WeiType

            lstCondition = New List(Of Double())
            lstWei = New List(Of Double)

            Dim cond() As Double
            Select Case Me.WeiType
                Case CAA_Weighting_Format.one
                    ReDim cond(0)
                Case CAA_Weighting_Format.two
                    ReDim cond(1)
                Case CAA_Weighting_Format.three
                    ReDim cond(2)
                Case Else
                    ReDim cond(1)
            End Select

            Dim w As Double = 0

            For ind As Integer = 0 To wt.lstCondition.Count - 1
                For jj As Integer = 0 To wt.lstCondition(ind).Count - 1
                    cond(jj) = wt.lstCondition(ind)(jj)
                Next

                w = wt.lstWei(ind)

                lstCondition.Add(cond)
                lstWei.Add(w)
            Next


            Return 1

        End Function

        Public Function defineMeFromIntendedString(IntendedString As String) As String
            Dim wt As New CAA_WT
            Try
                wt = convert_jsonString2Obj(IntendedString, New CAA_WT)
                defineMefromObject(wt)
            Catch ex As Exception
                Return 0
            End Try


            Return 1
        End Function
        Public Function exportMe2File(fileName As String)
            Dim fi As String = Me.BaseFolder + fileName
            If IO.File.Exists(fi) Then
                Try
                    IO.File.Delete(fi)
                Catch ex As Exception
                    Return 0
                End Try
            End If

            Dim fr As IO.StreamWriter

            Try
                fr = New IO.StreamWriter(fi)
            Catch ex As Exception
                Return 0
            End Try


            Dim s As String = ""


            fr.WriteLine(Me.Name)

            Select Case Me.WeiType
                Case CAA_Weighting_Format.one
                    s = "," + "level," + "Weighting,"
                Case CAA_Weighting_Format.two
                    s = "," + "level_a," + "level_b," + "Weighting,"
                Case CAA_Weighting_Format.three
                    s = "," + "level_1," + "level_2," + "level_3," + "Weighting,"
                Case Else
                    s = "," + "level_a," + "level_b," + "Weighting,"
            End Select

            fr.WriteLine(s)


            For ii As Integer = 0 To lstCondition.Count - 1

                s = ii.ToString + ","

                For jj As Integer = 0 To lstCondition(ii).Count - 1
                    s += lstCondition(ii)(jj).ToString + ","
                Next

                s += lstWei(ii).ToString

                fr.WriteLine(s)
            Next


            fr.Flush()
            fr.Close()
            fr.Dispose()

            Return 1
        End Function

        Public Function exportMe2IntendedString() As String
            Return write_obj2json_IndentedString(Me)
        End Function


#End Region





    End Class










End Namespace

