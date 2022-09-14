

Imports CAA.CAA


Namespace CAA


#Region "INV"


    Public Class INVENTORY

        Public SP As CAA.SPEC
        Public VE As Integer


        'make use of 
        '  Public Function convertTimeSpanInDays(STime As String, ETime As String) As Long
        '  Public Function convertStrTime2TimeTime(dt As String) As DateTime

        Sub New()
            SP = New SPEC
        End Sub

        Public Sub defineMeFromIntendedString(orS As String)
            Dim o As INVENTORY = convert_jsonString2Obj(orS, New INVENTORY)
            defineMeFromObject(o)
        End Sub

        Public Sub defineMeFromObject(o As INVENTORY)
            Me.SP.defineMeFromObject(o.SP)
            Me.VE = o.VE

        End Sub

        Public Function exportMe2IntendedString() As String
            Return write_obj2json_IndentedString(Me)
        End Function

        Public Function exportMe2String() As String
            'fw.WriteLine(",ART, MJ,SZ,CO,VE,DD")
            Return Me.SP.exportMe2String + "," + Me.VE.ToString
        End Function


        Public Function compareORDER2ME(o As INVENTORY) As Boolean
            Dim Same As Boolean = True
            If Me.VE <> o.VE Then
                Return False
            End If

            If Me.SP.compare2OtherSPEC(o.SP).same = False Then
                Return False
            End If

            Return True

        End Function


        Public Function compareORDER_ONLY_SPEC(o As ORDER) As Boolean
            Return Me.SP.compare2OtherSPEC(o.SP).same
        End Function




    End Class

#End Region



#Region "INVENTORY (Order set)"

    Public Class CAA_INVENTORY
        Public lstINV As List(Of INVENTORY)
        Public workDir As String
        Public myName As String
        Public originalFileName As String
        Public saveTime As String

        Public exportedFileName As String

        Sub New()
            lstINV = New List(Of INVENTORY)
        End Sub


        Public Sub defineMeFromObject(caaO As CAA_INVENTORY)
            Me.saveTime = convertDataTime2String_DateNTime(Now)


            Me.workDir = caaO.workDir
            Me.myName = caaO.myName
            Me.originalFileName = caaO.originalFileName


            lstINV = New List(Of INVENTORY)
            Dim o As New INVENTORY

            For ii As Integer = 0 To caaO.lstINV.Count - 1
                o.defineMeFromObject(caaO.lstINV(ii))
                Me.lstINV.Add(o)
            Next


        End Sub


        Public Sub defineMeFromJSONFile(workFolder As String, filename As String)

            Dim caaO As New CAA_INVENTORY
            caaO = read_json2obj(workFolder + filename, New CAA_INVENTORY)
            defineMeFromObject(caaO)
            Me.saveTime = convertDataTime2String_DateNTime(Now)
        End Sub

        Public Sub defineMeFromIntendedString(caaS As String)
            Dim caaO As New CAA_INVENTORY
            Try
                caaO = convert_jsonString2Obj(caaS, New CAA_INVENTORY)
                defineMeFromObject(caaO)
            Catch ex As Exception

            End Try

        End Sub

#Region "read and orgnize original CSV"
        Public Function defineMeFromORIGINALCSV(workFolder As String,
                                                filename As String,
                                                ARTCO As CAA_ARTCO) As Integer
            Me.saveTime = convertDataTime2String_DateNTime(Now)
            Me.workDir = workFolder
            Me.originalFileName = filename

            If IO.File.Exists(workFolder + filename) Then
                read_and_clean_ImportFile(workFolder + filename, ARTCO)
            End If

            Return 1
        End Function
#End Region


        Public Function exportMe2JSONFile(workfolder As String, filename As String)
            Me.saveTime = convertDataTime2String_DateNTime(Now)
            write_obj2json(Me, workfolder + filename)
            Return 1
        End Function


        Public Function exportMe2CSV_original() As Integer
            If Me.workDir = "" Then Return 0



            Dim lstART As New List(Of INV_print)

            For ii As Integer = 0 To Me.lstINV.Count - 1
                Dim inv As INVENTORY = Me.lstINV(ii)
                Dim invp As New INV_print
                invp.ART = inv.SP.ART
                invp.MJ = inv.SP.MJ
                invp.addSZZ(inv.SP.SZ, inv.VE)

                If lstART.Count = 0 Then
                    lstART.Add(invp)
                Else

                    Dim index_ART As Integer = -1

                    For jj As Integer = 0 To lstART.Count - 1
                        If lstART(jj).checkARTMJ_True4Same(invp.ART, invp.MJ) Then
                            index_ART = jj
                            GoTo label_exit
                        End If
                    Next
