Imports CAA.CAA



'===
#Region "Dev't template"
'===

'!+ A standard class for starting
'? This can be a template for dev't.


'#Region "CAA_MachineStatus"

'Public Class MACH
''   <Varibles>
'    Public Function defineMEFromObject(SO As OSTOOL) As Integer

'    End Function
'    Public Function exportMe2String() As String

'    End Function
'    Public Function Compare2ME_True4Same(os As OSTOOL) As Boolean

'    End Function
'End Class

'Public Class CAA_MachineStatus
'    Public lstMACH As List(Of MACH)
'    Public FileName As String
'    Public workdir As String
'    Public exportedFileName As String

'    Sub New()
'        lstMACH = New List(Of MACH)
'    End Sub

'    Public Function defineMeFromObject(ma As CAA_MachineStatus) As Integer

'    End Function

'    Public Function defineMeFromCSV(workDir As String, filename As String) As Integer

'    End Function
'    Public Function exportMe2CSV() As Integer

'    End Function

'#Region "read original CSV"
'    Private Function read_and_clean_ImportFile(ffi As String) As Integer

'    End Function
'End Class

'#End Region
'===
#End Region
'===


Namespace CAA

    Public Module Machine_Status_Module
        Public Enum MachineToolStatus
            Available
            InMaintainence
            Down
            Not_Available
            Available_Plan_free
            Available_Plan_inUse
        End Enum

    End Module


#Region "CAA_MachineStatus"

    Public Class MACH
        ',ID,MS,ART,SZ,CO,OS,SN1,SN2
        Public ID As String
        Public MS As MachineToolStatus
        Public ART As String
        Public SZ As Integer
        Public CO As String
        Public OS As String
        Public SN1 As String
        Public SN2 As String



        Public Function defineMEFromObject(ma As MACH) As Integer
            Me.ID = ma.ID
            Me.MS = ma.MS
            Me.ART = ma.ART
            Me.SZ = ma.SZ
            Me.CO = ma.CO
            Me.OS = ma.OS
            Me.SN1 = ma.SN1
            Me.SN2 = ma.SN2
            Return 1
        End Function
        Public Function exportMe2String() As String

            ',ID,MS,ART,SZ,CO,OS,SN1,SN2
            Dim s As String = ""
            s = ID + "," +
               CType(MS, Integer).ToString + "," +
               ART + "," +
               SZ.ToString + "," +
               CO + "," +
               OS + "," +
               SN1 + "," +
               SN2

            Return s
        End Function
        Public Function Compare2ME_True4Same(os As MACH) As Boolean
            If ID = os.ID Then
                Return True
            End If
            Return False
        End Function
    End Class

    Public Class CAA_MachineStatus
        Public lstMACH As List(Of MACH)
        Public FileName As String
        Public workdir As String
        Public exportedFileName As String

        Sub New()
            lstMACH = New List(Of MACH)
        End Sub

        Public Function defineMeFromObject(ma As CAA_MachineStatus) As Integer
            Me.FileName = ma.FileName
            Me.workdir = ma.workdir
            Me.exportedFileName = ma.exportedFileName

            lstMACH = New List(Of MACH)
            For ii As Integer = 0 To ma.lstMACH.Count - 1
                lstMACH.Add(ma.lstMACH(ii))
            Next
            Return 1
        End Function

        Public Function defineMeFromCSV(workDir As String, filename As String) As Integer
            If Not (IO.File.Exists(workDir + filename)) Then Return 0
            Me.workdir = workDir
            Me.FileName = filename

            Return read_and_clean_ImportFile(workDir + filename)
        End Function
        Public Function exportMe2CSV() As Integer
            If lstMACH.Count = 0 Then Return 0


            Dim sp() As String = Split(FileName, ".")
            Dim newfile As String = sp(0) + "_" + attachDateTimeSurfix() + "." + sp(1)


            Dim fw As New IO.StreamWriter(workdir + newfile)
            Dim sl As String = ""

            sl = "MACH,"
            fw.WriteLine(sl)
            sl = ",ID,MS,ART,SZ,CO,OS,SN1,SN2"
            fw.WriteLine(sl)

            For ii As Integer = 0 To Me.lstMACH.Count - 1
                sl = ii.ToString + "," +
                     lstMACH(ii).exportMe2String
                fw.WriteLine(sl)
            Next
            fw.Flush()
            fw.Close()
            fw.Dispose()
            exportedFileName = newfile

            Return 1
        End Function

