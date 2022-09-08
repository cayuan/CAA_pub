Public Class CAA_Weighting_Modify_UI


#Region "UI"


    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub CAA_Weighting_Modify_UI_Load(sender As Object, e As EventArgs) Handles Me.Load
        initilizeMe()

    End Sub


    Private Sub initilizeMe()
        cboWeiType.Items.Clear()
        For ii As Integer = 0 To Form1.USER.WeightingFile_Title.Count - 1
            cboWeiType.Items.Add(Form1.USER.WeightingFile_Title(ii))
        Next
        cboWeiType.SelectedIndex = -1
        CleardgWei()

    End Sub


    Private Sub CleardgWei()
        dgWei.Columns.Clear()
        dgWei.Enabled = True


        If cboWeiType.SelectedIndex < 0 Then
            Exit Sub
        End If

        Select Case Form1.USER.WeightingFile_Type(cboWeiType.SelectedIndex)

            Case CAA.CAA_USER_Function.CAA_Weighting_Format.one
                Dim width As Double = dgWei.Width / (3) - 2.5
                With dgWei
                    .ColumnCount = 3
                    .ColumnHeadersVisible = True
                    .RowHeadersWidth = 4

                    .Columns(0).Name = "ID"
                    .Columns(0).Width = width / 2

                    .Columns(1).Name = "level"
                    .Columns(1).Width = width

                    .Columns(2).Name = "Weighting"
                    .Columns(2).Width = width * 1.5
                    .DefaultCellStyle.Font = New Font("Tahoma", 8)
                End With
            Case CAA.CAA_USER_Function.CAA_Weighting_Format.two
                Dim width As Double = dgWei.Width / (4) - 2.5
                With dgWei
                    .ColumnCount = 4
                    .ColumnHeadersVisible = True
                    .RowHeadersWidth = 4

                    .Columns(0).Name = "ID"
                    .Columns(0).Width = width / 2

                    .Columns(1).Name = "levela"
                    .Columns(1).Width = width

                    .Columns(2).Name = "levelb"
                    .Columns(2).Width = width

                    .Columns(3).Name = "Weighting"
                    .Columns(3).Width = width * 1.5
                    .DefaultCellStyle.Font = New Font("Tahoma", 8)
                End With
            Case CAA.CAA_USER_Function.CAA_Weighting_Format.three
                Dim width As Double = dgWei.Width / (5) - 2.5
                With dgWei
                    .ColumnCount = 5
                    .ColumnHeadersVisible = True
                    .RowHeadersWidth = 4

                    .Columns(0).Name = "ID"
                    .Columns(0).Width = width / 2

                    .Columns(1).Name = "level1"
                    .Columns(1).Width = width

                    .Columns(2).Name = "level2"
                    .Columns(2).Width = width

                    .Columns(3).Name = "level3"
                    .Columns(3).Width = width

                    .Columns(4).Name = "Weighting"
                    .Columns(4).Width = width * 1.5
                    .DefaultCellStyle.Font = New Font("Tahoma", 8)
                End With
            Case Else
                Dim width As Double = dgWei.Width / (4) - 2.5
                With dgWei
                    .ColumnCount = 4
                    .ColumnHeadersVisible = True
                    .RowHeadersWidth = 4

                    .Columns(0).Name = "ID"
                    .Columns(0).Width = width / 2

                    .Columns(1).Name = "levela"
                    .Columns(1).Width = width

                    .Columns(2).Name = "levelb"
                    .Columns(2).Width = width

                    .Columns(3).Name = "Weighting"
                    .Columns(3).Width = width * 1.5
                    .DefaultCellStyle.Font = New Font("Tahoma", 8)
                End With
        End Select


        'Dim width As Double = dgWei.Width / (4) - 2.5


    End Sub


    Private Sub cboWeiType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWeiType.SelectedIndexChanged
        CleardgWei()

        Dim wt As CAA.CAA_WT = Form1.WEI(cboWeiType.SelectedIndex)

        For ii As Integer = 0 To wt.lstCondition.Count - 1
            dgWei.Rows.Add()
            dgWei.Rows(ii).Cells(0).Value = ii.ToString
            For jj As Integer = 0 To wt.lstCondition(ii).Count - 1
                dgWei.Rows(ii).Cells(jj + 1).Value = wt.lstCondition(ii)(jj).ToString
            Next
            dgWei.Rows(ii).Cells(wt.lstCondition(ii).Count + 1).Value = wt.lstWei(ii).ToString
        Next
    End Sub
#End Region


End Class