using System;
using System.ComponentModel;

namespace AvaloniaCalculator
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _displayText;
        private string _currentOperand = "";
        private double _currentResult = 0;
        private Func<double, double, double>? _currentOperation;
        private double _lastOperand;

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged(nameof(DisplayText));
                }
            }
        }

        public CalculatorViewModel()
        {
            Clear();
        }

        public void AppendToOperand(string value)
        {
            _currentOperand += value;
            DisplayText = _currentOperand;
        }

        public void ExecuteOperation(Func<double, double, double> operation)
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double operand = double.Parse(_currentOperand);
                _currentResult = _currentOperation != null ? _currentOperation(_currentResult, operand) : operand;
                _currentOperand = "";
                _currentOperation = operation;
                DisplayText = _currentResult.ToString();
            }
        }

        public void Calculate()
        {
            if (!string.IsNullOrEmpty(_currentOperand) && _currentOperation != null)
            {
                double operand = double.Parse(_currentOperand);
                _currentResult = _currentOperation(_currentResult, operand);
                _lastOperand = operand;
                _currentOperand = "";
                DisplayText = _currentResult.ToString();
            }
            else if (string.IsNullOrEmpty(_currentOperand) && _currentOperation != null)
            {
                _currentOperand = _lastOperand.ToString();
                _currentResult = _currentOperation(_currentResult, _lastOperand);
                DisplayText = _currentResult.ToString();
            }

        }

        public void Clear()
        {
            _currentOperand = "";
            _currentResult = 0;
            _currentOperation = null;
            DisplayText = "0";
        }

        public void DeleteLastCharacter()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                _currentOperand = _currentOperand.Substring(0, _currentOperand.Length - 1);
                DisplayText = _currentOperand;
            }
        }

        public void Factorial()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num = double.Parse(_currentOperand);
                double result = 1;
                for (int i = 1; i <= num; i++)
                {
                    result *= i;
                }
                DisplayText = result.ToString();
                _currentOperand = "";
            }
        }

        public void Log10()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num = double.Parse(_currentOperand);
                DisplayText = Math.Log10(num).ToString();
                _currentOperand = "";
            }
        }

        public void Log()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num = double.Parse(_currentOperand);
                DisplayText = Math.Log(num).ToString();
                _currentOperand = "";
            }
        }

        public void Sin()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num = double.Parse(_currentOperand);
                DisplayText = Math.Sin(num).ToString();
                _currentOperand = "";
            }
        }

        public void Cos()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num = double.Parse(_currentOperand);
                DisplayText = Math.Cos(num).ToString();
                _currentOperand = "";
            }
        }

        public void Tan()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num = double.Parse(_currentOperand);
                DisplayText = Math.Tan(num).ToString();
                _currentOperand = "";
            }
        }

        public void Floor()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num;
                if (double.TryParse(_currentOperand, out num))
                {
                    DisplayText = Math.Floor(num).ToString();
                    _currentOperand = "";
                }
                else
                {
                    DisplayText = "Error: Invalid input";
                }
            }
        }

        public void Ceiling()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                double num;
                if (double.TryParse(_currentOperand, out num))
                {
                    DisplayText = Math.Ceiling(num).ToString();
                    _currentOperand = "";
                }
                else
                {
                    DisplayText = "Error: Invalid input";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