#Region "read original CSV"
        Private Function read_and_clean_ImportFile(ffi As String) As Integer
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



            lstMACH = New List(Of MACH)


            For line As Integer = 0 To lstORIGINAL.Count - 1
                Dim sp() As String = Split(lstORIGINAL(line), ",")

                ',ID,MS,ART,SZ,CO,OS,SN1,SN2

                Dim ma As New MACH
                With ma
                    .ID = sp(1).Trim
                    .MS = CType(CInt(sp(2)), MachineToolStatus)
                    .ART = sp(3).Trim
                    .SZ = CInt(sp(4))
                    .CO = sp(5).Trim
                    .OS = sp(6).Trim
                    .SN1 = sp(7).Trim
                    .SN2 = sp(8).Trim
                End With

                lstMACH.Add(ma)
            Next
            Return 1
        End Function
    End Class

#End Region



#End Region



#Region "CAA_OSTools"





    Public Class OSTOOL
        Public OS As String
        Public Sta As MachineToolStatus
        Public lstSZ As List(Of Integer)
        Public MJ As Integer
        Public UnitProducts As Integer
        Public SN1 As String    ' the SN number against the SIZE, start with 4,5,6, (the smllest size in integal)
        Public SN2 As String    ' the SN number against the number of TOOL, start with 1, 2,3,





        Sub New()

            lstSZ = New List(Of Integer)
        End Sub

        Public Function defineMEFromObject(SO As OSTOOL) As Integer
            Me.OS = SO.OS
            Me.Sta = SO.Sta
            Me.SN1 = SO.SN1
            Me.SN2 = SO.SN2
            Me.MJ = SO.MJ
            Me.UnitProducts = SO.UnitProducts


            lstSZ = New List(Of Integer)
            For ii As Integer = 0 To SO.lstSZ.Count - 1
                lstSZ.Add(SO.lstSZ(ii))
            Next
            Return 1
        End Function

        Public Function exportMe2String() As String
            ',OS,Status,SN1,SN2,MJ,UnitPairs,SZ1,SZ2

            Dim s As String = ""

            s += Me.OS + ","
            s += CType(Me.Sta, Integer).ToString + ","
            s += Me.SN1 + ","
            s += Me.SN2 + ","
            s += Me.MJ.ToString + ","
            s += Me.UnitProducts.ToString + ","
            s += Me.lstSZ(0).ToString + ","
            s += Me.lstSZ(1).ToString
            Return s
        End Function


        Public Function AddSZ(SZ As Integer)
            If lstSZ.Count = 0 Then
                lstSZ.Add(SZ)
            Else
                Dim doubled As Boolean = False

                For ii As Integer = 0 To lstSZ.Count - 1
                    If lstSZ(ii) = SZ Then
                        doubled = True
                    End If
                Next

                If Not (doubled) Then
                    lstSZ.Add(SZ)
                End If

            End If

            Return 1
        End Function

        Public Function FindSZ(SZ As Integer) As Boolean
            For ii As Integer = 0 To lstSZ.Count - 1
                If lstSZ(ii) = SZ Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function Compare2ME_True4Same(os As OSTOOL) As Boolean
            If Me.OS = os.OS And
               Me.SN1 = os.SN1 And
               Me.SN2 = os.SN2 Then
                Return True
            End If

            Return False
        End Function






    End Class



    Public Class CAA_OSTOOLS
        Public lstOSTools As List(Of OSTOOL)
        Public workdir As String
        Public filename As String
        Public exportedFileName As String


        Sub New()
            lstOSTools = New List(Of OSTOOL)
        End Sub

        Public Function defineMeFromObject(ost As CAA_OSTOOLS) As Integer
            Me.workdir = ost.workdir
            Me.filename = ost.filename
            Me.exportedFileName = ost.exportedFileName

            lstOSTools = New List(Of OSTOOL)
            For ii As Integer = 0 To ost.lstOSTools.Count - 1
                lstOSTools.Add(ost.lstOSTools(ii))
            Next
            Return 1
        End Function


        Public Function defineMeFromCSV(workDir As String, filename As String) As Integer
            If Not (IO.File.Exists(workDir + filename)) Then Return 0
            Me.workdir = workDir
            Me.filename = filename
            Return read_and_clean_ImportFile(workDir + filename)
        End Function

        Public Function exportMe2CSV() As Integer
            If lstOSTools.Count = 0 Then Return 0
            If workdir = "" Then Return 0
            If filename = "" Then Return 0


            Dim sp() As String = Split(filename, ".")
            Dim newfile As String = sp(0) + "_" + attachDateTimeSurfix() + "." + sp(1)


            Dim fw As New IO.StreamWriter(workdir + newfile)
            Dim sl As String = ""

            sl = "OS-Tools,"
            fw.WriteLine(sl)
            sl = ",OS,Status,SN1,SN2,MJ,UnitPairs,SZ1,SZ2"
            fw.WriteLine(sl)

            For ii As Integer = 0 To Me.lstOSTools.Count - 1
                sl = ii.ToString + "," +
                     lstOSTools(ii).exportMe2String
                fw.WriteLine(sl)
            Next
            fw.Flush()
            fw.Close()
            fw.Dispose()
            exportedFileName = newfile

            Return 1
        End Function

        Public Function findOSTOOL(OS As String, SN1 As String, SN2 As String) As OSTOOL
            Dim ind As Integer = -1
            Dim ost As New OSTOOL
            ost.OS = OS.Trim
            ost.SN1 = SN1.Trim
            ost.SN2 = SN2.Trim
            For ii As Integer = 0 To lstOSTools.Count - 1
                If lstOSTools(ii).Compare2ME_True4Same(ost) Then
                    Return lstOSTools(ii)
                End If
            Next
            Return Nothing
        End Function

        Public Function findOSTOOL_LST(ART As String, SZ As Integer, OSARTMJ As CAA_OSARTMJ) As List(Of OSTOOL)
            Dim OS As String = OSARTMJ.findOSfromART(ART).OS
            Dim lst As New List(Of OSTOOL)

            For ii As Integer = 0 To lstOSTools.Count - 1
                Dim ost As OSTOOL = lstOSTools(ii)
                If ost.OS = OS Then
                    If ost.FindSZ(SZ) Then
                        If lst.Count = 0 Then
                            lst.Add(ost)
                        Else
                            Dim doubled As Boolean = False
                            For jj As Integer = 0 To lst.Count - 1
                                If lst(jj).Compare2ME_True4Same(ost) Then
                                    doubled = True
                                End If
                            Next

                            If Not (doubled) Then
                                lst.Add(ost)
                            End If
                        End If
                    End If
                End If
            Next
            Return lst
        End Function


        Public Function assignToolStatus(OS As String,
                                          SN1 As String,
                                          SN2 As String,
                                          ms As MachineToolStatus) As Integer
            Dim ost As New OSTOOL
            ost.OS = OS : ost.SN1 = SN1 : ost.SN2 = SN2

            For ii As Integer = 0 To lstOSTools.Count - 1
                If lstOSTools(ii).Compare2ME_True4Same(ost) Then
                    lstOSTools(ii).Sta = ms
                    Return 1
                End If
            Next

            Return 0


        End Function


