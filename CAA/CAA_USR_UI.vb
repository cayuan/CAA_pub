Imports CAA
Imports CAA.CAA

Public Class CAA_USR_UI

#Region "General UI options"


    Private Function read_output_return_full_path(Typ As OpenFileType, Optional workdir As String = "C:\Temp",
                                                        Optional fileDescription As String = "") As String

        Dim filter_string As String
        Select Case Typ
            Case OpenFileType.csv
                filter_string = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
            Case OpenFileType.json
                filter_string = "json files (*.json)|*.json|All files (*.*)|*.*"
            Case Else
                filter_string = "All files (*.*)|*.*"
        End Select


        '? open a file dialog 
        With OpenFileDialog1
            .Multiselect = False
            .CheckFileExists = True
            .ReadOnlyChecked = True
            .InitialDirectory = workdir
            .Filter = filter_string
            .Title = fileDescription
            .ShowDialog()
        End With

        Dim outname As String = ""

        If IO.File.Exists(OpenFileDialog1.FileName) Then
            outname = OpenFileDialog1.FileName
        End If
        Return outname
    End Function
#End Region



    Private Sub CAA_USR_UI_Load(sender As Object, e As EventArgs) Handles Me.Load
        initializeMe()
    End Sub


    Private Sub initializeMe()
        displayUSER()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
        Form1.Show()
    End Sub



#Region "USER basic information"

    Private Sub displayUSER()
        With Form1.USER
            txtUserFilePath.Text = .USER_file_folder
            txtBaseDBDir.Text = .Base_Data_Folder
            txtWorkDir.Text = .Current_Working_Folder

            cboWeiType.Items.Clear()
            For ii As Integer = 0 To .WeightingFile_Title.Count - 1
                cboWeiType.Items.Add(.WeightingFile_Title(ii))
            Next
            cboWeiType.SelectedIndex = -1


            CleardgWei()


            For ii As Integer = 0 To .WeightingFile_Title.Count - 1
                dgWeiFile.Rows.Add()
                dgWeiFile.Rows(ii).Cells(0).Value = ii.ToString
                dgWeiFile.Rows(ii).Cells(1).Value = .WeightingFile_Title(ii).ToString
                dgWeiFile.Rows(ii).Cells(2).Value = .WeightingFile_Type(ii).ToString
                dgWeiFile.Rows(ii).Cells(3).Value = .WeightingFile_Name(ii).ToString
            Next

        End With
    End Sub


    Private Sub CleardgWei()
        dgWeiFile.Columns.Clear()
        dgWeiFile.Enabled = False
        Dim width As Double = dgWeiFile.Width / (4) - 2.5

        With dgWeiFile
            .ColumnCount = 4
            .ColumnHeadersVisible = True
            .RowHeadersWidth = 4

            .Columns(0).Name = "ID"
            .Columns(0).Width = width / 2

            .Columns(1).Name = "Name"
            .Columns(1).Width = width

            .Columns(2).Name = "Type"
            .Columns(2).Width = width / 2

            .Columns(3).Name = "FileName"
            .Columns(3).Width = width * 1.5
            .DefaultCellStyle.Font = New Font("Tahoma", 8)
        End With

    End Sub


    Private Sub getUI2USER()
        With Form1.USER
            .Base_Data_Folder = Trim(txtBaseDBDir.Text)
            .Current_Working_Folder = Trim(txtWorkDir.Text)

            For ii As Integer = 0 To .WeightingFile_Title.Count - 1
                .WeightingFile_Name(ii) = dgWeiFile.Rows(ii).Cells(3).Value.ToString
            Next
        End With
    End Sub

#End Region








#Region "USER R/W"

    Private Sub cmdUSERSave_Click(sender As Object, e As EventArgs) Handles cmdUSERSave.Click
        getUI2USER()

        Form1.USER.writeMe()

        MsgBox("USER file exported.")

    End Sub

    Private Sub cmdUSERRead_Click(sender As Object, e As EventArgs) Handles cmdUSERRead.Click
        Form1.USER.readMe()
        displayUSER()
        MsgBox("USER file read.")
    End Sub




#End Region



