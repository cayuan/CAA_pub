
Namespace CAA


    Public Module CAA_const_structure


        Public Structure CAA_copyright
            Const copywright As String = "Copyright © 2022 Dr. Cadmus Yuan, All rights reserved. "

        End Structure
        Public Structure MultithreadSetting
            Const Thread_to_Thread_Waiting_time_ms As Integer = 15
            Const reverse_coeff As Double = -0.5
            Const perturb_option_1_reverse_coeff As Double = 1
            Const deltaX_4_differential As Double = 0.000005
            Const overfitcheck As Double = -1
            Const fitting_stop As Double = 0.01
            Const LargeNumber As Double = 1000000.0
            Const SmallNumber As Double = 0.00000001

        End Structure


        Public Structure CAA_const
            Const CAA_profile_name As String = "CAA_USER.json"


            Const CAA_ORDER_Clearn_file As String = "CAA_ORDER_Clean"
            Const CAA_ART_CO_file As String = "CAA_ART_CO"


        End Structure


        Public Structure KeyWindowStartPosition
            Const form1_x As Integer = 200
            Const form1_y As Integer = 50

            'Const form3_x As Integer = 100
            'Const form3_y As Integer = 500

            'Const form5_x As Integer = 100
            'Const form5_y As Integer = 150

            'Const form6_x As Integer = 150
            'Const form6_y As Integer = 200

            'Const form7_x As Integer = 250
            'Const form7_y As Integer = 250

            'Const form9_x As Integer = 250
            'Const form9_y As Integer = 290

            'Const form91_x As Integer = 350
            'Const form91_y As Integer = 200
        End Structure









    End Module
End Namespace