#Region "read original CSV"
        Private Function read_and_clean_ImportFile(ffi As String) As Integer

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

            Me.lstOSTools = New List(Of OSTOOL)
            For line As Integer = 0 To lstORIGINAL.Count - 1
                Dim sp() As String = Split(lstORIGINAL(line), ",")
                ',OS,Status,SN1,SN2,MJ,UnitPairs,SZ1,SZ2

                'put them into os
                Dim os As New OSTOOL
                With os
                    .OS = sp(1).Trim
                    .Sta = CType(CInt(sp(2)), MachineToolStatus)
                    .SN1 = sp(3).Trim
                    .SN2 = sp(4).Trim
                    .MJ = CInt(sp(5))
                    .UnitProducts = CInt(sp(6))
                    .lstSZ = New List(Of Integer)
                    .lstSZ.Add(CInt(sp(7)))
                    .lstSZ.Add(CInt(sp(8)))
                End With




                'check duplicate

                'add result

                If lstOSTools.Count = 0 Then
                    lstOSTools.Add(os)
                Else
                    Dim doubled As Boolean = False
                    For ii As Integer = 0 To lstOSTools.Count - 1
                        If lstOSTools(ii).Compare2ME_True4Same(os) Then
                            doubled = True
                        End If
                    Next ii

                    If Not (doubled) Then
                        lstOSTools.Add(os)
                    End If
                End If

            Next line

            Return 1

        End Function

