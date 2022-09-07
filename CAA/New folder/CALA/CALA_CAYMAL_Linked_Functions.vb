

'?++ ** In this module, I will store all the functions that linked to the CAYMAL**

Imports MathNet.Numerics.LinearAlgebra.Double



Namespace CALA




    '?+ **1. Parameter class**
    Module CALA_CAYMAL_Parameters

        Public Function CALA_CAYMAL_generate_parameter_file(matX As CALA_Matrix_FullDense, matY As CALA_Matrix_FullDense) As parameterClass()
            Dim DB As New CALA_Covariance_pre_DataPurge_input
            DB.matXX = New CALA_Matrix_FullDense
            DB.matYY = New CALA_Matrix_FullDense
            DB.matXX = matX
            DB.matYY = matY
            Return CALA_CAYMAL_generate_parameter_file(DB)

        End Function

        Public Function CALA_CAYMAL_generate_parameter_file(DB As CALA_Covariance_pre_DataPurge_input) As parameterClass()
            Dim x_count As Integer = DB.matXX.ColCount
            Dim y_count As Integer = DB.matYY.ColCount

            Dim para() As parameterClass

            '? inputs
            For ii As Integer = 0 To x_count - 1
                ReDim Preserve para(ii) : para(ii) = New parameterClass

                para(ii).ParaName = "Inp" + (ii + 1).ToString
                Dim w() As Double = DB.matXX.col_vec(ii)


                Dim mm As CALA_2_value_structure
                mm.max = 0 : mm.min = 0
                mm = _compute_max_min_from_vector_array(w)

                With para(ii)
                    .bolInput = True
                    .bolOutput = False
                    .intSequence_nn = ii + 1
                    .bolInp_Fixed_or_Recurrent = False
                    .bolOut_Leaved_of_Recurrent = False
                    .pre_defined_Max = 0.0
                    .pre_defined_Min = 0.0
                    .Norm_max = mm.max
                    .Norm_Real_max = mm.max
                    .Norm_min = mm.min
                    .Norm_Real_min = mm.min
                End With
            Next

            '? outputs
            For ii As Integer = 0 To y_count - 1
                ReDim Preserve para(ii + x_count) : para(ii + x_count) = New parameterClass

                para(ii + x_count).ParaName = "Out" + (ii + 1).ToString
                Dim maxmin As CALA_2_value_structure =
                    _compute_max_min_from_vector_array(DB.matYY.col_vec(ii))
                With para(ii + x_count)
                    .bolInput = False
                    .bolOutput = True
                    .intSequence_nn = ii + 1 + x_count
                    .bolInp_Fixed_or_Recurrent = False
                    .bolOut_Leaved_of_Recurrent = False
                    .pre_defined_Max = 0.0
                    .pre_defined_Min = 0.0
                    .Norm_max = maxmin.max
                    .Norm_Real_max = maxmin.max
                    .Norm_min = maxmin.min
                    .Norm_Real_min = maxmin.min
                End With
            Next

            Return para

        End Function

        Private Function _compute_max_min_from_vector_array(w() As Double) As _
                CALA_2_value_structure

            If w.Count = 0 Then Return Nothing
            Dim out As CALA_2_value_structure
            out.max = w(0)
            out.min = w(0)

            For ii As Integer = 1 To w.Count - 1
                If out.max < w(ii) Then
                    out.max = w(ii)
                End If
                If out.min > w(ii) Then
                    out.min = w(ii)
                End If
            Next

            Return out
        End Function

    End Module



End Namespace

