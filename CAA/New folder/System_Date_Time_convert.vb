Namespace CAYMAL



    Public Module System_Date_Time_convert



        Public Function convertDataTime2String(dt As DateTime) As String
            Dim str As String = ""

            str = dt.ToString("yyyyMMdd") + "_" + dt.ToString("HHmmss")

            Return str

        End Function

        Public Function convertDateTime_2_csv_String(dt As DateTime) As String


            Return (dt.ToString("yyyyMMdd") + "," + dt.ToString("HHmmss") + "," + dt.ToString("fff"))

        End Function



        Public Function convertIntTime2TimeTime(Time As Integer) As DateTime
            Dim str As String = Time.ToString

            Do Until str.Length = 6
                str = "0" + str
            Loop


            'If str.Length = 5 Then
            '    str = "0" + str
            'End If
            Dim out As DateTime = DateTime.ParseExact(str, "HHmmss", Nothing)
            Return out
        End Function

        Public Function convertTimeSpanInSeconds(STime As Integer, ETime As Integer) As Long
            Dim start As DateTime = convertIntTime2TimeTime(STime)
            Dim eend As DateTime = convertIntTime2TimeTime(ETime)
            Dim tp As TimeSpan = eend - start

            Return tp.TotalSeconds

        End Function


        Public Function convertIntTime2StringTime(Time As Integer) As String
            Dim str As String = Time.ToString

            Do Until str.Length = 6
                str = "0" + str
            Loop
            Return str


        End Function

    End Module


End Namespace
