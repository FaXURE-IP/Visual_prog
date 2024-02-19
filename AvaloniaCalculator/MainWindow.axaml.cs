using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaCalculator
{
    public partial class MainWindow : Window
    {
        private CalculatorViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CalculatorViewModel();
            DataContext = _viewModel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // Обработчик события для нажатия кнопок с цифрами
        public void OnDigitClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string digit = button.Content.ToString();
            _viewModel.AppendToOperand(digit);
        }

        // Обработчик события для нажатия кнопок с операторами
        public void OnOperatorClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string operation = button.Content.ToString();
            switch (operation)
            {
                case "+":
                    _viewModel.ExecuteOperation((a, b) => a + b);
                    break;
                case "-":
                    _viewModel.ExecuteOperation((a, b) => a - b);
                    break;
                case "*":
                    _viewModel.ExecuteOperation((a, b) => a * b);
                    break;
                case "/":
                    _viewModel.ExecuteOperation((a, b) => a / b);
                    break;
                case "mod":
                    _viewModel.ExecuteOperation((a, b) => a % b);
                    break;
                case "x^y":
                    _viewModel.ExecuteOperation(Math.Pow);
                    break;
                case "=":
                    _viewModel.Calculate();
                    break;
            }
        }

        // Обработчик события для нажатия кнопок с функциями
        public void OnFunctionClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string function = button.Content.ToString();
            switch (function)
            {
                case "n!":
                    _viewModel.Factorial();
                    break;
                case "lg":
                    _viewModel.Log10();
                    break;
                case "ln":
                    _viewModel.Log();
                    break;
                case "sin":
                    _viewModel.Sin();
                    break;
                case "cos":
                    _viewModel.Cos();
                    break;
                case "tan":
                    _viewModel.Tan();
                    break;
                case "floor":
                    _viewModel.Floor();
                    break;
                case "ceil":
                    _viewModel.Ceiling();
                    break;
                case "C":
                    _viewModel.Clear();
                    break;
                case "DEL":
                    _viewModel.DeleteLastCharacter();
                    break;
            }
        }
    }
}
