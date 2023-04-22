using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ReversePolishNotation _reversPolishNotation = new ReversePolishNotation();
        ReversePolishNotation _reversPolishNotation = new ReversePolishNotation();
        private string _typedText = "";
        private int _hasOpeningBracket = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_InputTextExample(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _typedText += (string)button.Content + " ";
            CalculationResultText.Text = _typedText + " ";
            _reversPolishNotation.Input = _typedText + " ";
            //ResultRPNText.Clear();
            //ResultRPNText.Text = _reversPolishNotation.outp.ToString();


        }

        private void Button_BracketInput(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //if ((string)button.Content == "(")
            switch (button.Content)
            {
                case "(":
                    _typedText += (string)button.Content;
                    CalculationResultText.Text = _typedText;
                    _hasOpeningBracket++;
                    break;

                case ")":
                    if(_hasOpeningBracket != 0)
                    {
                        _typedText += (string)button.Content;
                        CalculationResultText.Text = _typedText;
                        _hasOpeningBracket--;
                    }
                    break;
            }
        }

        private void Button_Equals(object sender, RoutedEventArgs e)
        {
            //ResultRPNText.Text = _reversPolishNotation.outp.ToString();

            _reversPolishNotation.LineInRPN();
            
            ResultRPNText.Text = _reversPolishNotation.outp.ToString();
            CalculationResultText.Clear();

        }
    }
}
