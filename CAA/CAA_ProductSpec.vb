

Imports CAA.CAA

Namespace CAA

    Public Class SPEC
        Public ART As String
        Public SZ As Integer
        Public MJ As Integer
        Public CO As String

        Sub New()

        End Sub



        Public Function defineMeFromObject(sp As SPEC) As Integer
            Try
                Me.ART = sp.ART
                Me.SZ = sp.SZ
                Me.MJ = sp.MJ
                Me.CO = sp.CO
            Catch ex As Exception
                Return 0
            End Try
            Return 1
        End Function

        Public Function defineMeFromValue(ART As String,
                                           SZ As Integer,
                                           MJ As Integer,
                                           CO As String) As Integer

            Me.ART = ART
            Me.SZ = SZ
            Me.MJ = MJ
            Me.CO = CO
            Return 1
        End Function

        Public Function compare2OtherSPEC(sp As SPEC) As dSPEC
            Return compareTWOSPEC(Me, sp)
        End Function


        Public Function exportMe2String() As String
            'ART, MJ,SZ,CO

            Return (Me.ART + "," + Me.MJ.ToString + "," + Me.SZ.ToString + "," + Me.CO)

        End Function
    End Class


    Public Class dSPEC
        Public dART As Boolean
        Public dSZ As Boolean
        Public dMJ As Boolean
        Public dCO As Boolean
        Public same As Boolean

        Public dSZ_Value As Integer

        Sub New()
            Me.dART = True
            Me.dSZ = True
            Me.dMJ = True
            Me.dCO = True
            Me.same = True

            Me.dSZ_Value = 0

        End Sub

    End Class

    Public Module SPEC_function
        Public Function compareTWOSPEC(s1 As SPEC, s2 As SPEC) As dSPEC
            Dim dsp As New dSPEC


            If s1.ART <> s2.ART Then
                dsp.same = False
                dsp.dART = True
            End If

            If s1.SZ <> s2.SZ Then
                dsp.dSZ = False
                dsp.dSZ_Value = s2.SZ - s1.SZ
                dsp.same = False
            End If

            If s1.MJ <> s2.MJ Then
                dsp.dMJ = False
                dsp.same = False
            End If

            If s1.CO <> s2.CO Then
                dsp.dCO = False
                dsp.same = False
            End If

            Return dsp
        End Function
    End Module



    Public Class ARTCO
        Public ART As String
        Public CO As String

        Sub New()

        End Sub

        Public Function compareMe_TRUEforSAME(a As ARTCO) As Boolean
            If Me.ART = a.ART And Me.CO = a.CO Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

    Public Class CAA_ARTCO
        Public lstARTCO As List(Of ARTCO)
        Public filename As String
        Public workdir As String
        Public exportedFileName As String


        Sub New()
            lstARTCO = New List(Of ARTCO)
        End Sub

        Public Function defineMEFromObject(ac As CAA_ARTCO) As Integer
            If ac Is Nothing Then Return 0

            Me.lstARTCO = New List(Of ARTCO)
            For ii As Integer = 0 To ac.lstARTCO.Count - 1
                Dim ar As New ARTCO
                ar.ART = ac.lstARTCO(ii).ART
                ar.CO = ac.lstARTCO(ii).CO

                lstARTCO.Add(ar)
            Next
            Return 1
        End Function

        Public Function defineMEFromCSV(workDir As String, Filename As String) As Integer
            If Not (IO.File.Exists(workDir + Filename)) Then Return 0
            Me.workdir = workDir
            Me.filename = Filename
            Return readCSVandImport2ME(workDir + Filename)
        End Function

        Public Function defineMEFromJSON(workDir As String, Filename As String) As Integer
            If Not (IO.File.Exists(workDir + Filename)) Then Return 0
            Dim aco As New CAA_ARTCO
            aco = read_json2obj(workDir + Filename, New CAA_ARTCO)
            Me.defineMEFromObject(aco)
            Return 1
        End Function

        Public Function exportME2CSV() As Integer
            If workdir = "" Then
                Return 0
            End If

            Return exportME2CSV(workdir)


        End Function
        Public Function exportME2CSV(workDir As String) As Integer
            'CAA_ART_CO
            'Dim fi As String = workDir + CAA_const.CAA_ART_CO_file + "_" + attachDateTimeSurfix() + ".csv"

            Dim sp() As String = Split(filename, ".")
            Dim newfile As String = sp(0) + "_" + attachDateTimeSurfix() + "." + sp(1)
            Dim fi As String = workDir + newfile



            Dim rl As String = ""
            Dim fw As New IO.StreamWriter(fi)



            rl = IO.Path.GetFileName(fi)
            fw.WriteLine(rl)
            rl = ",ART,CO"
            fw.WriteLine(rl)

            For ii As Integer = 0 To lstARTCO.Count - 1
                rl = ii.ToString + "," + lstARTCO(ii).ART + "," + lstARTCO(ii).CO
                fw.WriteLine(rl)
            Next


            Me.exportedFileName = newfile


            fw.Flush()
            fw.Close()
            fw.Dispose()

            Return 1
        End Function


        Public Function exportME2JSON(workdir As String)
            Dim fi As String = workdir + CAA_const.CAA_ART_CO_file + "_" + attachDateTimeSurfix() + ".json"
            write_obj2json(Me, fi)
            Return 1
        End Function

#Region "define me from csv and check for duplicate"
        Private Function readCSVandImport2ME(fi As String) As Integer

            Dim lstLINE As New List(Of String)

            Dim fr As New IO.StreamReader(fi)
            Dim rl As String
            rl = fr.ReadLine 'first line 
            rl = fr.ReadLine 'second

            rl = fr.ReadLine
            Do Until (rl Is Nothing)
                If rl IsNot Nothing Then
                    If rl <> "" Then
                        lstLINE.Add(rl)
                    End If
                End If
                rl = fr.ReadLine
            Loop
            fr.Close()
            fr.Dispose()


            lstARTCO = New List(Of ARTCO)

            For line As Integer = 0 To lstLINE.Count - 1
                Dim ARTCO As New ARTCO
                Dim sp() As String = Split(lstLINE(line), ",")
                ARTCO.ART = sp(1).Trim
                ARTCO.CO = sp(2).Trim

                If lstARTCO.Count = 0 Then
                    lstARTCO.Add(ARTCO)
                Else

                    Dim doubled As Boolean = False

                    For ii As Integer = 0 To lstARTCO.Count - 1
                        If lstARTCO(ii).compareMe_TRUEforSAME(ARTCO) Then
                            doubled = True
                        End If
                    Next

                    If Not (doubled) Then
                        lstARTCO.Add(ARTCO)
                    End If
                End If

            Next

            Return 1
        End Function
#End Region

#Region "return CO from ART"

        Public Function returnCOfromART(ART As String) As String
            Dim o As String = ""

            For ii As Integer = 0 To lstARTCO.Count - 1
                If lstARTCO(ii).ART = ART Then
                    o = lstARTCO(ii).CO
                    Exit For
                End If
            Next

            Return o

        End Function


#End Region


    End Class



    Public Class SZ_forPrint
        Public SZ() As Integer

        Sub New()
            ReDim SZ(25)
            '0-25
            '1-13.5

        End Sub

        Public Function ToString_format() As String
            Dim s As String = ""
            For ii As Integer = 0 To SZ.Count - 1
                s += SZ(ii).ToString + ","
            Next

            Return Strings.Left(s, s.Length - 1)
        End Function


    End Class


End Namespace