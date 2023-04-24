using System.Windows;
using System.Windows.Controls;

namespace Calc
{
    public partial class MainWindow : Window
    {
        ReversePolishNotation _reversPolishNotation = new ReversePolishNotation();
        private string _typedText = "";
        private int _hasOpeningBracket = 0;
        private bool _hasOperationUsed;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_InputNumberExample(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _typedText += (string)button.Content;
            CalculationResultText.Text = _typedText;
            _reversPolishNotation.Input = _typedText;
            _hasOperationUsed = true;
        }

        private void Button_InputTheOperation(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (_hasOperationUsed || button.Content.ToString() == "-")
            {
                if (string.IsNullOrEmpty(_typedText) || (_typedText.EndsWith(" ") && button.Content.ToString() == "-"))
                {
                    _typedText += (string)button.Content;
                    CalculationResultText.Text = _typedText;
                    _hasOperationUsed = false;
                }
                else
                {
                    _typedText += " " + (string)button.Content + " ";
                    CalculationResultText.Text = _typedText;
                    _hasOperationUsed = false;
                }
            }
        }

        private void Button_BracketInput(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Content)
            {
                case "(":
                    _typedText += " " + (string)button.Content + " ";
                    CalculationResultText.Text = _typedText;
                    _hasOpeningBracket++;
                    break;
                case ")":
                    if(_hasOpeningBracket != 0)
                    {
                        _typedText += " " + (string)button.Content + " ";
                        CalculationResultText.Text = _typedText;
                        _hasOpeningBracket--;
                    }
                    break;
            }
        }

        private void Button_Equals(object sender, RoutedEventArgs e)
        {
            _reversPolishNotation.LineInRPN();
            ResultRPNText.Text = _reversPolishNotation.output.ToString();
            _reversPolishNotation.CalculateRPN(_reversPolishNotation.output);
            CalculationResultText.Text = _reversPolishNotation.output.ToString();
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            _reversPolishNotation.output = "";
            _typedText = null;
            CalculationResultText.Clear();
            ResultRPNText.Clear();
        }

        private void Button_InputDot(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _typedText += (string)button.Content;
            CalculationResultText.Text = _typedText;
        }
    }
}
