using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;

namespace lab3.MainWindow
{
    public class MainWindow : Window
    {
        private TextBlock _displayTextBlock;
        private string _currentOperand = "";
        private string _previousOperand = "";
        private string _currentOperator = "";
        private double _result = 0;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _displayTextBlock = this.FindControl<TextBlock>("DisplayTextBlock");
        }

        private void UpdateDisplay()
        {
            _displayTextBlock.Text = _currentOperand;
        }

        private void AddToOperand(string value)
        {
            _currentOperand += value;
            UpdateDisplay();
        }

        private void Clear()
        {
            _currentOperand = "";
            _previousOperand = "";
            _currentOperator = "";
            _result = 0;
            UpdateDisplay();
        }

        private double Calculate()
        {
            double operand1 = double.Parse(_previousOperand);
            double operand2 = double.Parse(_currentOperand);
            double result = 0;
            switch (_currentOperator)
            {
                case "+":
                    result = operand1 + operand2;
                    break;
                case "-":
                    result = operand1 - operand2;
                    break;
                case "*":
                    result = operand1 * operand2;
                    break;
                case "/":
                    result = operand1 / operand2;
                    break;
                case "mod":
                    result = operand1 % operand2;
                    break;
                case "x^y":
                    result = Math.Pow(operand1, operand2);
                    break;
                case "lg":
                    result = Math.Log10(operand2);
                    break;
                case "ln":
                    result = Math.Log(operand2);
                    break;
                case "sin":
                    result = Math.Sin(operand2);
                    break;
                case "cos":
                    result = Math.Cos(operand2);
                    break;
                case "tan":
                    result = Math.Tan(operand2);
                    break;
                case "floor":
                    result = Math.Floor(operand2);
                    break;
                case "ceil":
                    result = Math.Ceiling(operand2);
                    break;
                case "!":
                    result = Factorial((int)operand2);
                    break;
            }
            return result;
        }

        private int Factorial(int n)
        {
            if (n == 0)
                return 1;
            return n * Factorial(n - 1);
        }

        private void ExecuteOperation()
        {
            if (!string.IsNullOrEmpty(_currentOperand))
            {
                if (string.IsNullOrEmpty(_currentOperator))
                {
                    _result = double.Parse(_currentOperand);
                }
                else
                {
                    _previousOperand = _result.ToString();
                    _result = Calculate();
                }
                _currentOperand = "";
                UpdateDisplay();
            }
        }

        private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();

            if (content == "C")
            {
                Clear();
            }
            else if (content == "=")
            {
                ExecuteOperation();
                _currentOperator = "";
                UpdateDisplay();
            }
            else if (content == "<-")
            {
                if (_currentOperand.Length > 0)
                {
                    _currentOperand = _currentOperand.Remove(_currentOperand.Length - 1);
                    UpdateDisplay();
                }
            }
            else if (content == "+" || content == "-" || content == "*" || content == "/" || content == "mod" || content == "x^y" || content == "lg" || content == "ln" || content == "sin" || content == "cos" || content == "tan" || content == "floor" || content == "ceil" || content == "!")
            {
                ExecuteOperation();
                _currentOperator = content;
            }
            else
            {
                AddToOperand(content);
            }
        }
    }
}