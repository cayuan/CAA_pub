Imports mnum = MathNet.Numerics
Namespace CALA


    Public Module CALA_IntegalTransform

        Public Enum CALA_IT_FFT_options_characteristics
            MathNet_half
            full
        End Enum
        Public Structure CALA_IT_FFT_data_output
            Dim y() As Double
            Dim nSamples As Integer
            Dim timeIntervals As Double
            Dim freq As Double
            Dim FFT_options As mnum.IntegralTransforms.FourierOptions
            Dim CALA_FFT_Options As CALA_IT_FFT_options_characteristics
        End Structure


        Public Function CALA_IT_FFT_RealVector_CALA_FFT_output(FFTvec() As Double,
                                                                 Optional FourierOptions As mnum.IntegralTransforms.FourierOptions = mnum.IntegralTransforms.FourierOptions.Matlab,
                                                                 Optional timeInterval As Double = 1) _
                                                            As CALA_IT_FFT_data_output

            Dim res() As Double = CALA.CALA_FFTShift_RealVector(FFTvec, FourierOptions)
            Dim o As CALA_IT_FFT_data_output
            With o
                .y = res
                .FFT_options = FourierOptions
                .CALA_FFT_Options = CALA_IT_FFT_options_characteristics.MathNet_half
                .timeIntervals = timeInterval
                .freq = 1 / timeInterval

            End With

            Return o


        End Function



        Public Function CALA_FFTShift_RealVector_FFT_output(FFTvec() As Double,
                                                            Optional FourierOptions As mnum.IntegralTransforms.FourierOptions = mnum.IntegralTransforms.FourierOptions.Matlab) _
                                                            As CALA_IT_FFT_data_output

            Dim res() As Double = CALA.CALA_FFTShift_RealVector(FFTvec, FourierOptions)
            Dim o As CALA_IT_FFT_data_output
            With o
                .y = res
                .FFT_options = FourierOptions
                .CALA_FFT_Options = CALA_IT_FFT_options_characteristics.MathNet_half
            End With

            Return o
        End Function
        Public Function CALA_FFTShift_RealVector(FFTvec() As Double,
                                                 Optional FourierOptions As mnum.IntegralTransforms.FourierOptions = mnum.IntegralTransforms.FourierOptions.Matlab) As Double()
            Dim fftmat As New mnum.LinearAlgebra.Double.DenseMatrix(FFTvec.Count, 1)
            For i As Integer = 0 To FFTvec.Count - 1
                fftmat(i, 0) = FFTvec(i)
            Next

            Dim fftmat_arr As New mnum.LinearAlgebra.Double.DenseMatrix(FFTvec.Count, 1)
            fftmat_arr = CALA_FFTShift_Real(fftmat)

            Dim res(FFTvec.Count) As Double
            For i As Integer = 0 To FFTvec.Count - 1
                res(i) = fftmat_arr(i, 0)
            Next
            Return res
        End Function
        Public Function CALA_FFTShift_RealVector(FFTvec() As Double) As Double()
            Dim fftmat As New mnum.LinearAlgebra.Double.DenseMatrix(FFTvec.Count, 1)
            For i As Integer = 0 To FFTvec.Count - 1
                fftmat(i, 0) = FFTvec(i)
            Next

            Dim fftmat_arr As New mnum.LinearAlgebra.Double.DenseMatrix(FFTvec.Count, 1)
            fftmat_arr = CALA_FFTShift_Real(fftmat)

            Dim res(FFTvec.Count) As Double
            For i As Integer = 0 To FFTvec.Count - 1
                res(i) = fftmat_arr(i, 0)
            Next
            Return res
        End Function

        Public Function CALA_FFTShift_RealVector_matlab(FFTvec() As Double) As Double()
            Dim fftmat As New mnum.LinearAlgebra.Double.DenseMatrix(FFTvec.Count, 1)
            For i As Integer = 0 To FFTvec.Count - 1
                fftmat(i, 0) = FFTvec(i)
            Next

            Dim fftmat_arr As New mnum.LinearAlgebra.Double.DenseMatrix(FFTvec.Count, 1)
            fftmat_arr = CALA_FFTShift_Real_matlab(fftmat)

            Dim res(FFTvec.Count) As Double
            For i As Integer = 0 To FFTvec.Count - 1
                res(i) = fftmat_arr(i, 0)
            Next
            Return res
        End Function

        Public Function CALA_FFTShift_Real(FFTmat As mnum.LinearAlgebra.Matrix(Of Double),
                                           Optional FourierOptions As mnum.IntegralTransforms.FourierOptions = mnum.IntegralTransforms.FourierOptions.Matlab) As mnum.LinearAlgebra.Matrix(Of Double)
            Dim Height As Integer = FFTmat.RowCount
            Dim Width As Integer = FFTmat.ColumnCount

            Dim arr As New mnum.LinearAlgebra.Complex32.DenseMatrix(Height, Width)
            Dim i, j As Integer
            'Fill arr with data....
            For i = 0 To Height - 1
                For j = 0 To Width - 1
                    'read out value from bitmap...
                    Dim value As Double = FFTmat(i, j)
                    arr(i, j) = New mnum.Complex32(value, 0.0)
                Next 'j
            Next 'i

            If Width > 1 Then
                For i = 0 To Height - 1
                    Dim in_row As New mnum.LinearAlgebra.Complex32.DenseVector(Width)
                    in_row = arr.Row(i).ToArray
                    mnum.IntegralTransforms.Fourier.Forward(in_row, FourierOptions)
                    arr.SetRow(i, in_row)

                Next 'loop over the rows
            End If

            If Height > 1 Then
                For i = 0 To Width - 1
                    Dim in_col As New mnum.LinearAlgebra.Complex32.DenseVector(Height)
                    in_col = arr.Column(i).ToArray
                    mnum.IntegralTransforms.Fourier.Forward(in_col)
                    arr.SetColumn(i, in_col)

                Next 'loop over the rows
            End If

            'the 2D Fourier transform is now stored in the array "arr"
            'do some evaluation with amplitudes and phases, for example

            'read out the amplitude
            'Dim magnitude As Double
            ' magnitude = arr(i, j).Magnitude
            'determine the maximum amplitude
            Dim maxval As New mnum.LinearAlgebra.Complex32.DenseVector(1)
            maxval(0) = arr.Enumerate().Max(Function(x) mnum.Complex32.Abs(x))


            'read out the phase
            'Dim phase As Double
            'phase = arr(i, j).Phase
            'determine the maximum phase
            Dim maxval1 As New mnum.LinearAlgebra.Complex32.DenseVector(1)
            maxval1(0) = arr.Enumerate().Max(Function(x As mnum.Complex32) Math.Abs(x.Phase))


            Dim res As New mnum.LinearAlgebra.Double.DenseMatrix(Height, Width)
            'Fill arr with data....
            For i = 0 To Height - 1
                For j = 0 To Width - 1
                    'read out value from bitmap...
                    Dim value As Double = arr(i, j).Magnitude
                    res(i, j) = value
                Next 'j
            Next 'i

            Return res

        End Function

        Public Function CALA_FFTShift_Real_matlab(FFTmat As mnum.LinearAlgebra.Matrix(Of Double)) As mnum.LinearAlgebra.Matrix(Of Double)
            Dim Height As Integer = FFTmat.RowCount
            Dim Width As Integer = FFTmat.ColumnCount

            Dim arr As New mnum.LinearAlgebra.Complex32.DenseMatrix(Height, Width)
            Dim i, j As Integer
            'Fill arr with data....
            For i = 0 To Height - 1
                For j = 0 To Width - 1
                    'read out value from bitmap...
                    Dim value As Double = FFTmat(i, j)
                    arr(i, j) = New mnum.Complex32(value, 0.0)
                Next 'j
            Next 'i

            If Width > 1 Then
                For i = 0 To Height - 1
                    Dim in_row As New mnum.LinearAlgebra.Complex32.DenseVector(Width)
                    in_row = arr.Row(i).ToArray
                    mnum.IntegralTransforms.Fourier.Forward(in_row, mnum.IntegralTransforms.FourierOptions.Matlab)
                    arr.SetRow(i, in_row)

                Next 'loop over the rows
            End If

            If Height > 1 Then
                For i = 0 To Width - 1
                    Dim in_col As New mnum.LinearAlgebra.Complex32.DenseVector(Height)
                    in_col = arr.Column(i).ToArray
                    mnum.IntegralTransforms.Fourier.Forward(in_col)
                    arr.SetColumn(i, in_col)

                Next 'loop over the rows
            End If

            'the 2D Fourier transform is now stored in the array "arr"
            'do some evaluation with amplitudes and phases, for example

            'read out the amplitude
            'Dim magnitude As Double
            ' magnitude = arr(i, j).Magnitude
            'determine the maximum amplitude
            Dim maxval As New mnum.LinearAlgebra.Complex32.DenseVector(1)
            maxval(0) = arr.Enumerate().Max(Function(x) mnum.Complex32.Abs(x))


            'read out the phase
            'Dim phase As Double
            'phase = arr(i, j).Phase
            'determine the maximum phase
            Dim maxval1 As New mnum.LinearAlgebra.Complex32.DenseVector(1)
            maxval1(0) = arr.Enumerate().Max(Function(x As mnum.Complex32) Math.Abs(x.Phase))


            Dim res As New mnum.LinearAlgebra.Double.DenseMatrix(Height, Width)
            'Fill arr with data....
            For i = 0 To Height - 1
                For j = 0 To Width - 1
                    'read out value from bitmap...
                    Dim value As Double = arr(i, j).Magnitude
                    res(i, j) = value
                Next 'j
            Next 'i

            Return res

        End Function
        Public Function CALA_FFTShift(FFTmat As mnum.LinearAlgebra.Matrix(Of mnum.Complex32)) As mnum.LinearAlgebra.Matrix(Of mnum.Complex32)

            Dim i, j As Integer
            Dim i_shift, j_shift As Integer
            Dim i_max, j_max As Integer

            i_shift = FFTmat.RowCount
            If IsEven(i_shift) Then
                i_shift = i_shift / 2
            Else
                i_shift = i_shift \ 2 + 1
            End If
            i_max = FFTmat.RowCount \ 2

            j_shift = FFTmat.ColumnCount

            If IsEven(j_shift) Then
                j_shift = j_shift / 2
            Else
                j_shift = j_shift \ 2 + 1
            End If
            j_max = FFTmat.ColumnCount \ 2

            Dim FFTShifted As New mnum.LinearAlgebra.Complex32.DenseMatrix(FFTmat.RowCount, FFTmat.ColumnCount)

            For i = 0 To i_max - 1
                For j = 0 To j_max - 1
                    FFTShifted(i + i_shift, j + j_shift) = FFTmat(i, j)
                    FFTShifted(i, j) = FFTmat(i + i_shift, j + j_shift)
                    FFTShifted(i + i_shift, j) = FFTmat(i, j + j_shift)
                    FFTShifted(i, j + j_shift) = FFTmat(i + i_shift, j)
                Next
            Next

            Return FFTShifted
        End Function

        Public Function IsOdd(ByVal iNum As Integer) As Boolean
            IsOdd = ((iNum \ 2) * 2 <> iNum)
        End Function
        Public Function IsEven(ByVal iNum As Integer) As Boolean
            IsEven = ((iNum \ 2) * 2 = iNum)
        End Function

    End Module
End Namespace