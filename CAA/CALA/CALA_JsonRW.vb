'read and write of Json file 

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

'? note: this is a differnece between the Excel and .net json library
'?       the difference is the there will be a extra pair of '[' and ']' at the 
'?       the beginning and end of file. 
'?       To make things goes easy, only use Excel format

'? note2: NN_JsonRW also applied for RL use 20200421

'reference : https://www.newtonsoft.com/json/help/html/SerializeObject.htm

'?+ **The concept is from the NN_JsonRW.vb**


Namespace CALA
    Module CALA_JsonRW
        Public Function read_json2obj(filepath As String, obj As Object) As Object
            If filepath Is Nothing Then Return Nothing
            If Not (IO.File.Exists(filepath)) Then Return Nothing


            Dim rs As New IO.StreamReader(filepath)
            Dim s As String = rs.ReadLine
            Dim outS As String
            'due to test remove the first and last line

            s = rs.ReadLine 'remove the firstline
            Do While (s IsNot Nothing)

                outS += s + vbNewLine
                s = rs.ReadLine
                If s Is Nothing Then  'deal with the last line
                    outS = Left(outS, outS.Length - 5)
                End If
            Loop


            rs.Close()
            rs.Dispose()

            'Select Case obj.GetType


            '    '? matrix general
            '    Case GetType(CALA_Matrix_output_format)
            '        Return JsonConvert.DeserializeObject(Of CALA_Matrix_output_format)(outS)

            'End Select

            Return _returnObject(outS, obj)
        End Function


        Private Function _returnObject(outS As String, obj As Object) As Object
            Select Case obj.GetType


                '? matrix general
                Case GetType(CALA_Matrix_output_format)
                    Return JsonConvert.DeserializeObject(Of CALA_Matrix_output_format)(outS)

                    '    '? Covariance general
                    'Case GetType(CALA_Covariance_output_format)
                    '    Return JsonConvert.DeserializeObject(Of CALA_Covariance_output_format)(outS)

                    '    'Covariance pre data purge data format
                    'Case GetType(CALA_preDP_json_format)
                    '    Return JsonConvert.DeserializeObject(Of CALA_preDP_json_format)(outS)

                    '    'Covariance post data purge data format
                    'Case GetType(CALA_postDP_json_format)
                    '    Return JsonConvert.DeserializeObject(Of CALA_postDP_json_format)(outS)

            End Select
            Return Nothing
        End Function



        Public Sub write_obj2json(nn As Object, filepath As String)
            'JsonConvert.SerializeObject(nn_json_inp, Formatting.Indented)
            If nn Is Nothing Then Exit Sub
            If filepath Is Nothing Then Exit Sub

            Dim convS As String = JsonConvert.SerializeObject(nn, Formatting.Indented)

            Dim rw As New IO.StreamWriter(filepath)

            rw.WriteLine("[")
            rw.Write(convS + vbNewLine)


            rw.WriteLine("]")
            rw.Close()
            rw.Dispose()
        End Sub




    End Module
End Namespace

