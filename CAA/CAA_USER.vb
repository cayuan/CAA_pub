
Imports System.Threading
Imports System.IO

Imports MathNet.Numerics.LinearAlgebra.Double

Imports Microsoft.Office.Interop


Namespace CAA
    Public Class CAA_USER
        Public USER_file_folder As String
        Public Base_Data_Folder As String
        Public Current_Working_Folder As String
        Public WeightingFile_Title() As String
        Public WeightingFile_Name() As String
        Public WeightingFile_Type() As CAA_Weighting_Format
        Public PriceBookWeighting() As Double
        Public CostReportWeighting() As Double

        Sub New()
            Call initializeMe()
        End Sub

        'ReadOnly Property USER_file_path As String
        '    Get
        '        Return USER_file_folder
        '    End Get
        'End Property

        Private Sub initializeMe()




            ' setup the WeightingFile_Name

            ReDim WeightingFile_Name(4)
            ReDim WeightingFile_Type(4)
            ReDim WeightingFile_Title(4)

            WeightingFile_Title(0) = "DueDay"
            WeightingFile_Type(0) = CAA.CAA_Weighting_Format.two


            WeightingFile_Title(1) = "MachineStatus_price"
            WeightingFile_Type(1) = CAA.CAA_Weighting_Format.three

            WeightingFile_Title(2) = "InventoryWei"
            WeightingFile_Type(2) = CAA.CAA_Weighting_Format.two

            WeightingFile_Title(3) = "MachineStatus_cost"
            WeightingFile_Type(3) = CAA.CAA_Weighting_Format.three

            WeightingFile_Title(4) = "PreviousEarning"
            WeightingFile_Type(4) = CAA.CAA_Weighting_Format.two


            ReDim PriceBookWeighting(2)
            ReDim CostReportWeighting(2)

        End Sub


        Public Sub defineMefromObject(u As CAA_USER)
            Me.Base_Data_Folder = u.Base_Data_Folder
            Me.Current_Working_Folder = u.Current_Working_Folder
            For ii As Integer = 0 To u.WeightingFile_Name.Count - 1
                Me.WeightingFile_Name(ii) = u.WeightingFile_Name(ii)
                Me.WeightingFile_Type(ii) = u.WeightingFile_Type(ii)
                Me.WeightingFile_Title(ii) = u.WeightingFile_Title(ii)
            Next

            For ii As Integer = 0 To PriceBookWeighting.Count - 1
                Me.PriceBookWeighting(ii) = u.PriceBookWeighting(ii)
            Next
            For ii As Integer = 0 To CostReportWeighting.Count - 1
                Me.CostReportWeighting(ii) = u.CostReportWeighting(ii)
            Next

        End Sub

        Public Sub readMe()
            Dim filename As String = Me.USER_file_folder + CAA_const_structure.CAA_const.CAA_profile_name
            Dim uu As New CAA_USER

            If IO.File.Exists(filename) Then
                uu = read_json2obj(filename, New CAA_USER)
            Else
                uu.generateDefaultUSERObject()
            End If

            defineMefromObject(uu)
        End Sub

        Public Sub writeMe()
            writeUSER(Me)
        End Sub


        Public Function generateDefaultUSERObject() As CAA_USER

            For ii As Integer = 0 To WeightingFile_Type.Count - 1
                WeightingFile_Name(ii) = WeightingFile_Title(ii) + ".csv"
            Next

            PriceBookWeighting(0) = 0.5 'for DD
            PriceBookWeighting(1) = 0.2 'for machine status
            PriceBookWeighting(2) = 0.3 'for inventory

            CostReportWeighting(0) = 0.5 '???

            Return Me
        End Function



        Public Function WeiFileExists_YesNo(ID As Integer) As Boolean
            Dim fi As String = Me.Base_Data_Folder + Me.WeightingFile_Name(ID)
            If IO.File.Exists(fi) Then
                Return True
            Else
                Return False
            End If

        End Function



    End Class

    Public Module CAA_USER_Function
        Public Enum CAA_Weighting_Format
            one
            two
            three
            others
        End Enum


        Public Function writeUSER(USER As CAA_USER) As Integer
            Dim filename As String = USER.USER_file_folder + CAA_const_structure.CAA_const.CAA_profile_name

            If IO.File.Exists(filename) Then
                IO.File.Delete(filename)
            End If

            Try
                write_obj2json(USER, filename)

                Return 1
            Catch ex As Exception
                Return 0
            End Try



        End Function

    End Module



End Namespace