label_exit:
                    If index_ART = -1 Then
                        'new item
                        lstART.Add(invp)

                    Else
                        lstART(index_ART).addSZZ(inv.SP.SZ, inv.VE)
                    End If
                End If

            Next


            'print 

            Dim sp() As String = Split(Me.originalFileName, ".")
            Dim newfile As String = sp(0) + "_" + attachDateTimeSurfix() + "." + sp(1)
            Dim fi As String = workDir + newfile

            Me.exportedFileName = newfile

            Dim fw As New IO.StreamWriter(fi)

            Dim sl As String = ""

            sl = IO.Path.GetFileName(fi)
            fw.WriteLine(sl)
            fw.WriteLine(",ART,mj,1,1.5,2,2.5,3,3.5,4,4.5,5,5.5,6,6.5,7,7.5,8,8.5,9,9.5,10,10.5,11,11.5,12,12.5,13,13.5")

            For ii As Integer = 0 To lstART.Count - 1

                sl = ii.ToString + "," +
                     lstART(ii).ART + "," +
                     lstART(ii).MJ.ToString + "," +
                     lstART(ii).SZZ.ToString_format
                fw.WriteLine(sl)
            Next


            fw.Flush()
            fw.Close()
            fw.Dispose()



            Return 1

        End Function

        Public Function exportMe2CSV() As Integer
            If Me.workDir = "" Then Return 0

            Return exportMe2CSV(workDir)

        End Function
        Public Function exportMe2CSV(workfolder As String) As Integer

            Dim sp() As String = Split(Me.originalFileName, ".")
            Dim newfile As String = sp(0) + "_CAA_format_" + attachDateTimeSurfix() + "." + sp(1)
            Dim fi As String = workDir + newfile

            Me.exportedFileName = newfile


            'Dim fi As String = workDir + CAA_const.CAA_ORDER_Clearn_file + "_" + attachDateTimeSurfix() + ".csv"
            Dim fw As New IO.StreamWriter(fi)

            Dim sl As String = ""

            sl = IO.Path.GetFileName(fi)
            fw.WriteLine(sl)
            fw.WriteLine(",ART, MJ,SZ,CO,VE")

            For ii As Integer = 0 To lstINV.Count - 1
                sl = ii.ToString + "," + lstINV(ii).exportMe2String
                fw.WriteLine(sl)
            Next

            fw.Flush()
            fw.Close()
            fw.Dispose()

            Return 1
        End Function

        Public Function exportMe2IntendedString() As String
            Return write_obj2json_IndentedString(Me)
        End Function

#Region "ORDER Public Function"

        Private Function FindORDERIndex(o As INVENTORY) As Integer

        End Function

#End Region

#Region "read and orgnize ORDER original CSV"

        Private Function read_and_clean_ImportFile(ffi As String, ARTCO As CAA_ARTCO) As Integer

            If Not (IO.File.Exists(ffi)) Then Return 0


            'read the file, line by line, to the lstORIGINAL (of string()), 
            ' then close the file

            Dim lstORIGINAL As New List(Of String)

            Dim fr As New IO.StreamReader(ffi)
            fr.ReadLine() 'title
            fr.ReadLine() 'title2

            Dim readline As String = ""

            readline = fr.ReadLine
            Do Until (readline Is Nothing)

                If readline IsNot Nothing Then
                    If readline <> "" Then
                        lstORIGINAL.Add(readline)
                    End If
                End If

                readline = fr.ReadLine
            Loop
            fr.Close()
            fr.Dispose()


            'assign to order one by one
            ' check if there is anything duplicate

            Me.lstINV = New List(Of INVENTORY)


            For line As Integer = 0 To lstORIGINAL.Count - 1
                Dim sp() As String = Split(lstORIGINAL(line), ",")

                Dim ART As String = sp(1)
                'Dim DD As String = sp(2)
                Dim MJ As Integer = 1

                If sp(2).ToUpper = "MS" Then
                    MJ = 1
                End If
                If sp(2).ToUpper = "JR" Then
                    MJ = 0
                End If

                Dim lstSZ As New List(Of Integer)
                For ii As Integer = 0 To 25
                    lstSZ.Add(CInt(sp(ii + 3)))
                Next

                ',ART,date,mj,1,1.5,2,2.5,3,3.5,4,4.5,5,5.5,6,6.5,7,7.5,8,8.5,9,9.5,10,10.5,11,11.5,12,12.5,13,13.5

                For iSZ As Integer = 0 To lstSZ.Count - 1
                    If lstSZ(iSZ) <> 0 Then
                        'VE is not empty
                        Dim SZ As Integer = iSZ * 5 + 10   ' conver iSZ to the 10xsize --> integeger

                        Dim spec As New SPEC
                        Dim CO As String = ARTCO.returnCOfromART(ART)
                        spec.defineMeFromValue(ART, SZ, MJ, CO)

                        Dim INV As New INVENTORY
                        'ORD.DD = DD
                        INV.SP.defineMeFromObject(spec)
                        INV.VE = lstSZ(iSZ)

                        Me.lstINV.Add(INV)

                    End If
                Next
            Next

            ''export to file caa_const.CAA_ORDER_Clearn_file

            Return 1
        End Function
#End Region

    End Class


#End Region





    Public Class INV_print
        Public ART As String
        Public MJ As Integer
        Public SZZ As SZ_forPrint


        Sub New()
            SZZ = New SZ_forPrint
        End Sub


        Public Function checkARTMJ_True4Same(ART As String, MJ As Integer) As Boolean
            If Me.ART = ART And Me.MJ = MJ Then
                Return True
            End If
            Return False
        End Function

        Public Function addSZZ(SZ As Integer, VE As Integer) As Integer
            Dim ind As Integer = (SZ - 10) / 5
            Me.SZZ.SZ(ind) = VE

            Return 1

        End Function


    End Class



End Namespace



