Imports MathNet.Numerics.LinearAlgebra.Double
Imports CAYMAL.CALA



Public Class Form15_CALA_GP

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
        '?+ **For <txtDS_DS_Directly_Import>**
        If Me.InvokeRequired Then
            Dim d As New txt_Delegate(AddressOf Addtxt_2_txtDS_Directly_Import)
            Me.Invoke(d, New Object() {txt})
        Else
            txtDS_DS_Directly_Import.Text = (txt)
        End If
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
        txtDS_DS_Directly_Import.Text = ""
        txtGP_output.Text = ""



        '? default constants

        txtExtrasetting.Text = "0.0"
        txtRangeMaxsetting.Text = "1.0"
        txtRangeMinsetting.Text = "0.0"
        txtSplitsetting.Text = "100"

        txtP1setting.Text = "1"
        txtP2setting.Text = "1"
        txtP3setting.Text = "1"

        txtPreDP_paraMax.Text = "0.9"
        txtPreDP_paraMin.Text = "0.1"

        txtGP_Normal_Max.Text = "0.9"
        txtGP_Normal_Min.Text = "-0.9"
        txtEpsilonsetting.Text = "1e-4"


        txtCovKOpt_LR.Text = "0.1"
        txtCovKOpt_MaxIter.Text = "250"
        txtCovKOpt_DeltaP1.Text = "1e-3"

        '? add combo: Kernel type
        cboKernelType.Items.Clear()
        Dim items As Array
        items = System.Enum.GetNames(GetType(CALA_Kernel_Type))
        For Each af As String In items
            cboKernelType.Items.Add(af)
        Next
        cboKernelType.SelectedIndex = 0


        'clear combo
        cboYparaSelect.Items.Clear() : cboYparaSelect.Text = ""
        cboYparaSelect.Items.Add("0")
        cboYparaSelect.SelectedIndex = 0


        cboSigmaOption.Items.Clear()
        cboSigmaOption.Text = ""
        cboSigmaOption.Items.Add("Power2")
        cboSigmaOption.Items.Add("no action")
        cboSigmaOption.Items.Add("square 2")
        cboSigmaOption.SelectedIndex = 0


        cboYparaSelect_prev_index = 0

        grpNoShow.Visible = False


        '? objects 
        preDP.matXX = New CALA_Matrix_FullDense(workDir)
        preDP.matYY = New CALA_Matrix_FullDense(workDir)


        '? clean para cls
        ReDim para_DP(0)
        ReDim para_GP(0)



        '? chart initialize
        _initial_chartGP()


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
    Private Sub cmd_Export_preDP_matXX_YY_json_Click(sender As Object, e As EventArgs) Handles cmd_Export_preDP_matXX_YY_json.Click
        Call _export_preDP_matXX_YY_json()
    End Sub
    Private Sub cmd_Export_preDP_matXX_YY_csv_Click(sender As Object, e As EventArgs) Handles cmd_Export_preDP_matXX_YY_csv.Click
        Call _export_preDP_matXX_YY_csv()
    End Sub
    Private Sub cmdReadMatXX_and_go_DP_Click(sender As Object, e As EventArgs) Handles cmdReadMatXX_and_go_DP.Click
        '? only read matXX 
        Call _import_preDP_matXX_json_only()
    End Sub
    Private Sub cmdReadMatYY_and_go_DP_Click(sender As Object, e As EventArgs) Handles cmdReadMatYY_and_go_DP.Click
        '? only read matYY
        Call _import_preDP_matYY_json_only()
    End Sub
    Private Sub cmdReadMatXX_csv_and_go_DP_Click(sender As Object, e As EventArgs) Handles cmdReadMatXX_csv_and_go_DP.Click
        Call _import_preDP_matXX_csv_only()
    End Sub
    Private Sub cmdReadMatYY_csv_and_go_DP_Click(sender As Object, e As EventArgs) Handles cmdReadMatYY_csv_and_go_DP.Click
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
    Private Sub cmd_Export_postDP_matX_Y_S_csv_Click(sender As Object, e As EventArgs) Handles cmd_Export_postDP_matX_Y_S_csv.Click
        Call _export_postDP_matX_Y_S_csv()
    End Sub
    Private Sub cmdLoadXs_Click(sender As Object, e As EventArgs) Handles cmdLoadXs.Click
        Call _LoadXs_2_dgvXs()
    End Sub
    Private Sub cmdSaveXs_CSV_Click(sender As Object, e As EventArgs) Handles cmdSaveXs_CSV.Click
        Call _SaveXs_CSV()
    End Sub
    Private Sub cmdConfirmXs_Click(sender As Object, e As EventArgs) Handles cmdConfirmXs.Click
        _confirmXs()
    End Sub
    Private Sub cmdReadXs_CSV_Click(sender As Object, e As EventArgs) Handles cmdReadXs_CSV.Click
        Call _ReadXs_CSV()
    End Sub
    Private Sub cmdGenCovK_Click(sender As Object, e As EventArgs) Handles cmdGenCovK.Click
        Call _generate_covK_all()
    End Sub
    Private Sub cmdUndo_paraGP_Click(sender As Object, e As EventArgs) Handles cmdUndo_paraGP.Click
        _Undo_postGP_and_mat_pre_GP_Xs()
    End Sub
    Private Sub cmdExportpara_GP_Click(sender As Object, e As EventArgs) Handles cmdExportpara_GP.Click
        _export_para_GP()
    End Sub
    Private Sub cmdGenPreCovK_Click(sender As Object, e As EventArgs) Handles cmdGenPreCovK.Click
        _generate_pre_covK()
    End Sub
    Private Sub cmdExportALLCov2Json_Click(sender As Object, e As EventArgs) Handles cmdExportALLCov2Json.Click
        Call _ExportALLCov2Json()
    End Sub
    Private Sub cmdExportALLCov2CSV_Click(sender As Object, e As EventArgs) Handles cmdExportALLCov2CSV.Click
        Call _ExportALLCov2CSV()
    End Sub
    Private Sub cmdKernel_Parameter_go_optimizer_Click(sender As Object, e As EventArgs) Handles cmdKernel_Parameter_go_optimizer.Click
        Call _Kernel_Parameter_go_optimizer()
    End Sub
    Private Sub cmdGPGP_Click(sender As Object, e As EventArgs) Handles cmdGPGP.Click
        _computeGP()
    End Sub
    Private Sub cmdGoGP_result_Click(sender As Object, e As EventArgs) Handles cmdGoGP_result.Click
        Call _GoGP_result_CSV()
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
                If p IsNot Nothing Then
                    If p.bolInput Then
                    inp_count += 1
                End If
                    If p.bolOutput Then
                        out_count += 1
                    End If
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

        cboYparaSelect.Items.Clear()
        For ii As Integer = 0 To para_DP.Count - 1
            If para_DP(ii).bolOutput Then
                cboYparaSelect.Items.Add(para_DP(ii).ParaName)
            End If
        Next
        cboYparaSelect.SelectedIndex = 0


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

    Private Sub _go_Data_Purge()
        '? check Data

        Dim s As String = ""

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

        txtDS_DS_Directly_Import.AppendText(IO.Path.GetFileName(strFilePath))

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
    '!+ **dgvXs handling**
    '? generate, update to memory, save/write
    '---B
    Private Function __initial_dgvXs_by_pre_DP_para() As Boolean
        If para_DP Is Nothing Then
            Dim str As String = "[Error] Lack of para_DP"
            Addtxt_2_txtGP_output(str)
            Return False
        End If

        Dim ccc As Integer = 0
        For Each p As parameterClass In para_DP
            If p IsNot Nothing Then
                If p.bolInput Then
                    ccc += 1
                End If
            End If
        Next

        dgvXs.Columns.Clear() : dgvXs.Rows.Clear()


        Dim width As Double = dgvXs.Width / (ccc + 2)
        With dgvXs
            .BackgroundColor = Color.Beige
            .ColumnCount = postDP.matX.ColCount
            .ColumnHeadersVisible = True
            .RowHeadersWidth = dgvXs.Width * 0.05
        End With

        Dim cc As Integer = 0
        For Each p As parameterClass In para_DP
            If p IsNot Nothing Then
                If p.bolInput Then
                    dgvXs.Columns(cc).Name = p.ParaName
                    dgvXs.Columns(cc).Width = width

                    cc += 1
                    If cc >= dgvXs.Columns.Count - 1 Then
                        GoTo label_for_each_p
                    End If
                End If
            End If
        Next

