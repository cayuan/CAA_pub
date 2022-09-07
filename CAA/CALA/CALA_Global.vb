Namespace CALA

    Public Module CALA_Global


        Enum CALA_Output_file_Type
            json
            csv
        End Enum


        Public Structure CALA_constants
            Const small_number As Double = 0.000000001
            Const large_number As Double = 10000000000.0

        End Structure

        Public Structure CALA_file_name
            Const pre_data_purge_X_and_Y As String = "preDP"
            Const pre_data_purge_matX As String = "preDP_matXX"
            Const pre_data_purge_matY As String = "preDP_matYY"

            Const post_data_purge_X_and_Y_and_S As String = "postDP"
            Const post_data_purge_matX As String = "postDP_matX"
            Const post_data_purge_matY As String = "postDP_matY"
            Const post_data_purge_matS As String = "postDP_matS"

            '? This is only for Form15, the Xs csv 
            Const pre_GP_Xs_CSV As String = "pre_GP_Xs"

            'for Cov

            Const covK As String = "covKxx"
            Const covKs As String = "covKxs"
            Const covKss As String = "covKss"
            Const CovK_y As String = "cov_y"

            'GP result
            Const GP_result_X As String = "GPR_X"
            Const GP_result_Y As String = "GPR_Y"
            Const GP_result_S As String = "GPR_S"
            Const GP_result_ori_X As String = "GPR_ori_X"
            Const GP_result_ori_Y As String = "GPR_ori_Y"

            'PCA4DR
            Const PCA4DR_Q As String = "PCA_Q"
            Const PCA4DR_R As String = "PCA_R"
            Const PCA4DR_B As String = "PCA_B"

        End Structure

        Public Structure CALA_2_value_structure
            Dim max As Double
            Dim min As Double
        End Structure



    End Module


End Namespace


