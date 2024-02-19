using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab3
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _currentOperand = "";
        private double _previousValue;
        private char? _previousOperator;

        public string CurrentOperand
        {
            get => _currentOperand;
            set
            {
                _currentOperand = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ButtonClick(string buttonText)
        {
            if (buttonText == "C")
            {
                Clear();
            }
            else if (buttonText == "←")
            {
                Backspace();
            }
            else if (IsNumeric(buttonText) || buttonText == ".")
            {
                AppendNumber(buttonText);
            }
            else if (buttonText == "=")
            {
                PerformOperation();
            }
            else
            {
                PerformOperation(buttonText[0]);
            }
        }

        private void Clear()
        {
            CurrentOperand = "";
            _previousValue = 0;
            _previousOperator = null;
        }

        private void Backspace()
        {
            if (CurrentOperand.Length > 0)
            {
                CurrentOperand = CurrentOperand.Substring(0, CurrentOperand.Length - 1);
            }
        }

        private void AppendNumber(string number)
        {
            CurrentOperand += number;
        }

        private void PerformOperation(char? op = null)
        {
            if (!string.IsNullOrEmpty(CurrentOperand))
            {
                double currentValue = double.Parse(CurrentOperand);
                if (_previousOperator != null)
                {
                    switch (_previousOperator)
                    {
                        case '+':
                            _previousValue += currentValue;
                            break;
                        case '-':
                            _previousValue -= currentValue;
                            break;
                        case '*':
                            _previousValue *= currentValue;
                            break;
                        case '/':
                            _previousValue /= currentValue;
                            break;
                    }
                }
                else
                {
                    _previousValue = currentValue;
                }

                if (op != null && op != '=')
                {
                    _previousOperator = op;
                }
                else
                {
                    _previousOperator = null;
                }

                CurrentOperand = "";
            }
        }

        private bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