label_for_each_p:

        Return True
    End Function

    Private Sub _LoadXs_2_dgvXs()
        If postDP.matX Is Nothing Then
            Dim str As String = "[Error] Lack of post DP"
            Addtxt_2_txtGP_output(str)
            Exit Sub
        End If
        If para_DP Is Nothing Then
            Dim str As String = "[Error] Lack of para_DP"
            Addtxt_2_txtGP_output(str)
            Exit Sub
        End If


        If Not (__initial_dgvXs_by_pre_DP_para()) Then
            Exit Sub
        End If

        Dim ww() As String : ReDim ww(dgvXs.Columns.Count - 1)
        For ii As Integer = 0 To dgvXs.Columns.Count - 1
            ww(ii) = "0"
        Next
        dgvXs.Rows.Add(ww)

        For ii As Integer = 0 To dgvXs.Columns.Count - 1
            ww(ii) = "1"
        Next
        dgvXs.Rows.Add(ww)

        dgvXs.Update()


        'save to memory matXs
        _update_Xs_matrix_from_dgvX()

        'update to txtout
        Dim ss As String = "dgvXs is generated. Pre-Data has been loaded."
        Addtxt_2_txtGP_output(ss)


    End Sub
    Private Sub _update_Xs_matrix_from_dgvX()
        If dgvXs.Rows.Count = 0 And dgvXs.ColumnCount = 0 Then
            Exit Sub
        End If

        If dgvXs.Rows(dgvXs.Rows.Count - 1).Cells(0).Value Is Nothing Then
            matXs = New CALA_Matrix_FullDense("matXs",
                                          dgvXs.Rows.Count - 1,
                                          dgvXs.Columns.Count,
                                          workDir)
        Else
            matXs = New CALA_Matrix_FullDense("matXs",
                                          dgvXs.Rows.Count,
                                          dgvXs.Columns.Count,
                                          workDir)
        End If


        For ii As Integer = 0 To dgvXs.Rows.Count - 1
            For jj As Integer = 0 To dgvXs.Columns.Count - 1
                If dgvXs.Rows(ii).Cells(jj).Value IsNot Nothing Then
                    Dim ss As String = Trim(dgvXs.Rows(ii).Cells(jj).Value.ToString)
                    If IsNumeric(ss) Then
                        matXs.a(ii, jj) = CDbl(ss)
                    Else
                        matXs.a(ii, jj) = 0
                    End If
                End If
            Next
        Next




    End Sub
    Private Sub _SaveXs_CSV()

        If matXs Is Nothing Then
            Dim s As String = "[Error] The matXs is empty."
            Addtxt_2_txtGP_output(s)
            Exit Sub
        End If

        _update_Xs_matrix_from_dgvX()

        Dim filename As String = CALA_convert_basedir_file(workDir,
                                            CALA_file_name.pre_GP_Xs_CSV + "_" + attachDateTimeSurfix(),
                                            CALA_Output_file_Type.csv)

        matXs.Me2CSV(filename)

        Dim ss As String = "The pre GP Xs has been exported to the file" + IO.Path.GetFileName(filename)
        Addtxt_2_txtGP_output(ss)

    End Sub
    Private Sub _confirmXs()
        _update_Xs_matrix_from_dgvX()
        Dim ss As String = "The dgvXs has been updated into memory."
        Addtxt_2_txtGP_output(ss)
    End Sub
    Private Sub _readXs_CSV_file(strFilePath As String)
        ' initial dgvXs
        If Not (__initial_dgvXs_by_pre_DP_para()) Then
            Exit Sub
        End If


        matXs = New CALA_Matrix_FullDense(workDir)
        matXs.CSV2Me(IO.Path.GetFileName(strFilePath))

        'put the xs into dgv
        For ii As Integer = 0 To matXs.RowCount - 1
            dgvXs.Rows.Add()

            For jj As Integer = 0 To dgvXs.Columns.Count - 1
                dgvXs.Rows(ii).Cells(jj).Value = matXs.a(ii, jj).ToString
            Next
        Next

        'show message
        Dim ss As String = "Pre GP CSV File: " + IO.Path.GetFileName(strFilePath) + " has been loaded and displayed in the dgvXs."
        Addtxt_2_txtGP_output(ss)
    End Sub
    Private Sub _ReadXs_CSV()




        ' read matXs
        Dim strFilePath As String =
           _read_CALA_output_return_full_path(CALA_Output_file_Type.csv, "Import pre GP matXs csv")

        If Not (IO.File.Exists(strFilePath)) Then
            Exit Sub
        End If

        _readXs_CSV_file(strFilePath)
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
        mat_pre_GP_Xs = CALA_Covariance_pre_GP_Xs_forming(matXs, CInt(txtSplitsetting.Text))
        ss = "mat_pre_GP_Xs  is formed, size of " + mat_pre_GP_Xs.RowCount.ToString + " with " + mat_pre_GP_Xs.RowCount.ToString + " parameters."
        Addtxt_2_txtGP_output(ss)


        Call _plot_chartGP_after_preCovK()
        ss = "The figure updated"
        Addtxt_2_txtGP_output(ss)



        '? check and do normzlized 
        '   if not, still generate new matrix and para_GP

        '? anyway , i will generate a new **para_GP**
        '? the difference is that, I generate it through 

        If chkNormalized_before_GP.Checked Then
            para_GP = _generate_para_GP_from_post_GP()
        Else
            para_GP = _generate_NO_CHANGE_para_GP()
        End If



        'modify to a new post_GP
        'modify to a new mat_preGP_Xs
        _modify_postGP_and_mat_pre_GP_Xs()


        ss = "The post_GP and mat_pre_GP_Xs has been updated by new para_GP. "
        Addtxt_2_txtGP_output(ss)
    End Sub


    Private Sub _generate_covK_all()

        '? forming Kxx, Kxs and Kss
        Call _get_kp_from_UI()

        '? forming K

        Dim ktype As CALA_Kernel_Type = CType(cboKernelType.SelectedIndex, CALA_Kernel_Type)

        CovK = New CALA_Cov("Kxx", ktype, kp, workDir)
        Dim sigmaoption As Integer = cboSigmaOption.SelectedIndex
        CovK.CovK_Generation_Exp_Kxx(postDP.matX, postDP.matX, postDP.matS, cboYparaSelect.SelectedIndex, epsilon, sigmaoption)

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
        If para_DP Is Nothing Then
            _generate_parameter_from_preDP(False)
        End If

        Dim para_GPP() As parameterClass
        ReDim para_GPP(para_DP.Count - 1)

        Dim inp_c As Integer = 0 : Dim out_c As Integer = 0 ' input and output parameter counter
        Dim cc As Integer = 0
        For Each p As parameterClass In para_DP
            If p IsNot Nothing Then
                If p.bolInput Then
                para_GPP(cc) = New parameterClass
                para_GPP(cc) = p
                    With para_GPP(cc)
                        .Norm_max = CDbl(txtGP_Normal_Max.Text)
                        .Norm_min = CDbl(txtGP_Normal_Min.Text)
                        .Norm_Real_max = postDP.matX.col_max(inp_c)
                        .Norm_Real_min = postDP.matX.col_min(inp_c)
                        inp_c += 1

                    End With
                End If
            End If

            If p.bolOutput Then
                para_GPP(cc) = New parameterClass
                para_GPP(cc) = p
                With para_GPP(cc)
                    .Norm_max = CDbl(txtGP_Normal_Max.Text)
                    .Norm_min = CDbl(txtGP_Normal_Min.Text)
                    .Norm_Real_max = postDP.matY.col_max(out_c)
                    .Norm_Real_min = postDP.matY.col_min(out_c)
                    out_c += 1

                End With

            End If
            cc += 1
        Next
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
            If p IsNot Nothing Then
                para_GPP(cc) = New parameterClass
            para_GPP(cc) = p
                With para_GPP(cc)
                    .Norm_max = 1
                    .Norm_min = 0
                    .Norm_Real_max = 1
                    .Norm_Real_min = 0
                    cc += 1
                End With
            End If
        Next
        Return para_GPP
    End Function


    Private Sub _modify_postGP_and_mat_pre_GP_Xs()
        Dim inp_c As Integer = 0 : Dim out_c As Integer = 0 : Dim cc As Integer = 0
        'matX
        For Each p As parameterClass In para_GP
            If p IsNot Nothing Then
                If p.bolInput Then
                    For exp As Integer = 0 To postDP.matX.RowCount - 1
                        postDP.matX.a(exp, inp_c) = convert_real_to_norm(p, CStr(postDP.matX.a(exp, inp_c)))
                    Next
                    inp_c += 1
                    cc += 1
                End If
            End If


        Next
        inp_c = 0 : cc = 0 : out_c = 0
        'matXs
        For Each p As parameterClass In para_GP
            If p IsNot Nothing Then


                If p.bolInput Then
                    For exp As Integer = 0 To mat_pre_GP_Xs.RowCount - 1
                        mat_pre_GP_Xs.a(exp, inp_c) = convert_real_to_norm(p, CStr(mat_pre_GP_Xs.a(exp, inp_c)))
                    Next
                    inp_c += 1
                    cc += 1
                End If
            End If
        Next
        inp_c = 0 : cc = 0 : out_c = 0
        'matY
        For Each p As parameterClass In para_GP
            If p IsNot Nothing Then

                If p.bolOutput Then
                    For exp As Integer = 0 To postDP.matY.RowCount - 1
                        postDP.matY.a(exp, out_c) = convert_real_to_norm(p, CStr(postDP.matY.a(exp, out_c)))
                        postDP.matS.a(exp, out_c) = convert_real_to_norm_noaddMin(p, CStr(postDP.matS.a(exp, out_c)))
                    Next
                    out_c += 1
                    cc += 1
                End If
            End If
        Next
    End Sub

    Private Sub _Undo_postGP_and_mat_pre_GP_Xs()
        If para_GP Is Nothing Then Exit Sub
        If para_GP(0) Is Nothing Then Exit Sub


        Dim inp_c As Integer = 0 : Dim out_c As Integer = 0 : Dim cc As Integer = 0
        'matX
        For Each p As parameterClass In para_GP
            If p IsNot Nothing Then


                If p.bolInput Then
                    For exp As Integer = 0 To postDP.matX.RowCount - 1
                        postDP.matX.a(exp, inp_c) = convert_norm_to_real(p, CStr(postDP.matX.a(exp, inp_c)))
                    Next
                    inp_c += 1
                    cc += 1
                End If
            End If
        Next
        inp_c = 0 : cc = 0 : out_c = 0
        'matXs
        For Each p As parameterClass In para_GP
            If p IsNot Nothing Then


                If p.bolInput Then
                    For exp As Integer = 0 To mat_pre_GP_Xs.RowCount - 1
                        mat_pre_GP_Xs.a(exp, inp_c) = convert_norm_to_real(p, CStr(mat_pre_GP_Xs.a(exp, inp_c)))
                    Next
                    inp_c += 1
                    cc += 1
                End If
            End If
        Next
        inp_c = 0 : cc = 0 : out_c = 0
        'matY
        For Each p As parameterClass In para_GP
            If p IsNot Nothing Then


                If p.bolOutput Then
                    For exp As Integer = 0 To postDP.matY.RowCount - 1
                        postDP.matY.a(exp, out_c) = convert_norm_to_real(p, CStr(postDP.matY.a(exp, out_c)))
                    Next
                    out_c += 1
                    cc += 1
                End If
            End If
        Next

        Dim ss As String = "the post_DP and mat_pre_GP_matXs has been reset. "
        Addtxt_2_txtGP_output(ss)


        para_GP = Nothing


    End Sub

    Private Sub _export_para_GP()
        Dim filepath As String = CALA_convert_basedir_file(workDir,
                                                                  DefaultInputFile.post_parameters + "_" + attachDateTimeSurfix(),
                                                                  CALA_Output_file_Type.json)

        'Dim para As parameterClass_json = convertParameterJson2Parameters()

        Dim paras As New parameterClass_json
        ReDim paras.para(Me.para_GP.Count - 1)
        For ii As Integer = 0 To Me.para_GP.Count - 1
            paras.para(ii) = New parameterClass
            paras.para(ii) = para_GP(ii)
        Next
        write_NN_jfile_general(paras, filepath)


        Dim s As String =
            "Parameter file: " + IO.Path.GetFileName(filepath) +
            " has been exported. If you want to use this para file in the program. You should re-load it again. "

        Addtxt_2_txtGP_output(s)
    End Sub

    Private Sub _get_kp_from_UI()
        With kp
            .P1 = CDbl(txtP1setting.Text)
            .P2 = CDbl(txtP2setting.Text)
            .P3 = CDbl(txtP3setting.Text)
        End With
        epsilon = CDbl(txtEpsilonsetting.Text)
    End Sub


    Private Sub _ExportALLCov2Json()
        If (CovK.K Is Nothing) Or
           (CovKs.K Is Nothing) Or
           (CovKss.K Is Nothing) Then
            Dim ss As String = "The CovK is empry!"
            Addtxt_2_txtGP_output(ss)
            Exit Sub
        End If

        Dim fil1 As String = CALA_file_name.covK + "_" + attachDateTimeSurfix() + ".json"
        Dim fil2 As String = CALA_file_name.covKs + "_" + attachDateTimeSurfix() + ".json"
        Dim fil3 As String = CALA_file_name.covKss + "_" + attachDateTimeSurfix() + ".json"
        Dim fil4 As String = CALA_file_name.CovK_y + "_" + attachDateTimeSurfix() + ".json"
        CovK.Me2Json(fil1)
        CovKs.Me2Json(fil2)
        CovKss.Me2Json(fil3)
        postDP.matY.Me2Json(fil4)

        Dim s As String = "The Cov object has been exported: " + vbNewLine +
                "CovKxx to file " + fil1 + vbNewLine +
                "CovKxs to file " + fil2 + vbNewLine +
                "CovKss to file " + fil3 + vbNewLine +
                "y to file" + fil4
            Addtxt_2_txtGP_output(s)



    End Sub


    Private Sub _ExportALLCov2CSV()
        If (CovK.K Is Nothing) Or
           (CovKs.K Is Nothing) Or
           (CovKss.K Is Nothing) Then
            Dim ss As String = "The CovK is empry!"
            Addtxt_2_txtGP_output(ss)
            Exit Sub
        End If

        Dim fil1 As String = CALA_file_name.covK + "_" + attachDateTimeSurfix() + ".csv"
            Dim fil2 As String = CALA_file_name.covKs + "_" + attachDateTimeSurfix() + ".csv"
            Dim fil3 As String = CALA_file_name.covKss + "_" + attachDateTimeSurfix() + ".csv"
            Dim fil4 As String = CALA_file_name.CovK_y + "_" + attachDateTimeSurfix() + ".csv"
            CovK.K.Me2CSV(fil1)
            CovKs.K.Me2CSV(fil2)
            CovKss.K.Me2CSV(fil3)
            postDP.matY.Me2CSV(fil4)

            Dim s As String = "The Cov object has been exported: " + vbNewLine +
                "CovKxx to file " + fil1 + vbNewLine +
                "CovKxs to file " + fil2 + vbNewLine +
                "CovKss to file " + fil3 + vbNewLine +
                 "y to file" + fil4
            Addtxt_2_txtGP_output(s)




    End Sub




    '===R


    '===R
    '---B
    '!+ **CovK opt**
    '? compute, optimization, para handling
    '---B
    Private Sub _Kernel_Parameter_go_optimizer()


        're-form covk
        Call _get_kp_from_UI()
        Dim ktype As CALA_Kernel_Type = CType(cboKernelType.SelectedIndex, CALA_Kernel_Type)

        CovK = New CALA_Cov("Kxx", ktype, kp, workDir)
        Dim sigmaoption As Integer = cboSigmaOption.SelectedIndex
        CovK.CovK_Generation_Exp_Kxx(postDP.matX, postDP.matX, postDP.matS, cboYparaSelect.SelectedIndex, epsilon, sigmaoption)



        Dim lr As Double = CDbl(txtCovKOpt_LR.Text)
        Dim maxI As Double = CInt(txtCovKOpt_MaxIter.Text)
        Dim delP1 As Double = CDbl(txtCovKOpt_DeltaP1.Text)
        Dim originalLML As Double = CovK.Compute_Log_Marginal_likelihood(postDP.matY, cboYparaSelect.SelectedIndex)


        Dim koo As CALA.Kernel_Optimiation_Output =
            CALA_Covariance_Kernel_SE_Optimiation_Single(Me.CovK, postDP.matX, postDP.matY, cboYparaSelect.SelectedIndex, postDP.matS, lr, delP1, maxI)


        txt_CovK_opt_P1.Text = koo.KernelPara.P1.ToString

        Dim ss As String = "K. opt: Iter=" + koo.Iteration.ToString +
            " and LML=" + koo.Log_Marginal_LikeliHood.ToString("##.###") +
            " from  LML=" + originalLML.ToString("##.###") +
            " P1 is " + koo.KernelPara.P1.ToString("##.##")

        Addtxt_2_txtGP_output(ss)


    End Sub



    '===R
    '---B
    '!+ **GP and display**
    '? compute, optimization, para handling
    '---B
    Private Sub _initial_chartGP()
        dvGPData = New DataVisualization.Charting.Series
        With dvGPData
            .Name = "GP Prediction"
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .Color = Color.Navy
            .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            .MarkerSize = 5
            .MarkerColor = Color.Blue


        End With


        dvOriData = New DataVisualization.Charting.Series
        With dvOriData
            .Name = "Original Data"
            .ChartType = DataVisualization.Charting.SeriesChartType.Point
            '.Color = Color.Navy
            .MarkerStyle = DataVisualization.Charting.MarkerStyle.Cross
            .MarkerSize = 10
            .MarkerColor = Color.Red
        End With

        dvGPErrorBar = New DataVisualization.Charting.Series
        With dvGPErrorBar
            .Name = "GP Prediction ErrorBar"
            .ChartType = DataVisualization.Charting.SeriesChartType.ErrorBar
            .Color = Color.Gray

        End With



        With chartGP
            .Series.Clear()
            .Series.Add(dvOriData)
            .Series(0).IsVisibleInLegend = False


            .Series.Add(dvGPData)
            .Series(1).IsVisibleInLegend = False

            .Series.Add(dvGPErrorBar)
            .Series(2).IsVisibleInLegend = False

            With .ChartAreas(0).AxisX
                '.Interval = 1
                '.IntervalOffset = 1
                '.IntervalAutoMode = DataVisualization.Charting.IntervalAutoMode.FixedCount
                '.LabelStyle.Enabled = False
                .MajorGrid.LineWidth = 0
                '.IsMarginVisible = False
                .LabelStyle.Format = "{0.00}"
            End With
            With .ChartAreas(0).AxisY
                .LabelStyle.Format = "{0.00}"

            End With
        End With

        '        aSeries.Points.AddXY(firstXPoint, firstMiddleYPoint, firstLowerYBound, firstUpperYBound);
        'aSeries.Points.AddXT(secondXPoint, secondMiddleYPoint, secondLowerYBound, secondUpperYBound);


    End Sub

    Private Sub _plot_chartGP_after_preCovK()

        Dim x0() As Double = mat_pre_GP_Xs.row_vec(0)
        Dim x00 As Double = mat_pre_GP_Xs.a(0, 0)
        '! the original one

        Dim dist() As Double
        'ReDim dist(postDP.matX.a_count - 1)
        ReDim dist(postDP.matX.RowCount - 1)
        '? compute distance
        For ii As Integer = 0 To dist.Count - 1
            Dim x() As Double = postDP.matX.row_vec(ii)
            dist(ii) = CALA_Dist_X_Xp(x0, x)
        Next


        Dim rightN() As Integer
        Dim rightDist() As Double
        Dim leftN() As Integer
        Dim leftDist() As Double

        Dim left_c As Integer = 0 : Dim right_c As Integer = 0
        For ii As Integer = 0 To dist.Count - 1
            If postDP.matX.a(ii, 0) - x00 > 0 Then
                'right
                ReDim Preserve rightN(right_c)
                rightN(right_c) = ii
                right_c += 1
            Else
                'left
                ReDim Preserve leftN(left_c)
                leftN(left_c) = ii
                left_c += 1
            End If
        Next

        'right , d shou be bigger and bigger 
        If right_c > 0 Then
            For ii As Integer = 0 To rightN.Count - 1
                For jj As Integer = ii To rightN.Count - 1
                    If dist(rightN(ii)) > dist(rightN(jj)) Then
                        Dim temp As Integer
                        temp = rightN(ii)
                        rightN(ii) = rightN(jj)
                        rightN(jj) = temp
                    End If
                Next
            Next
        End If

        If left_c > 0 Then
            For ii As Integer = 0 To leftN.Count - 1
                For jj As Integer = ii To leftN.Count - 1
                    If dist(leftN(ii)) < dist(leftN(jj)) Then
                        Dim temp As Integer
                        temp = leftN(ii)
                        leftN(ii) = leftN(jj)
                        leftN(jj) = temp
                    End If
                Next
            Next
        End If

        ReDim dv_ori_X_dist(postDP.matX.RowCount - 1)
        ReDim dv_ori_X_setN(postDP.matX.RowCount - 1)

        If left_c > 0 Then
            For ii As Integer = 0 To leftN.Count - 1
                dv_ori_X_setN(ii) = leftN(ii)
                dv_ori_X_dist(ii) = dist(leftN(ii)) * (-1)
            Next
        End If

        If right_c > 0 Then
            For ii As Integer = 0 To rightN.Count - 1
                dv_ori_X_setN(ii + left_c) = rightN(ii)
                dv_ori_X_dist(ii + left_c) = dist(rightN(ii))
            Next
        End If

        'plot
        dvGPData.Points.Clear()
        dvGPErrorBar.Points.Clear()
        dvOriData.Points.Clear()
        For ii As Integer = 0 To dv_ori_X_setN.Count - 1
            With dvOriData
                .Points.AddXY(dv_ori_X_dist(ii), postDP.matY.a(dv_ori_X_setN(ii), cboYparaSelect.SelectedIndex))
            End With
        Next

        '? also deal with the mat_pre_GP_Xs 
        ReDim dist(mat_pre_GP_Xs.RowCount - 1)
        '? compute distance
        For ii As Integer = 0 To dist.Count - 1
            Dim x() As Double = mat_pre_GP_Xs.row_vec(ii)
            dist(ii) = CALA_Dist_X_Xp(x0, x)
        Next

        rightN = Nothing : rightDist = Nothing : leftN = Nothing : leftDist = Nothing
        left_c = 0 : right_c = 0


        For ii As Integer = 0 To dist.Count - 1
            If mat_pre_GP_Xs.a(ii, 0) - x00 > 0 Then
                'right
                ReDim Preserve rightN(right_c)
                rightN(right_c) = ii
                right_c += 1
            Else
                'left
                ReDim Preserve leftN(left_c)
                leftN(left_c) = ii
                left_c += 1
            End If
        Next

        'right , d shou be bigger and bigger 
        If right_c > 0 Then
            For ii As Integer = 0 To rightN.Count - 1
                For jj As Integer = ii To rightN.Count - 1
                    If dist(rightN(ii)) > dist(rightN(jj)) Then
                        Dim temp As Integer
                        temp = rightN(ii)
                        rightN(ii) = rightN(jj)
                        rightN(jj) = temp
                    End If
                Next
            Next
        End If

        If left_c > 0 Then
            For ii As Integer = 0 To leftN.Count - 1
                For jj As Integer = ii To leftN.Count - 1
                    If dist(leftN(ii)) < dist(leftN(jj)) Then
                        Dim temp As Integer
                        temp = leftN(ii)
                        leftN(ii) = leftN(jj)
                        leftN(jj) = temp
                    End If
                Next
            Next
        End If

        ReDim dv_res_X_dist(mat_pre_GP_Xs.RowCount - 1)
        ReDim dv_res_X_setN(mat_pre_GP_Xs.RowCount - 1)

        If left_c > 0 Then
            For ii As Integer = 0 To leftN.Count - 1
                dv_res_X_setN(ii) = leftN(ii)
                dv_res_X_dist(ii) = dist(leftN(ii)) * (-1)
            Next
        End If

        If right_c > 0 Then
            For ii As Integer = 0 To rightN.Count - 1
                dv_res_X_setN(ii + left_c) = rightN(ii)
                dv_res_X_dist(ii + left_c) = dist(rightN(ii))
            Next
        End If

        'plot
        'dvOriData.Points.Clear()
        'For ii As Integer = 0 To dv_ori_X_setN.Count - 1
        '    With dvOriData
        '        .Points.AddXY(dv_ori_X_dist(ii), postDP.matY.a(dv_ori_X_setN(ii), cboYparaSelect.SelectedIndex))
        '    End With
        'Next

    End Sub


    Private Sub _plot_chartGP_after_GP()

        '? plot reuslt
        dvGPData.Points.Clear()

        For ii As Integer = 0 To GP_out.matX.RowCount - 1
            Dim valX As Double = dv_res_X_dist(ii)
            Dim valY As Double = GP_out.ys(dv_res_X_setN(ii))

            dvGPData.Points.AddXY(valX, valy)
        Next




        'plot error bar
        dvGPErrorBar.Points.Clear()
        For ii As Integer = 0 To GP_out.matX.RowCount - 1
            Dim valX As Double = dv_res_X_dist(ii)
            Dim valY As Double = GP_out.ys(dv_res_X_setN(ii))
            Dim errorY As Double = GP_out.Sigma(dv_res_X_setN(ii))

            dvGPErrorBar.Points.AddXY(valX, valY, valY - errorY, errorY + valY)


            'aSeries.Points.AddXY(firstXPoint, firstMiddleYPoint, firstLowerYBound, firstUpperYBound);
            'aSeries.Points.AddXT(secondXPoint, secondMiddleYPoint, secondLowerYBound, secondUpperYBound);
            dvGPData.Points.AddXY(valX, valY)
        Next


        chartGP.Update()




    End Sub
    Private Sub _computeGP()
        Dim ktype As CALA_Kernel_Type = CType(cboKernelType.SelectedIndex, CALA_Kernel_Type)
        Call _get_kp_from_UI()
        Dim y_ind As Integer = cboYparaSelect.SelectedIndex


        GP_out = CALA_GP_all_CovK_and_y(CovK, CovKs, CovKss, postDP, mat_pre_GP_Xs, ktype, kp, epsilon, y_ind)
        Dim ss As String = "GP done. "
        Addtxt_2_txtGP_output(ss)



        Call _convert_GP_out_back(y_ind)
        ss = "converting done"
        Addtxt_2_txtGP_output(ss)

        _plot_chartGP_after_GP()
        ss = "plot done"
        Addtxt_2_txtGP_output(ss)


    End Sub

    Private Sub _convert_GP_out_back(y_ind As Integer)

        GP_ori_X = New CALA_Matrix_FullDense("matX", postDP.matX.RowCount, postDP.matX.ColCount, postDP.matX.BaseDir)
        ReDim gp_ori_y(postDP.matX.RowCount - 1)

        Dim inp_c As Integer = 0 : Dim out_c As Integer = 0 : Dim cc As Integer = 0

        For Each p As parameterClass In para_GP
            If p IsNot Nothing Then
                If p.bolInput Then
                For ii As Integer = 0 To GP_out.matX.RowCount - 1
                    GP_out.matX.a(ii, inp_c) = convert_norm_to_real(p, GP_out.matX.a(ii, inp_c).ToString)
                Next

                For ii As Integer = 0 To GP_ori_X.RowCount - 1
                    GP_ori_X.a(ii, inp_c) = convert_norm_to_real(p, postDP.matX.a(ii, inp_c).ToString)
                Next

                inp_c += 1
            End If

            If p.bolOutput Then
                If out_c = y_ind Then
                    For ii As Integer = 0 To GP_out.ys.Count - 1
                        GP_out.ys(ii) = convert_norm_to_real(p, GP_out.ys(ii).ToString)
                        GP_out.Sigma(ii) = convert_norm_to_real_noaddMin(p, GP_out.Sigma(ii).ToString)
                    Next
                    For ii As Integer = 0 To postDP.matY.RowCount - 1
                        GP_ori_y(ii) = convert_norm_to_real(p, postDP.matY.a(ii, y_ind).ToString)
                    Next

                End If
                out_c += 1
            End If
                cc += 1
            End If
        Next

    End Sub

    Private Sub _GoGP_result_CSV()

        'original X
        'original Y
        Dim fil1 As String = CALA_file_name.GP_result_ori_X + attachDateTimeSurfix()
        Dim fil2 As String = CALA_file_name.GP_result_ori_Y + attachDateTimeSurfix()

        'export matXs real one
        'export ys _real 
        'export sigma_real
        Dim fil3 As String = CALA_file_name.GP_result_X + attachDateTimeSurfix()
        Dim fil4 As String = CALA_file_name.GP_result_Y + attachDateTimeSurfix()
        Dim fil5 As String = CALA_file_name.GP_result_S + attachDateTimeSurfix()


        Dim tempMat As CALA_Matrix_FullDense
        Dim tempM As DenseMatrix


        tempMat = New CALA_Matrix_FullDense("Y", GP_ori_y.Count, 1, workDir)
        tempM = New DenseMatrix(GP_ori_y.Count, 1)
        For ii As Integer = 0 To GP_ori_y.Count - 1
            tempM.At(ii, 0, GP_ori_y(ii))
        Next
        tempMat.MathNet2Me(tempM)

        GP_ori_X.BaseDir = mat_pre_GP_Xs.BaseDir
        GP_ori_X.Me2CSV(fil1)
        tempMat.Me2CSV(fil2)

        GP_out.matX.Me2CSV(fil3)

        tempMat = New CALA_Matrix_FullDense("Y", GP_out.ys.Count, 1, workDir)
        tempM = New DenseMatrix(GP_out.ys.Count, 1)
        For ii As Integer = 0 To GP_out.ys.Count - 1
            tempM.At(ii, 0, GP_out.ys(ii))
        Next
        tempMat.MathNet2Me(tempM)
        tempMat.Me2CSV(fil4)

        tempMat = New CALA_Matrix_FullDense("S", GP_out.Sigma.Count, 1, workDir)
        tempM = New DenseMatrix(GP_out.Sigma.Count, 1)
        For ii As Integer = 0 To GP_out.Sigma.Count - 1
            tempM.At(ii, 0, GP_out.Sigma(ii))
        Next
        tempMat.MathNet2Me(tempM)
        tempMat.Me2CSV(fil5)


        Dim s As String = "File output:" + vbNewLine
        s += "Ori_X:" + fil1
        s += "Ori_Y:" + fil2
        s += "Res_X:" + fil3
        s += "Res_y:" + fil4
        s += "Res_Sigma:" + fil5
        Addtxt_2_txtGP_output(s)

    End Sub


    Private Sub cmdFastDebug_Click(sender As Object, e As EventArgs) Handles cmdFastDebug.Click
        Dim postfile As String = "postDP_20210513150304.json"
        Dim parafile As String = "para_20210512085738.json"
        Dim Xsfile As String = "pre_GP_Xs_20210513211113.csv"



        Me.postDP = CALA_postDB_from_json(workDir + "\" + postfile)

        Dim para_json As parameterClass_json =
           read_NN_jfile_SAR(workDir + "\" + parafile, New parameterClass_json)


        Dim para_preRead() As parameterClass _
            = convertParameterJson2Parameters(para_json)
        If para_preRead IsNot Nothing Then
            Me.para_DP = Nothing
            Me.para_DP = para_preRead
            Call _display_Parameter_in_para_cbo()

            Dim inp_count As Integer = 0
            Dim out_count As Integer = 0
            For Each p As parameterClass In para_DP
                If p IsNot Nothing Then
                    If p.bolInput Then
                    inp_count += 1
                End If
                    If p.bolOutput Then
                        out_count += 1
                    End If
                End If
            Next
        End If

        _readXs_CSV_file(workDir + "\" + Xsfile)


        cmdGenPreCovK_Click(Me, EventArgs.Empty)
        cmdGenCovK_Click(Me, EventArgs.Empty)
        cmdGPGP_Click(Me, EventArgs.Empty)
    End Sub

    Private Sub cboYparaSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboYparaSelect.SelectedIndexChanged
        If cboYparaSelect_prev_index <> cboYparaSelect.SelectedIndex Then
            Call _Undo_postGP_and_mat_pre_GP_Xs()
            cboYparaSelect_prev_index = cboYparaSelect.SelectedIndex
        End If

    End Sub



    Private Sub chartGP_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles chartGP.MouseDoubleClick
        Dim ms As New IO.MemoryStream()
        chartGP.SaveImage(ms, DataVisualization.Charting.ChartImageFormat.Bmp)
        Dim bm As Bitmap = New Bitmap(ms)
        Clipboard.SetImage(bm)

        MsgBox("image copied to clipboard")
    End Sub
End Class


