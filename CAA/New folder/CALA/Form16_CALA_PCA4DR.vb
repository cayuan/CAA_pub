Imports CAYMAL.CALA
Imports MathNet.Numerics.LinearAlgebra.Double



Public Class Form16_CALA_PCA4DR

    Dim workDir As String
    Dim targetDB As String


    '? paracls : read write/ application: post 
    '? read file: 
    '? para 
    '? preDP
    '? postDP
    '?  kp (kernel parameters)
    '? ginp (gaussian input parameters)
    '? go (gaussian output parameters)
    '? matXs (X*) 
    '---B
    Dim preDP As CALA_Covariance_pre_DataPurge_input
    Dim postDP As CALA_Covariance_Data_Purge_Output
    Dim outDP As CALA_Covariance_output_format

    Dim ginp As CALA_GP_input
    Dim gout As CALA_GP_output
    Dim para() As parameterClass

    Dim matXs As CALA_Matrix_FullDense

    Dim para_DP() As parameterClass
    Dim para_DP_json_file_full_path As String
    Dim para_GP() As parameterClass

    Dim cboYparaSelect_prev_index As Integer

    '? the internal use varibles 
    Dim mat_pre_GP_Xs As CALA_Matrix_FullDense



    '?  **CovK, CovKs, CovKss**
    Dim CovK As CALA_Cov
    Dim CovKs As CALA_Cov
    Dim CovKss As CALA_Cov


    Dim kp As CALA.CALA_Kernel_Parameters
    Dim epsilon As Double

    '? GP result

    Dim GP_out As CALA_GP_output
    Dim GP_ori_X As CALA_Matrix_FullDense
    Dim GP_ori_y() As Double


    'PCA4DR

    Dim QR_Hess As CALA.CALA_QR_Hessenberg_Iteration_output




    '? chart use
    Dim dvOriData As DataVisualization.Charting.Series
    Dim dvGPData As DataVisualization.Charting.Series
    Dim dvGPErrorBar As DataVisualization.Charting.Series

    Dim dv_ori_X_setN() As Integer 'store the y display sequence, it is depend on the distance
    Dim dv_ori_X_dist() As Double  'store the assumed x distance data

    Dim dv_res_X_setN() As Integer 'store the y display sequence, it is depend on the distance
    Dim dv_res_X_dist() As Double  'store the assumed x distance data
    '---
    '---
    '? UI
    '---


    '?add txt function
    Public Delegate Sub txt_Delegate(ByVal txt As String)
    Public Sub Addtxt_2_txtDS_Before_FileName(ByVal txt As String)
        '?+ **For <txtDS_Before_FileName>**
        If Me.InvokeRequired Then
            Dim d As New txt_Delegate(AddressOf Addtxt_2_txtDS_Before_FileName)
            Me.Invoke(d, New Object() {txt})
        Else
            txtDS_Before_FileName.Text = (txt)
        End If
    End Sub

    Public Sub Addtxt_2_txtDS_Directly_Import(ByVal txt As String)

    End Sub

    Public Sub Addtxt_2_txtGP_output(ByVal txt As String)
        '?+ **For <Addtxt_2_txtGP_output>**
        If Me.InvokeRequired Then
            Dim d As New txt_Delegate(AddressOf Addtxt_2_txtGP_output)
            Me.Invoke(d, New Object() {txt})
        Else
            txtGP_output.AppendText(txt + vbNewLine)
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Call CloseMe()
    End Sub

    Private Sub Form15_CALA_GP_Load(sender As Object, e As EventArgs) Handles Me.Load

        workDir = Trim(Form1.lblWorkDir.Text)
        txtWorkingDir.Text = workDir

        Call Initial_Settings()

        lblCopyright.Text = CAYMAL_const_structure.CAYMAL_copyright.copywright

    End Sub


    Private Sub Initial_Settings()
        lblVersion.Text = My.Application.Info.Version.ToString


        '? light control 
        picGreenLight.Visible = False
        picredLight.Visible = False
        picEmpty.Visible = True


        '?+ **Initial varibles**


        '? clean txt
        txtDS_Before_FileName.Text = ""

        txtGP_output.Text = ""



        '? default constants


        txtP1setting.Text = "1"
        txtP2setting.Text = "1"
        txtP3setting.Text = "1"


        txtEpsilonsetting.Text = "1e-4"

        txtQRCutsetting.Text = "0"
        txtQRLimitsetting.Text = "250"
        txtQRStopsetting.Text = CALA_constants.small_number.ToString
        txtA_valuesetting.Text = "175"
        '? add combo: Kernel type
        cboKernelType.Items.Clear()
        Dim items As Array
        items = System.Enum.GetNames(GetType(CALA_Kernel_Type))
        For Each af As String In items
            cboKernelType.Items.Add(af)
        Next
        cboKernelType.SelectedIndex = 0



        cboYparaSelect_prev_index = 0

        chkInvDisplay.Checked = False



        '? objects 
        preDP.matXX = New CALA_Matrix_FullDense(workDir)
        preDP.matYY = New CALA_Matrix_FullDense(workDir)


        '? clean para cls
        ReDim para_DP(0)
        ReDim para_GP(0)



        '? chart initialize
        '_initial_chartGP()


    End Sub



    Private Sub CloseMe()

        Me.Close()
        Form1.Show()


    End Sub

    Private Sub FastChangeDirToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FastChangeDirToolStripMenuItem1.Click
        assignTargetFolder_direct()
    End Sub
    Private Sub cmdReadTrsio_and_go_DP_Click(sender As Object, e As EventArgs) Handles cmdReadTrsio_and_go_DP.Click
        Me.preDP = _read_preDP_trsioCSV()
    End Sub
    Private Sub cmd_Export_preDP_json_Click(sender As Object, e As EventArgs) Handles cmd_Export_preDP_json.Click
        If (preDP.matXX IsNot Nothing) And
                (preDP.matYY IsNot Nothing) Then
            Call _export_preDP_2_json()
        End If
    End Sub
    Private Sub cmdRead_preDP_json_Click(sender As Object, e As EventArgs) Handles cmdRead_preDP_json.Click
        Call _import_preDP_json_()
    End Sub
    Private Sub cmd_Export_preDP_matXX_YY_json_Click(sender As Object, e As EventArgs)
        Call _export_preDP_matXX_YY_json()
    End Sub
    Private Sub cmd_Export_preDP_matXX_YY_csv_Click(sender As Object, e As EventArgs)
        Call _export_preDP_matXX_YY_csv()
    End Sub
    Private Sub cmdReadMatXX_and_go_DP_Click(sender As Object, e As EventArgs) Handles cmdReadMatXX_and_go_DP.Click
        '? only read matXX 
        Call _import_preDP_matXX_json_only()
    End Sub
    Private Sub cmdReadMatYY_and_go_DP_Click(sender As Object, e As EventArgs)
        '? only read matYY
        Call _import_preDP_matYY_json_only()
    End Sub
    Private Sub cmdReadMatXX_csv_and_go_DP_Click(sender As Object, e As EventArgs) Handles cmdReadMatXX_csv_and_go_DP.Click
        Call _import_preDP_matXX_csv_only()
    End Sub
    Private Sub cmdReadMatYY_csv_and_go_DP_Click(sender As Object, e As EventArgs)
        Call _import_preDP_matYY_csv_only()
    End Sub
    Private Sub cmdRead_Parameters_file_from_json_Click(sender As Object, e As EventArgs) Handles cmdRead_Parameters_file_from_json.Click
        Call _import_Parameters_file_from_json()
    End Sub
    Private Sub cmdCalculate_para_from_DP_Click(sender As Object, e As EventArgs) Handles cmdCalculate_para_from_DP.Click
        '? this option is mainly for test, not for real use
        Call _generate_parameter_from_preDP()
    End Sub
    Private Sub cmdGo_Data_Purge_Click(sender As Object, e As EventArgs) Handles cmdGo_Data_Purge.Click
        Call _go_Data_Purge()
    End Sub
    Private Sub cmd_Export_postDP_matX_Y_S_N_json_Click(sender As Object, e As EventArgs) Handles cmd_Export_postDP_matX_Y_S_N_json.Click
        Call _export_postDP_matX_Y_S_N_json()
    End Sub
    Private Sub cmdRead_postDP_json_Click(sender As Object, e As EventArgs) Handles cmdRead_postDP_json.Click
        Call _read_postDP_json()
    End Sub
    Private Sub cmd_Export_postDP_matX_Y_S_csv_Click(sender As Object, e As EventArgs)
        Call _export_postDP_matX_Y_S_csv()
    End Sub
    Private Sub cmdGenCovK_Click(sender As Object, e As EventArgs)
        Call _generate_covK_all()
    End Sub

    Private Sub cmdExportpara_GP_Click(sender As Object, e As EventArgs)
        ' _export_para_GP()
    End Sub
    Private Sub cmdGenPreCovK_Click(sender As Object, e As EventArgs)
        _generate_pre_covK()
    End Sub
    Private Sub cmdExportALLCov2Json_Click(sender As Object, e As EventArgs) Handles cmdExportALLCov2Json.Click
        'Call _ExportALLCov2Json()
        Call _exportQR_json()
    End Sub
    Private Sub cmdExportALLCov2CSV_Click(sender As Object, e As EventArgs) Handles cmdExportALLCov2CSV.Click
        'Call _ExportALLCov2CSV()
        Call _export_B_json()
    End Sub

    Private Sub cmdGPGP_Click(sender As Object, e As EventArgs) Handles cmdGPGP.Click
        '        _computeGP()
        Call _go_PCA4DR()

    End Sub

    Private Sub cheLogScale_CheckedChanged(sender As Object, e As EventArgs) Handles cheLogScale.CheckedChanged
        _picEigenValue_Display()
    End Sub

    Private Sub cmdPicReDraw_Click(sender As Object, e As EventArgs) Handles cmdPicReDraw.Click
        _picEigenValue_Display()
    End Sub
    Private Sub cmdExportQRCSV_Click(sender As Object, e As EventArgs) Handles cmdExportQRCSV.Click
        _exportQR_CSV()
    End Sub
    Private Sub cmdExportBCSV_Click(sender As Object, e As EventArgs) Handles cmdExportBCSV.Click
        Call _export_B_csv()
    End Sub

    '===R
    '---
    '? UI end
    '---
    '---

    Private Sub assignTargetFolder_direct()
        Dim Message, Title As String
        Message = "Direct input the target folder "    ' Set prompt.
        Title = "Folder Path"    ' Set title.

        ' Display message, title, and default value.
        Dim folderpath As String = InputBox(Message, Title, "")

        If IO.Directory.Exists(folderpath) Then
            txtWorkingDir.Text = folderpath
            Form1.DealWith_Form2_Command("cd," + folderpath)
            'preOBJ.TargetFolderFullPath = folderpath
            Me.workDir = folderpath
        End If

    End Sub

    '===R
    '---B
    '!+ **File write / read / converting**
    '---B
    Private Function _read_CALA_output_return_full_path(Typ As CALA_Output_file_Type,
                                                        Optional fileDescription As String = "") As String

        Dim filter_string As String

        Select Case Typ
            Case CALA_Output_file_Type.csv
                filter_string = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
            Case CALA_Output_file_Type.json
                filter_string = "json files (*.json)|*.json|All files (*.*)|*.*"
            Case Else
                filter_string = "All files (*.*)|*.*"
        End Select


        '? open a file dialog 
        With OpenFileDialog1
            .Multiselect = False
            .CheckFileExists = True
            .ReadOnlyChecked = True
            .InitialDirectory = workDir
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
    Private Function _read_preDP_trsioCSV() As CALA_Covariance_pre_DataPurge_input

        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.csv, "Reaf trsio_ini.csv")

        Dim out As CALA_Covariance_pre_DataPurge_input =
            CALA_Covariance_Read_Trsio_file_return_preDP(strFilePath)

        If (out.matXX IsNot Nothing) And
            (out.matYY IsNot Nothing) Then
            Dim str As String = "File: " + IO.Path.GetFileName(strFilePath) + " is imported."

            Addtxt_2_txtDS_Before_FileName(IO.Path.GetFileName(strFilePath))
            Addtxt_2_txtGP_output(str)
        End If
        Return out
    End Function

    Private Function _read_preDP_trsioCSV(strFolder As String) As CALA_Covariance_pre_DataPurge_input

        'Dim strFilePath As String =
        '    _read_CALA_output_return_full_path(CALA_Output_file_Type.csv, "Reaf trsio_ini.csv")


        Dim strfilepath As String = ""

        If Strings.Right(strFolder, 1) = "\" Then
            strfilepath = strFolder + CAYMAL_const_structure.DefaultInputFile.TrainingSetI_O
        Else
            strfilepath = strFolder + "\" + CAYMAL_const_structure.DefaultInputFile.TrainingSetI_O

        End If



        Dim out As CALA_Covariance_pre_DataPurge_input =
            CALA_Covariance_Read_Trsio_file_return_preDP(strFilePath)

        If (out.matXX IsNot Nothing) And
            (out.matYY IsNot Nothing) Then
            Dim str As String = "File: " + IO.Path.GetFileName(strFilePath) + " is imported."

            Addtxt_2_txtDS_Before_FileName(IO.Path.GetFileName(strFilePath))
            Addtxt_2_txtGP_output(str)
        End If
        Return out
    End Function

    Private Sub _export_preDP_2_json()
        Call CALA_preDP_2_json(preDP, workDir)
        Dim filepath As String = CALA_convert_basedir_file(workDir,
                                                               CALA_file_name.pre_data_purge_X_and_Y + "_" + attachDateTimeSurfix(),
                                                               CALA_Output_file_Type.json)

        Dim str As String =
            "preDP has been exported @ " + IO.Path.GetFileName(filepath)
        Addtxt_2_txtGP_output(str)

    End Sub

    Private Sub _import_preDP_json_()
        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.json, "Import pre Data Purge MatXX and MatYY")

        Dim import_preDP As CALA_Covariance_pre_DataPurge_input =
            CALA_preDP_from_json(strFilePath)

        If (import_preDP.matXX IsNot Nothing) And
                (import_preDP.matYY IsNot Nothing) Then
            Me.preDP = import_preDP

            Dim str As String = "File: " + IO.Path.GetFileName(strFilePath) + " is imported."
            Addtxt_2_txtDS_Before_FileName(IO.Path.GetFileName(strFilePath))
            Addtxt_2_txtGP_output(str)
        End If

    End Sub

    Private Sub _export_preDP_matXX_YY_json()
        If (preDP.matXX IsNot Nothing) And
                (preDP.matYY IsNot Nothing) Then

            Dim str As String =
                CALA_preDP_2_matX_and_matY_json(preDP, workDir)

            str = "Files " + str + " have been exported to " + workDir

            Addtxt_2_txtGP_output(str)
        End If
    End Sub

    Private Sub _export_preDP_matXX_YY_csv()
        If (preDP.matXX IsNot Nothing) And
                (preDP.matYY IsNot Nothing) Then

            Dim str As String =
                CALA_preDP_2_matX_and_matY_csv(preDP, workDir)

            str = "Files " + str + " have been exported to " + workDir

            Addtxt_2_txtGP_output(str)
        End If
    End Sub

    Private Sub _import_preDP_matXX_json_only()
        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.json, "Import pre Data Purge MatXX json")

        If preDP.matXX Is Nothing Then preDP.matXX = New CALA_Matrix_FullDense(workDir)
        preDP.matXX.Json2Me(strFilePath)

        txtDS_Before_FileName.AppendText(IO.Path.GetFileName(strFilePath))

        Dim str As String = "PreDP added matXX: " + IO.Path.GetFileName(strFilePath) + " from " + IO.Path.GetDirectoryName(strFilePath)

        Addtxt_2_txtGP_output(str)

    End Sub

    Private Sub _import_preDP_matYY_json_only()
        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.json, "Import pre Data Purge MatYY json")

        If preDP.matYY Is Nothing Then preDP.matYY = New CALA_Matrix_FullDense(workDir)
        preDP.matYY.Json2Me(strFilePath)

        txtDS_Before_FileName.AppendText(IO.Path.GetFileName(strFilePath))

        Dim str As String = "PreDP added matYY: " + IO.Path.GetFileName(strFilePath) + " from " + IO.Path.GetDirectoryName(strFilePath)

        Addtxt_2_txtGP_output(str)

    End Sub

    Private Sub _import_preDP_matXX_csv_only()
        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.csv, "Import pre Data Purge MatXX csv")

        If preDP.matXX Is Nothing Then preDP.matXX = New CALA_Matrix_FullDense(workDir)
        preDP.matXX.CSV2Me(strFilePath)

        txtDS_Before_FileName.AppendText(IO.Path.GetFileName(strFilePath))

        Dim str As String = "PreDP added matXX: " + IO.Path.GetFileName(strFilePath) + " from " + IO.Path.GetDirectoryName(strFilePath)

        Addtxt_2_txtGP_output(str)

    End Sub

    Private Sub _import_preDP_matYY_csv_only()
        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.csv, "Import pre Data Purge MatYY csv")

        If preDP.matYY Is Nothing Then preDP.matYY = New CALA_Matrix_FullDense(workDir)
        preDP.matYY.CSV2Me(strFilePath)

        txtDS_Before_FileName.AppendText(IO.Path.GetFileName(strFilePath))

        Dim str As String = "PreDP added matXX: " + IO.Path.GetFileName(strFilePath) + " from " + IO.Path.GetDirectoryName(strFilePath)

        Addtxt_2_txtGP_output(str)

    End Sub


    '===R
    '---B
    '!+ **Object fixing before go Data Purge**
    '---B
    Private Sub _fixing_Key_objects()
        'Dim preDP As CALA_Covariance_pre_DataPurge_input
        'Dim outDP As CALA_Covariance_output_format
        'Dim kp As CALA_Kernel_Parameters
        'Dim ginp As CALA_GP_input
        'Dim gout As CALA_GP_output
        'Dim para() As parameterClass
        'Dim matXs As CALA_Matrix_FullDense


    End Sub



    '===R
    '---B
    '!+ **Parameter file handling**
    '? read , generate and write
    '---B
    Private Sub __import_Parameters_file_from_json_file(strFilePath As String)
        Dim para_json As parameterClass_json =
            read_NN_jfile_SAR(strFilePath, New parameterClass_json)


        Dim para_preRead() As parameterClass _
            = convertParameterJson2Parameters(para_json)
        If para_preRead IsNot Nothing Then
            Me.para_DP = Nothing
            Me.para_DP = para_preRead
            Call _display_Parameter_in_para_cbo()

            Dim inp_count As Integer = 0
            Dim out_count As Integer = 0
            For Each p As parameterClass In para_DP
                If p.bolInput Then
                    inp_count += 1
                End If
                If p.bolOutput Then
                    out_count += 1
                End If
            Next

            Dim s As String =
                IO.Path.GetFileName(strFilePath).ToString +
                " has been loaded. It contains " + inp_count.ToString + " input(s) and " +
                out_count.ToString + " output(s)."

            Me.para_DP_json_file_full_path = strFilePath

            Addtxt_2_txtGP_output(s)


        End If
    End Sub
    Private Sub _import_Parameters_file_from_json()
        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.json, "Import Parameter json file")
        If IO.File.Exists(strFilePath) Then
            __import_Parameters_file_from_json_file(strFilePath)
        End If

    End Sub
    Private Sub _display_Parameter_in_para_cbo()
        If Me.para_DP Is Nothing Then
            Exit Sub
        End If


    End Sub
    Private Sub _generate_parameter_from_preDP(Optional bolAskSave As Boolean = True)
        If (preDP.matXX IsNot Nothing) And (preDP.matYY IsNot Nothing) Then
            para_DP = CALA_CAYMAL_generate_parameter_file(preDP)
            Dim s As String =
                "The parameter has been re-calculated based on the preDP. " + vbNewLine +
                "This computation result is stored in the memory and it will be applied in the future. " + vbNewLine +
                "If you don't want to use them, you can override the para by re-load the para_<XXX>.json"

            Addtxt_2_txtGP_output(s)

        End If
        If bolAskSave Then
            Dim result As DialogResult = MessageBox.Show("Export the para file?",
                              "CAYMAL",
                              MessageBoxButtons.YesNo)

            If result = vbYes Then
                Dim filepath As String = CALA_convert_basedir_file(workDir,
                                                                  DefaultInputFile.pre_parameters + "_" + attachDateTimeSurfix(),
                                                                  CALA_Output_file_Type.json)

                'Dim para As parameterClass_json = convertParameterJson2Parameters()

                Dim paras As New parameterClass_json
                ReDim paras.para(Me.para_DP.Count - 1)
                For ii As Integer = 0 To Me.para_DP.Count - 1
                    paras.para(ii) = New parameterClass
                    paras.para(ii) = para_DP(ii)
                Next
                write_NN_jfile_general(paras, filepath)


                Dim s As String =
                    "Parameter file: " + IO.Path.GetFileName(filepath) +
                    " has been exported. If you want to use this para file in the program. You should re-load it again. "

                Addtxt_2_txtGP_output(s)
            End If

        End If


    End Sub
    '===R
    '---B
    '!+ **Data Purge and postDP data write/read**
    '? read , generate and write
    '---B

    Private Sub _go_Data_Purge(Optional bolGUI As Boolean = True)
        '? check Data

        Dim s As String = ""
        If bolGUI Then
            If preDP.matXX Is Nothing Then
                s = "There is no input matrix!"
                Addtxt_2_txtGP_output(s)
                MsgBox(s)
                Exit Sub
            End If
            If preDP.matXX.RowCount = 0 Or
                preDP.matXX.ColCount = 0 Then
                s = "There is no input matrix contents!"
                Addtxt_2_txtGP_output(s)
                MsgBox(s)
                Exit Sub
            End If

            If preDP.matYY Is Nothing Then
                s = "There is no output matrix!"
                Addtxt_2_txtGP_output(s)
                MsgBox(s)
                Exit Sub
            End If

            If preDP.matYY.RowCount = 0 Or
                preDP.matYY.ColCount = 0 Then
                s = "There is no output matrix contents!"
                Addtxt_2_txtGP_output(s)
                MsgBox(s)
                Exit Sub
            End If
        End If
        postDP = New CALA_Covariance_Data_Purge_Output
        postDP = CALA_Covariance_Data_Purge_Matrix(preDP)

        Dim repeated As Integer = 0
        For ii As Integer = 0 To postDP.matS.RowCount - 1
            If postDP.matS.a(ii, 0) > 1 Then
                repeated += 1
            End If
        Next


        s = "The Data Purge done." + vbNewLine +
            " It contains " + postDP.matX.ColCount.ToString + " input parameter(s), and " +
            postDP.matY.ColCount.ToString + " output parameter(s). " + vbNewLine +
            " There is " + repeated.ToString + " repeated data"

        Addtxt_2_txtGP_output(s)




    End Sub
    Private Sub _export_postDP_matX_Y_S_N_json()
        If (postDP.matX IsNot Nothing) And
                (postDP.matY IsNot Nothing) And
                (postDP.matS IsNot Nothing) Then


            Dim str As String =
                CALA_postDP_2_json(postDP, workDir)

            str = "Files " + IO.Path.GetFileName(str) + " have been exported to " + workDir

            Addtxt_2_txtGP_output(str)

        End If


    End Sub
    Private Sub _read_postDP_json()
        Dim strFilePath As String =
            _read_CALA_output_return_full_path(CALA_Output_file_Type.json, "Import post Data Purge json")

        If Not (IO.File.Exists(strFilePath)) Then Exit Sub

        Me.postDP = CALA_postDB_from_json(strFilePath)

        ' txtDS_DS_Directly_Import.AppendText(IO.Path.GetFileName(strFilePath))

        Dim str As String = "PostDP added : " + IO.Path.GetFileName(strFilePath) + " from " + IO.Path.GetDirectoryName(strFilePath)

        Addtxt_2_txtGP_output(str)
    End Sub
    Private Sub _export_postDP_matX_Y_S_csv()
        Dim s() As String = CALA_postDP_2_csv(postDP, workDir)

        Dim st As String =
            "The postDB has been exported to matX (" + IO.Path.GetFileName(s(0)) +
            "), matY(" + IO.Path.GetFileName(s(1)) +
            ") and matS(" + IO.Path.GetFileName(s(2)) + ")"

        Addtxt_2_txtGP_output(st)


    End Sub
    '===R
    '---B
    '!+ **dgv handling**
    '? generate, update to memory, save/write
    '---B


    Private Sub _dgvEigenValue_Display()
        If QR_Hess.QR_H.Q Is Nothing Then Exit Sub
        If QR_Hess.QR_H.R Is Nothing Then Exit Sub

        'clear

        'display
        dgvEigenValue.Columns.Clear() : dgvEigenValue.Rows.Clear()

        Dim ccc As Integer = 3
        Dim width As Double = dgvEigenValue.Width / (ccc) - 2
        With dgvEigenValue
            .BackgroundColor = Color.Beige
            .ColumnCount = 2
            .ColumnHeadersVisible = True
            .RowHeadersWidth = dgvEigenValue.Width * 0.05

            .Columns(0).Name = ""
            .Columns(0).Width = width / 2
            .Columns(1).Name = "EV"
            .Columns(1).Width = width * 1.5


        End With

        Dim dgchk As New DataGridViewCheckBoxColumn
        dgvEigenValue.Columns.Insert(2, dgchk)
        dgvEigenValue.Columns(2).Name = "O?"
        dgvEigenValue.Columns(2).Width = width - 2
        'set all to false


        For ev As Integer = 0 To QR_Hess.EigenValue.Count - 1
            dgvEigenValue.Rows.Add()
            With dgvEigenValue.Rows(ev)
                .Cells(0).Value = ev.ToString
                .Cells(1).Value = QR_Hess.EigenValue(ev).ToString("##.####")
                .Cells(2).Value = True
            End With
        Next

    End Sub


    Private Sub _dgvEigenVector_Display()
        If QR_Hess.QR_H.Q Is Nothing Then Exit Sub
        If QR_Hess.QR_H.R Is Nothing Then Exit Sub

        'clear

        'display
        dgvEigenVector.Columns.Clear() : dgvEigenVector.Rows.Clear()

        Dim ccc As Integer = QR_Hess.EigenValue.Count
        Dim width As Double = dgvEigenVector.Width / (ccc) - 2
        With dgvEigenVector

            .BackgroundColor = Color.Beige
            .ColumnCount = ccc
            .ColumnHeadersVisible = False
            .RowHeadersWidth = dgvEigenVector.Width * 0.01


            For ii As Integer = 0 To ccc - 1
                .Columns(ii).Name = ""
                .Columns(ii).Width = width

            Next
        End With

        For ii As Integer = 0 To QR_Hess.QR_H.Q.RowCount - 1
            'For ii As Integer = 0 To 0
            dgvEigenVector.Rows.Add()
            With dgvEigenVector.Rows(ii)
                For jj As Integer = 0 To QR_Hess.QR_H.Q.ColCount - 1

                    'Dim o As String = QR_Hess.QR_H.Q.a(ii, jj).ToString("##.######")
                    Dim value As Double = QR_Hess.QR_H.Q.a(ii, jj)
                    If Math.Abs(value) > CALA.CALA_constants.small_number Then
                        .Cells(jj).Value = value.ToString
                    Else
                        .Cells(jj).Value = "0"
                    End If

                Next
            End With

        Next


    End Sub

    '===R
    '---B
    '!+ **CovK handling**
    '? compute, optimization, para handling
    '---B

    Private Sub _generate_pre_covK()
        Dim ss As String = ""
        '? forming true Xs
        ' named _mat_preXs
        ' mat_pre_GP_Xs = CALA_Covariance_pre_GP_Xs_forming(matXs, CInt(txtSplitsetting.Text))
        ss = "mat_pre_GP_Xs  is formed, size of " + mat_pre_GP_Xs.RowCount.ToString + " with " + mat_pre_GP_Xs.RowCount.ToString + " parameters."
        Addtxt_2_txtGP_output(ss)



        ss = "The figure updated"
        Addtxt_2_txtGP_output(ss)



        '? check and do normzlized 
        '   if not, still generate new matrix and para_GP

        '? anyway , i will generate a new **para_GP**
        '? the difference is that, I generate it through 

        'If chkNormalized_before_GP.Checked Then
        '    para_GP = _generate_para_GP_from_post_GP()
        'Else
        '    para_GP = _generate_NO_CHANGE_para_GP()
        'End If



        'modify to a new post_GP
        'modify to a new mat_preGP_Xs
        '_modify_postGP_and_mat_pre_GP_Xs()


        ss = "The post_GP and mat_pre_GP_Xs has been updated by new para_GP. "
        Addtxt_2_txtGP_output(ss)
    End Sub


    Private Sub _generate_covK_all()

        '? forming Kxx, Kxs and Kss
        Call _get_kp_from_UI()

        '? forming K

        Dim ktype As CALA_Kernel_Type = CType(cboKernelType.SelectedIndex, CALA_Kernel_Type)

        CovK = New CALA_Cov("Kxx", ktype, kp, workDir)
        ' CovK.CovK_Generation_Exp_Kxx(postDP.matX, postDP.matX, postDP.matS, cboYparaSelect.SelectedIndex, epsilon)

        CovKs = New CALA_Cov("Kxs", ktype, kp, workDir)
        CovKs.CovK_Generation_Exp(postDP.matX, mat_pre_GP_Xs)

        CovKss = New CALA_Cov("Kss", ktype, kp, workDir)
        CovKss.CovK_Generation_Exp(mat_pre_GP_Xs, mat_pre_GP_Xs)

        Dim ss As String = " The Cov matrix has been formed. " + vbNewLine +
            "CovK (" + CovK.K.RowCount.ToString + "," + CovK.K.ColCount.ToString + "), " +
            "CovKs (" + CovKs.K.RowCount.ToString + "," + CovKs.K.ColCount.ToString + "), " +
            "CovKss (" + CovKss.K.RowCount.ToString + "," + CovKss.K.ColCount.ToString + ")" + vbNewLine +
            "Kernel Type=" + ktype.ToString

        Addtxt_2_txtGP_output(ss)



    End Sub

    Private Function _generate_para_GP_from_post_GP() As parameterClass()
        'If para_DP Is Nothing Then
        '    _generate_parameter_from_preDP(False)
        'End If

        Dim para_GPP() As parameterClass
        'ReDim para_GPP(para_DP.Count - 1)

        'Dim inp_c As Integer = 0 : Dim out_c As Integer = 0 ' input and output parameter counter
        'Dim cc As Integer = 0
        'For Each p As parameterClass In para_DP
        '    If p.bolInput Then
        '        para_GPP(cc) = New parameterClass
        '        para_GPP(cc) = p
        '        With para_GPP(cc)
        '            .Norm_max = CDbl(txtGP_Normal_Max.Text)
        '            .Norm_min = CDbl(txtGP_Normal_Min.Text)
        '            .Norm_Real_max = postDP.matX.col_max(inp_c)
        '            .Norm_Real_min = postDP.matX.col_min(inp_c)
        '            inp_c += 1

        '        End With
        '    End If

        '    If p.bolOutput Then
        '        para_GPP(cc) = New parameterClass
        '        para_GPP(cc) = p
        '        With para_GPP(cc)
        '            .Norm_max = CDbl(txtGP_Normal_Max.Text)
        '            .Norm_min = CDbl(txtGP_Normal_Min.Text)
        '            .Norm_Real_max = postDP.matY.col_max(out_c)
        '            .Norm_Real_min = postDP.matY.col_min(out_c)
        '            out_c += 1

        '        End With

        '    End If
        '    cc += 1
        'Next
        Return para_GPP
    End Function

    Private Function _generate_NO_CHANGE_para_GP() As parameterClass()
        If para_DP Is Nothing Then
            _generate_parameter_from_preDP(False)
        End If

        Dim para_GPP() As parameterClass
        ReDim para_GPP(para_DP.Count - 1)

        Dim cc As Integer = 0
        For Each p As parameterClass In para_DP
            para_GPP(cc) = New parameterClass
            para_GPP(cc) = p
            With para_GPP(cc)
                .Norm_max = 1
                .Norm_min = 0
                .Norm_Real_max = 1
                .Norm_Real_min = 0
                cc += 1
            End With
        Next
        Return para_GPP
    End Function



    Private Sub _get_kp_from_UI()
        With kp
            .P1 = CDbl(txtP1setting.Text)
            .P2 = CDbl(txtP2setting.Text)
            .P3 = CDbl(txtP3setting.Text)
        End With
        epsilon = CDbl(txtEpsilonsetting.Text)
    End Sub



    Private Sub _exportQR_json()
        '+ This is **QR json**

        Dim filQ As String = CALA_file_name.PCA4DR_Q + "_" + attachDateTimeSurfix() + ".json"
        Dim filR As String = CALA_file_name.PCA4DR_R + "_" + attachDateTimeSurfix() + ".json"

        QR_Hess.QR_H.Q.Me2Json(filQ)
        QR_Hess.QR_H.R.Me2Json(filR)

        Dim ss As String
        ss = "Q has been export to " + filQ + vbNewLine +
                "r has been export to " + filR
        Addtxt_2_txtGP_output(ss)
    End Sub

    Private Sub _exportQR_CSV()
        '+ This is **QR CSV**
        Dim filQ As String = CALA_file_name.PCA4DR_Q + "_" + attachDateTimeSurfix() + ".csv"
        Dim filR As String = CALA_file_name.PCA4DR_R + "_" + attachDateTimeSurfix() + ".csv"

        QR_Hess.QR_H.Q.Me2CSV(filQ)
        QR_Hess.QR_H.R.Me2CSV(filR)

        Dim ss As String
        ss = "Q has been export to " + filQ + vbNewLine +
                "r has been export to " + filR
        Addtxt_2_txtGP_output(ss)
    End Sub

    Private Sub _export_B_csv()
        Dim fil As String = CALA_file_name.PCA4DR_B + "_" + attachDateTimeSurfix() + ".csv"
        Dim B As CALA_Matrix_FullDense = __get_output_B_matrix()
        B.Me2CSV(fil)
        Dim s As String = "B export to " + fil
        Addtxt_2_txtGP_output(s)
    End Sub
    Private Sub _export_B_json()
        Dim fil As String = CALA_file_name.PCA4DR_B + "_" + attachDateTimeSurfix() + ".json"
        Dim B As CALA_Matrix_FullDense = __get_output_B_matrix()
        B.Me2Json(fil)
        Dim s As String = "B export to " + fil
        Addtxt_2_txtGP_output(s)
    End Sub

    Private Function __get_output_B_matrix() As CALA_Matrix_FullDense
        Dim lstW As New List(Of Integer)
        For ii As Integer = 0 To QR_Hess.EigenValue.Count - 1
            If dgvEigenValue.Rows(ii).Cells(2).Value Then
                lstW.Add(ii)
            End If
        Next

        Dim w() As Integer : ReDim w(lstW.Count - 1)
        For ii As Integer = 0 To lstW.Count - 1
            w(ii) = lstW(ii)
        Next

        Dim B_out As New CALA_Matrix_FullDense("B", QR_Hess.EigenValue.Count, w.Count, workDir)

        For jj As Integer = 0 To w.Count - 1
            Dim inp() As Double = QR_Hess.QR_H.Q.col_vec(jj)
            For ii As Integer = 0 To QR_Hess.EigenValue.Count - 1
                B_out.a(ii, jj) = inp(ii)
            Next
        Next

        Return B_out
    End Function




    '===R


    '===R
    '---B
    '!+ **CovK opt**
    '? compute, optimization, para handling
    '---B


    '===R
    '---B
    '!+ ** display**
    '? compute, optimization, para handling







    Private Sub cboYparaSelect_SelectedIndexChanged(sender As Object, e As EventArgs)
        'If cboYparaSelect_prev_index <> cboYparaSelect.SelectedIndex Then
        '    Call _Undo_postGP_and_mat_pre_GP_Xs()
        '    cboYparaSelect_prev_index = cboYparaSelect.SelectedIndex
        'End If

    End Sub

    Private Sub _picEigenValue_Display()
        picEigenVector.Image = Nothing

        'picEigenVector.BackColor = Color.Transparent

        Dim d_w As Single = picEigenVector.Width / ((QR_Hess.EigenValue.Count) * 2)
        Dim d_h As Single = picEigenVector.Height / ((QR_Hess.EigenValue.Count) * 2)
        Dim rad As Single = Math.Min(d_w, d_h) * 0.9
        Dim a_value As Integer = CInt(txtA_valuesetting.Text)
        'draw a circle

        Dim p As New System.Drawing.Pen(Color.Black, 1)
        Dim br As New System.Drawing.SolidBrush(Color.Red)
        Dim co As System.Drawing.Color

        Dim bmp As New Bitmap(picEigenVector.Width, picEigenVector.Height)
        bmp.MakeTransparent()

        Dim g As System.Drawing.Graphics

        g = Graphics.FromImage(bmp)
        g.Clear(Color.Transparent)

        Dim rect As System.Drawing.Rectangle
        Dim ii As Integer = 0
        Dim jj As Integer = 0

        Dim max As Double
        Dim min As Double
        Dim diff As Double

        If cheLogScale.Checked Then
            max = QR_Hess.QR_H.Q.me_abs_max
            min = QR_Hess.QR_H.Q.me_abs_min
            diff = Math.Log(max) - Math.Log(min)
        Else
            max = QR_Hess.QR_H.Q.me_max
            min = QR_Hess.QR_H.Q.me_min
            diff = max - min
        End If

        For ii = 0 To QR_Hess.EigenValue.Count - 1
            For jj = 0 To QR_Hess.EigenValue.Count - 1
                'For jj = 0 To 1
                Dim locX As Single = 0.5 * d_w + 2 * d_w * jj
                Dim locY As Single = 0.5 * d_h + 2 * d_h * ii
                rect = New System.Drawing.Rectangle(locX, locY, rad, rad)

                g.DrawEllipse(p, rect)

                Dim value As Double = QR_Hess.QR_H.Q.a(ii, jj)
                Debug.Print("a(" + ii.ToString + "," + jj.ToString + ")=" + value.ToString)

                If cheLogScale.Checked Then


                    If Math.Abs(Math.Log(value)) > CALA.CALA_constants.small_number Then
                        If Math.Log(value) > 0 Then
                            'blue 
                            Dim blue
                            If chkInvDisplay.Checked Then
                                blue = 255 - (Math.Log(value) - Math.Log(min)) / diff * 255
                            Else
                                blue = (Math.Log(value) - Math.Log(min)) / diff * 255
                            End If

                            co = Color.FromArgb(a_value, 10, 10, blue)
                        Else
                            value = Math.Abs(value)
                            Dim red
                            If chkInvDisplay.Checked Then
                                red = 255 - ((Math.Log(value)) - Math.Log(min)) / diff * 255
                            Else
                                red = ((Math.Log(value)) - Math.Log(min)) / diff * 255
                            End If
                            co = Color.FromArgb(a_value, red, 10, 10)
                        End If
                        br = New System.Drawing.SolidBrush(co)
                        g.FillEllipse(br, rect)
                    End If
                Else


                    If Math.Abs(value) > CALA.CALA_constants.small_number Then
                        If value > 0 Then
                            'blue 
                            Dim green
                            If chkInvDisplay.Checked Then
                                green = 255 - (value - min) / diff * 255
                            Else
                                green = (value - min) / diff * 255
                            End If

                            co = Color.FromArgb(a_value, 0, green, 0)
                            Debug.Print("green:" + green.ToString)
                        Else
                            Dim red
                            If chkInvDisplay.Checked Then
                                red = 255 - (Math.Abs(value) - min) / diff * 255
                            Else
                                red = (Math.Abs(value) - min) / diff * 255
                            End If


                            co = Color.FromArgb(a_value, red, 0, 0)
                            Debug.Print("red:" + red.ToString)
                        End If
                        br = New System.Drawing.SolidBrush(co)
                        g.FillEllipse(br, rect)
                    End If
                End If

            Next
        Next



        picEigenVector.Image = bmp


    End Sub

    '===R
    '---B
    '!+ **PCA4DR**
    '? compute, optimization, para handling
    '---B

    Private Sub _go_PCA4DR()

        Dim ss As String = ""

        '? check postDP ready 

        If postDP.matX Is Nothing Then
            ss = "[Error] There is no postDP, load the data and execute the DataPurge."
            Addtxt_2_txtGP_output(ss)
            Exit Sub
        End If


        ' I don't check para, because it can be no use of para

        ''? check para_dp is ready
        'If para_DP Is Nothing Then
        '    ss = "[Error] There is no para file, load para file or generate a new one."
        '    Addtxt_2_txtGP_output(ss)
        '    Exit Sub
        'End If
        'If para_DP(0) Is Nothing Then
        '    ss = "[Error] There is no para file, load para file or generate a new one."
        '    Addtxt_2_txtGP_output(ss)
        '    Exit Sub
        'End If
        '? generate covK (col)

        '? forming Kxx, Kxs and Kss
        Call _get_kp_from_UI()

        '? forming K

        Dim ktype As CALA_Kernel_Type = CType(cboKernelType.SelectedIndex, CALA_Kernel_Type)

        CovK = New CALA_Cov("Kxx", ktype, kp, workDir)
        CovK.CovK_Generation_Para_PCA4DR(postDP.matX, epsilon)



        ss = " The Cov matrix has been formed. " + vbNewLine +
            "CovK (" + CovK.K.RowCount.ToString + "," + CovK.K.ColCount.ToString + "), " +
            "Kernel Type=" + ktype.ToString

        Addtxt_2_txtGP_output(ss)

        ' PCA computation
        '

        QR_Hess = CALA.CALA_Lanczos_Hessenberg_QR(CovK.K,, 0)

        ss = " Lanczos Hessenbert QR done " + vbNewLine +
            "QR Iter:  " + QR_Hess.Iteration.ToString + ", " +
            "QR L2Norm:  " + QR_Hess.L2Norm.ToString("0.0#e+00")

        Addtxt_2_txtGP_output(ss)

        '===B

        '?++  Display!

        _dgvEigenValue_Display()
        _dgvEigenVector_Display()
        _picEigenValue_Display()


    End Sub





    Private Sub chartGP_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        'Dim ms As New IO.MemoryStream()
        'chartGP.SaveImage(ms, DataVisualization.Charting.ChartImageFormat.Bmp)
        'Dim bm As Bitmap = New Bitmap(ms)
        'Clipboard.SetImage(bm)

        'MsgBox("image copied to clipboard")
    End Sub

    Private Sub cmdDebug_Click(sender As Object, e As EventArgs) Handles cmdDebug.Click
        Dim fil1 = workDir + "\" + "postDP_20210516084754.json"
        Dim fil2 = workDir + "\" + "para_20210516084540.json"


        Me.postDP = CALA_postDB_from_json(fil1)

        ' txtDS_DS_Directly_Import.AppendText(IO.Path.GetFileName(strFilePath))

        Dim str As String = "PostDP added : " + IO.Path.GetFileName(fil1) + " from " + IO.Path.GetDirectoryName(fil1)

        Addtxt_2_txtGP_output(str)

        __import_Parameters_file_from_json_file(fil2)

        _go_PCA4DR()


    End Sub



    Private Sub picEigenVector_DoubleClick(sender As Object, e As EventArgs) Handles picEigenVector.DoubleClick
        'Dim ms As New IO.MemoryStream()
        'picEigenVector.Image
        'chartGP.SaveImage(ms, DataVisualization.Charting.ChartImageFormat.Bmp)
        'Dim bm As Bitmap = New Bitmap(ms)

        'Dim bmp As New Bitmap(picEigenVector.Width, picEigenVector.Height)
        'Dim g As Graphics = Graphics.FromImage(bmp)




        Clipboard.SetImage(picEigenVector.Image)

        MsgBox("image copied to clipboard")
    End Sub




    '===R
    '===R
    '===R

    '!++ External conrol sub

    '===R





#Region "External Control Sub"

    Public Function external_CALL_ForPCA_FolderWithTRSIO_Return_Q_hess(strBasFoler As String) As Object
        Debug.Print(strBasFoler)


        Me.preDP = _read_preDP_trsioCSV(strBasFoler)

        Call _go_Data_Purge(False)


        Call _go_PCA4DR()





        'Stop



        Return QR_Hess

    End Function






#End Region




End Class


