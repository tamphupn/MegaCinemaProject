using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaCommon.ANNModel
{
    public interface INeuralNetwork
    {
        void SetWeights(double[] weights);
        double[] ComputeOutputs(double[] xValue);
    }
    /// <summary>
    /// Defined structured of Neutron Network
    /// </summary>
    public class NeuralNetwork : INeuralNetwork
    {
        #region Property
        #region Infrastructure Neutron parameters
        /// <summary>
        /// Defined number of Node in Input Layer
        /// </summary>
        private int _numInput;

        /// <summary>
        /// Defined number of Node in Output Layer
        /// </summary>
        private int _numOutput;

        /// <summary>
        /// Defined number of Node in Hidden Layer
        /// </summary>
        private int _numHidden;

        /// <summary>
        /// Defined input data to Neutron 
        /// </summary>
        private double[] _inputs;

        /// <summary>
        /// Defined result output of neutron
        /// </summary>
        private double[] _outputs;

        /// <summary>
        /// Defined weight from Input layer to Hidden layer
        /// </summary>
        private double[][] _ihWeights;

        /// <summary>
        /// Defined biases value of Node in Hiddent layer
        /// </summary>
        private double[] _hBiases;

        /// <summary>
        /// Defined value of hidden node afer calc sum and add activate function
        /// </summary>
        private double[] _hOutputs;

        /// <summary>
        /// Defined weight from Hidden layer to Output layer
        /// </summary>
        private double[][] _hoWeights;

        /// <summary>
        /// Defined biases of Node in Output layer
        /// </summary>
        private double[] _oBiases;
        #endregion

        #region Back-propagation 

        /// <summary>
        /// Defined output Gradients
        /// </summary>
        private double[] _oGrads;

        /// <summary>
        /// Defined hiddent Gradients
        /// </summary>
        private double[] _hGrads;

        private double[][] _ihPrevWeightsDelta;
        private double[] _hPrevBiasesDelta;
        private double[][] _hoPrevWeightsDelta;
        private double[] _oPrevBiasesDelta;
        #endregion

        #region Train data
        private static Random rnd;
        #endregion
        #endregion

        #region Implement method 
        #region Infrastructure method compute 
        public NeuralNetwork(int numInput, int numHidden, int numOutput)
        {
            rnd = new Random(0);
            this._numInput = numInput;
            this._numOutput = numOutput;
            this._numHidden = numHidden;

            this._inputs = new double[numInput];
            this._outputs = new double[numOutput];

            this._ihWeights = MakeMatrix(numInput, numHidden);
            this._hBiases = new double[numHidden];
            this._hOutputs = new double[numHidden];

            this._hoWeights = MakeMatrix(numHidden, numOutput);
            this._oBiases = new double[numOutput];

            InitializeWeights();

            _oGrads = new double[_numOutput];
            _hGrads = new double[_numHidden];

            _ihPrevWeightsDelta = MakeMatrix(_numInput, _numHidden);
            _hPrevBiasesDelta = new double[_numHidden];
            _hoPrevWeightsDelta = MakeMatrix(_numHidden, _numOutput);
            _oPrevBiasesDelta = new double[_numOutput];

            //InitMatrix(_ihPrevWeightsDelta, 0.011);
            //InitMatrix(_hoPrevWeightsDelta, 0.011);
            //InitVector(_hPrevBiasesDelta, 0.011);
            //InitVector(_oPrevBiasesDelta, 0.011);
        }

        private static void InitVector(double[] vector, double value)
        {
            for (int i = 0; i < vector.Length; i++)
                vector[i] = value;
        }

        private static void InitMatrix(double[][] matrix, double value)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i][j] = value;
        }
        private static double[][] MakeMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new double[cols];
            }
            return result;
        }

        public void SetWeights(double[] weights)
        {
            int numWeights = (_numInput * _numHidden) + _numHidden + (_numHidden * _numOutput) + _numOutput;
            if (weights.Length != numWeights) throw new Exception("Wrong weights defined");

            int k = 0;

            for (int i = 0; i < _numInput; i++)
                for (int j = 0; j < _numHidden; j++)
                    _ihWeights[i][j] = weights[k++];

            for (int i = 0; i < _numHidden; i++)
                _hBiases[i] = weights[k++];

            for (int i = 0; i < _numHidden; i++)
                for (int j = 0; j < _numOutput; j++)
                    _hoWeights[i][j] = weights[k++];

            for (int i = 0; i < _numOutput; i++)
                _oBiases[i] = weights[k++];
        }

        public double[] GetWeights()
        {
            int numWeights = (_numInput * _numHidden) + _numHidden + (_numHidden * _numOutput) + _numOutput;
            double[] result = new double[numWeights];
            int k = 0;

            for (int i = 0; i < _numInput; i++)
                for (int j = 0; j < _numHidden; j++)
                    result[k++] = _ihWeights[i][j];

            for (int i = 0; i < _numHidden; i++)
                result[k++] = _hBiases[i];

            for (int i = 0; i < _numHidden; i++)
                for (int j = 0; j < _numOutput; j++)
                    result[k++] = _hoWeights[i][j];

            for (int i = 0; i < _numOutput; i++)
                result[k++] = _oBiases[i];

            return result;
        }

        public double[] ComputeOutputs(double[] xValue)
        {
            if (xValue.Length != _numInput)
                throw new Exception("Wrong values");

            double[] hSums = new double[_numHidden];
            double[] oSums = new double[_numOutput];

            for (int i = 0; i < xValue.Length; i++)
                _inputs[i] = xValue[i];

            for (int i = 0; i < _numHidden; i++)
                for (int j = 0; j < _numInput; j++)
                    hSums[i] += _inputs[j] * _ihWeights[j][i];

            for (int i = 0; i < _numHidden; i++)
                hSums[i] += _hBiases[i];

            for (int i = 0; i < _numHidden; i++)
                _hOutputs[i] = HyperTan(hSums[i]);

            for (int i = 0; i < _numOutput; i++)
                for (int j = 0; j < _numHidden; j++)
                    oSums[i] += hSums[j] * _hoWeights[j][i];

            for (int i = 0; i < _numOutput; i++)
                oSums[i] += _oBiases[i];

            double[] softOut = SoftMax(oSums);

            for (int i = 0; i < _outputs.Length; i++)
                _outputs[i] = softOut[i];

            double[] result = new double[_numOutput];

            for (int i = 0; i < _outputs.Length; i++)
                result[i] = _outputs[i];

            return result;
        }

        private static double HyperTan(double v)
        {
            if (v < -20.0)
                return -1.0;
            else if (v > 20.0)
                return 1.0;
            else return Math.Tanh(v);
        }

        private static double LogSigmod(double x)
        {
            if (x < -45.0)
                return 0.0;
            else if (x > 45.0)
                return 1.0;
            else return 1.0 / (1.0 + Math.Exp(-x));
        }

        private static double[] SoftMax(double[] oSums)
        {
            double max = oSums[0];
            for (int i = 0; i < oSums.Length; i++)
                if (oSums[i] > max) max = oSums[i];

            double scale = 0.0;
            for (int i = 0; i < oSums.Length; i++)
                scale += Math.Exp(oSums[i] - max);

            double[] result = new double[oSums.Length];
            for (int i = 0; i < oSums.Length; i++)
                result[i] = Math.Exp(oSums[i] - max) / scale;

            return result;
        }

        public double[] GetOutputs()
        {
            return _outputs;
        }
        #endregion

        #region Back-Propagation 
        public void FindWeights(double[] tValues, double[] xValues, double learnRate, double momentum, int maxEpochs)
        {
            int epoch = 0;
            while (epoch <= maxEpochs)
            {
                double[] yValues = ComputeOutputs(xValues);
                UpdateWeights(tValues, learnRate, momentum);

                if (epoch % 100 == 0)
                {
                    Console.Write("epoch= " + epoch.ToString().PadLeft(5) + "   cur outputs = ");
                    //Program.ShowVector(yValues, 2, 4, true);
                }
                epoch++;
            }
        }

        private void UpdateWeights(double[] tValues, double learnRate, double momentum)
        {
            //can remove to improve performance
            if (tValues.Length != _numOutput)
                throw new Exception("Wrong test training");

            //compute the gradients each node in output 
            for (int i = 0; i < _oGrads.Length; i++)
            {
                double derivative = (1 - _outputs[i]) * _outputs[i];
                _oGrads[i] = derivative * (tValues[i] - _outputs[i]);
            }

            //compute the gradients each node in hidden
            for (int i = 0; i < _hGrads.Length; i++)
            {
                double derivative = (1 - _hOutputs[i]) * (1 + _hOutputs[i]);
                double sum = 0.0;
                for (int j = 0; j < _numOutput; j++)
                    sum += _oGrads[j] * _hoWeights[i][j];
                _hGrads[i] = derivative * sum;
            }

            //update input - hidden weights
            for (int i = 0; i < _ihWeights.Length; i++)
            {
                for (int j = 0; j < _ihWeights[i].Length; j++)
                {
                    double delta = learnRate * _hGrads[j] * _inputs[i];
                    _ihWeights[i][j] += delta;
                    _ihWeights[i][j] += momentum * _ihPrevWeightsDelta[i][j];
                    _ihPrevWeightsDelta[i][j] = delta;
                }
            }

            //update biases hidden nodes
            for (int i = 0; i < _hBiases.Length; i++)
            {
                double delta = learnRate * _hGrads[i];
                _hBiases[i] += delta;
                _hBiases[i] += momentum * _hPrevBiasesDelta[i];
                _hPrevBiasesDelta[i] = delta;
            }

            //update hidden to ouput node weights
            for (int i = 0; i < _hoWeights.Length; i++)
            {
                for (int j = 0; j < _hoWeights[i].Length; j++)
                {
                    double delta = learnRate * _oGrads[j] * _hOutputs[i];
                    _hoWeights[i][j] += delta;
                    _hoWeights[i][j] += momentum * _hoPrevWeightsDelta[i][j];
                    _hoPrevWeightsDelta[i][j] = delta;
                }
            }

            //update biases output node 
            for (int i = 0; i < _oBiases.Length; i++)
            {
                double delta = learnRate * _oGrads[i] * 1.0;
                _oBiases[i] += delta;
                _oBiases[i] += momentum * _oPrevBiasesDelta[i];
                _oPrevBiasesDelta[i] = delta;
            }
        }
        #endregion

        #region Train data
        private void InitializeWeights()
        {
            // Initialize weights and biases to small random values.
            int numWeights = (_numInput * _numHidden) + (_numHidden * _numOutput) +
            _numHidden + _numOutput;
            double[] initialWeights = new double[numWeights];
            double lo = -0.01;
            double hi = 0.01;
            for (int i = 0; i < initialWeights.Length; ++i)
                initialWeights[i] = (hi - lo) * rnd.NextDouble() + lo;
            this.SetWeights(initialWeights);
        }
        public void Train(double[][] trainData, int maxEpochs, double learnRate, double momentum)
        {
            int epoch = 0;
            double[] xValues = new double[_numInput];
            double[] tValues = new double[_numOutput];

            int[] sequence = new int[trainData.Length];
            for (int i = 0; i < sequence.Length; i++)
                sequence[i] = i;

            while (epoch < maxEpochs)
            {
                double mse = MeanSquareError(trainData);
                if (mse < 0.040)
                    break;

                Shuffle(sequence);
                for (int i = 0; i < trainData.Length; i++)
                {
                    int idx = sequence[i];
                    Array.Copy(trainData[idx], xValues, _numInput);
                    Array.Copy(trainData[idx], _numInput, tValues, 0, _numOutput);
                    ComputeOutputs(xValues);
                    UpdateWeights(tValues, learnRate, momentum);
                }
                epoch++;
            }
        }

        private static void Shuffle(int[] sequence)
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        }

        private double MeanSquareError(double[][] trainData)
        {
            double sumSquaredError = 0.0;
            double[] xValues = new double[_numInput];
            double[] tValues = new double[_numOutput];

            for (int i = 0; i < trainData.Length; i++)
            {
                Array.Copy(trainData[i], xValues, _numInput);
                Array.Copy(trainData[i], _numInput, tValues, 0, _numOutput);

                double[] yValues = this.ComputeOutputs(xValues);
                for (int j = 0; j < _numOutput; j++)
                {
                    double err = tValues[j] - yValues[j];
                    sumSquaredError += err * err;
                }
            }
            return sumSquaredError / trainData.Length;
        }

        public double Accuracy(double[][] testData)
        {
            int numCorrect = 0;
            int numWrong = 0;
            double[] xValues = new double[_numInput];
            double[] tValues = new double[_numOutput];
            double[] yValues;

            for (int i = 0; i < testData.Length; i++)
            {
                Array.Copy(testData[i], xValues, _numInput);
                Array.Copy(testData[i], _numInput, tValues, 0, _numOutput);
                yValues = this.ComputeOutputs(xValues);
                int maxIndex = MaxIndex(yValues);

                if (tValues[maxIndex] == 1.0)
                    numCorrect++;
                else
                    numWrong++;
            }

            return (numCorrect * 1.0) / (numCorrect + numWrong);
        }

        public int TrainValuePredition(double[] preditionValue)
        {
            if (preditionValue.Length != _numInput) return -1;
            double[] yValues = this.ComputeOutputs(preditionValue);
            int maxIndex = MaxIndex(yValues);
            return maxIndex;
        }

        public int MaxIndex(double[] vector)
        {
            int bigIndex = 0;
            double biggestVal = vector[0];
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i];
                    bigIndex = i;
                }
            }
            return bigIndex;
        }
        #endregion
        #endregion
    }
}