#End Region

    End Class


#End Region



#Region "OS-ART-MJ"


    Public Class OSARTMJ
        Public ART As String
        Public OS As String
        Public MJ As Integer

        Sub New()

        End Sub

        Public Function defineMeFromObject(oo As OSARTMJ) As Integer
            Try
                Me.ART = oo.ART
                Me.OS = oo.OS
                Me.MJ = oo.MJ
            Catch ex As Exception
                Return 0
            End Try
            Return 1
        End Function

        Public Function Compare2ME_True4Same(oo As OSARTMJ) As Boolean
            If Me.ART = oo.ART And
               Me.MJ = oo.MJ And
               Me.OS = oo.OS Then
                Return True
            Else
                Return False
            End If

        End Function

    End Class

    Public Class CAA_OSARTMJ

        Public lstOSARTMJ As List(Of OSARTMJ)
        Public filename As String
        Public workDir As String
        Public exportedFileName As String
        Sub New()
            lstOSARTMJ = New List(Of OSARTMJ)
        End Sub


        Public Function defineMeFromORIGINALCSV(workFolder As String,
                                                filename As String) As Integer
            'Me.saveTime = convertDataTime2String_DateNTime(Now)

            If IO.File.Exists(workFolder + filename) Then
                Me.filename = filename
                Me.workDir = workFolder
                read_and_clean_ImportFile(workFolder + filename)
            End If

            Return 1
        End Function

        Public Function exportMe2CSV() As Integer

            If lstOSARTMJ.Count = 0 Then Return 0


            Dim sp() As String = Split(filename, ".")
            Dim newfile As String = sp(0) + "_" + attachDateTimeSurfix() + "." + sp(1)


            Dim fw As New IO.StreamWriter(workDir + newfile)
            Dim sl As String = ""

            sl = "OS-SAR-MJ,"
            fw.WriteLine(sl)
            sl = ",OS,ART,MJ"
            fw.WriteLine(sl)

            For ii As Integer = 0 To Me.lstOSARTMJ.Count - 1
                sl = ii.ToString + "," +
                     lstOSARTMJ(ii).OS + "," +
                     lstOSARTMJ(ii).ART + "," +
                     lstOSARTMJ(ii).MJ.ToString + ","
                fw.WriteLine(sl)
            Next
            fw.Flush()
            fw.Close()
            fw.Dispose()
            exportedFileName = newfile

            Return 1

        End Function


        Public Function findOSfromART(ART As String) As OSARTMJ
            For ii As Integer = 0 To lstOSARTMJ.Count - 1
                If lstOSARTMJ(ii).ART = ART Then
                    Return lstOSARTMJ(ii)
                End If
            Next
            Return Nothing
        End Function


        Private Function checkDoubled_True4Doubled(sa As OSARTMJ) As Boolean
            For ii As Integer = 0 To lstOSARTMJ.Count - 1
                If lstOSARTMJ(ii).Compare2ME_True4Same(sa) Then
                    Return True
                End If
            Next
            Return False
        End Function




#Region "read and orgnize ORDER original CSV"

        Private Function read_and_clean_ImportFile(ffi As String) As Integer

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

            Me.lstOSARTMJ = New List(Of OSARTMJ)


            For line As Integer = 0 To lstORIGINAL.Count - 1
                Dim sp() As String = Split(lstORIGINAL(line), ",")
                ' ,OS,ART,MJ  

                Dim oam As New OSARTMJ
                Try

                    oam.OS = Trim(sp(1))
                    oam.ART = Trim(sp(2))
                    oam.MJ = CInt(Trim(sp(3)))
                Catch ex As Exception

                End Try

                If lstOSARTMJ.Count = 0 Then
                    lstOSARTMJ.Add(oam)
                Else
                    If Not (checkDoubled_True4Doubled(oam)) Then
                        lstOSARTMJ.Add(oam)
                    End If
                End If
            Next

            ''export to file caa_const.CAA_ORDER_Clearn_file
            Return 1
        End Function
#End Region


    End Class
#End Region

End Namespace