#Region "Assign folders"
    Private Sub cmdFChange_BaseDBDir_Click(sender As Object, e As EventArgs) Handles cmdFChange_BaseDBDir.Click
        Dim folderp As String = assignTargetFolder_direct()
        If folderp <> "" Then
            Form1.USER.Base_Data_Folder = folderp
            txtBaseDBDir.Text = folderp
        End If
    End Sub
    Private Sub cmdcmdChange_BaseDBDir_Click(sender As Object, e As EventArgs) Handles cmdcmdChange_BaseDBDir.Click
        Dim folderp As String = folderselection()
        If folderp <> "" Then
            Form1.USER.Base_Data_Folder = folderp
            txtBaseDBDir.Text = folderp
        End If
    End Sub
    Private Sub cmdFChange_WorkDir_Click(sender As Object, e As EventArgs) Handles cmdFChange_WorkDir.Click
        Dim folderp As String = assignTargetFolder_direct()
        If folderp <> "" Then
            Form1.USER.Current_Working_Folder = folderp
            txtWorkDir.Text = folderp
        End If
    End Sub

    Private Sub cmdChange_WorkDir_Click(sender As Object, e As EventArgs) Handles cmdChange_WorkDir.Click
        Dim folderp As String = folderselection()
        If folderp <> "" Then
            Form1.USER.Current_Working_Folder = folderp
            txtWorkDir.Text = folderp
        End If
    End Sub




    Private Function assignTargetFolder_direct() As String
        Dim Message, Title As String
        Message = "Direct input the target folder "    ' Set prompt.
        Title = "Folder Path"    ' Set title.

        ' Display message, title, and default value.
        Dim folderpath As String = InputBox(Message, Title, "")

        If IO.Directory.Exists(folderpath) Then
            Return CheckFolderSurfix(folderpath)

        Else
            Return Nothing
        End If
    End Function


    Private Function folderselection() As String
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer

        FolderBrowserDialog1.ShowDialog()

        Dim folderpath As String = FolderBrowserDialog1.SelectedPath

        If IO.Directory.Exists(folderpath) Then
            Return CheckFolderSurfix(folderpath)
        Else
            Return Nothing
        End If

    End Function

#End Region


#Region "Weighting files"
    Private Sub cmdRegenALLWei_Click(sender As Object, e As EventArgs) Handles cmdRegenALLWei.Click

        If Form1.USER.Base_Data_Folder = "" Then
            Exit Sub
        Else
            If Not (IO.Directory.Exists(Form1.USER.Base_Data_Folder)) Then
                Exit Sub
            End If
        End If

        For ii As Integer = 0 To Form1.USER.WeightingFile_Title.Count - 1
            Dim fullpath As String = Form1.USER.Base_Data_Folder +
                Form1.USER.WeightingFile_Name(ii)


            Select Case Form1.USER.WeightingFile_Type(ii)
                Case CAA.CAA_Weighting_Format.one
                    Call CAA.CAA_Weighting_Generate_Template.Gen_one_Wei(fullpath, Form1.USER.WeightingFile_Name(ii))
                Case CAA.CAA_Weighting_Format.two
                    Call CAA.CAA_Weighting_Generate_Template.Gen_two_Wei(fullpath, Form1.USER.WeightingFile_Name(ii))
                Case CAA.CAA_Weighting_Format.three
                    Call CAA.CAA_Weighting_Generate_Template.Gen_three_Wei(fullpath, Form1.USER.WeightingFile_Name(ii))
                Case Else
                    Call CAA.CAA_Weighting_Generate_Template.Gen_two_Wei(fullpath, Form1.USER.WeightingFile_Name(ii))
            End Select



        Next



    End Sub

    Private Sub cmdAssignFiles_Choose_Click(sender As Object, e As EventArgs) Handles cmdAssignFiles_Choose.Click
        If txtBaseDBDir.Text = "" Then Exit Sub
        If Not (IO.Directory.Exists(txtBaseDBDir.Text)) Then Exit Sub
        If cboWeiType.SelectedIndex < 0 Then Exit Sub


        Dim fi As String =
            read_output_return_full_path(
                  OpenFileType.csv,
                  txtBaseDBDir.Text,
                  Form1.USER.WeightingFile_Title(cboWeiType.SelectedIndex))

        If Not (IO.File.Exists(fi)) Then Exit Sub

        txtSelectedFile.Text = IO.Path.GetFileName(fi)

    End Sub

    Private Sub cmdAssignFiles_Assign_Click(sender As Object, e As EventArgs) Handles cmdAssignFiles_Assign.Click
        If txtBaseDBDir.Text = "" Then Exit Sub
        If Not (IO.Directory.Exists(txtBaseDBDir.Text)) Then Exit Sub

        If cboWeiType.SelectedIndex < 0 Then Exit Sub

        Dim fi As String = txtBaseDBDir.Text + txtSelectedFile.Text
        If Not (IO.File.Exists(fi)) Then Exit Sub

        dgWeiFile.Rows(cboWeiType.SelectedIndex).Cells(3).Value = IO.Path.GetFileName(fi)

    End Sub




#End Region




End Class