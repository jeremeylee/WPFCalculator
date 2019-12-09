using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private char operation;
        private bool operationSelected = false;
        private int decimalCount = 0;
        private double result = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonContent = button.Content.ToString();
            string fieldText = inputField.Text;

            switch (buttonContent)
            {
                case "C":
                    inputField.Text = "";
                    this.operationSelected = false;
                    this.decimalCount = 0;
                    break;
                case "Del":
                    if (fieldText.Length > 0)
                    {
                        inputField.Text = inputField.Text.Remove(inputField.Text.Length - 1);
                    }
                    break;
                case "=":
                    if (this.operationSelected && inputField.Text[inputField.Text.Length - 1] != '.')
                    {
                        inputField.Text = Evaluate(inputField.Text);
                    }         
                    break;
                default:
                    inputField.Text = String.Concat(inputField.Text, InputHandler(buttonContent, inputField.Text));
                    break;
            }
            
        }

        private string InputHandler(string buttonContent, string inputField)
        {
            
            if (Regex.IsMatch(buttonContent, @"[\+\-\/\*]"))
            {
                return ValidOperatorUsage(buttonContent, inputField);
            }

            if (buttonContent == ".")
            {
                Console.WriteLine("here");
                return ValidDecimalUsage(buttonContent);
            }

            return buttonContent;

        }

        private string ValidOperatorUsage(string buttonContent, string inputField)
        {
            int fieldLength = inputField.Length;
            if (fieldLength > 0 && Char.IsNumber(inputField[fieldLength - 1]) && this.operationSelected == false)
            {
                this.operation = buttonContent[0];
                this.operationSelected = true;
                return buttonContent;
            }
            else
            {
                return "";
            }
        }

        private string ValidDecimalUsage(string buttonContent)
        {
            if (this.decimalCount == 1 && this.operationSelected == false)
            {
                return "";
            }
            else if (this.decimalCount == 2 && this.operationSelected == true)
            {
                return "";
            }
            else
            {
                this.decimalCount++;
                return buttonContent;
            }

        }

        private string Evaluate(string inputField)
        {
            double leftInt = 0;
            double rightInt = 0;

            ParseExpression(inputField, out leftInt, out rightInt);

            switch (this.operation)
            {
                case '+':
                    this.result = leftInt + rightInt;
                    break;
                case '-':
                    this.result = leftInt - rightInt;
                    break;
                case '*':
                    this.result = leftInt * rightInt;
                    break;
                case '/':
                    this.result = leftInt / rightInt;
                    break;
                default:
                    return "";
            }

            this.operationSelected = false;
            return result.ToString();
        }

        private void ParseExpression(string inputField, out double leftInt, out double rightInt)
        {
            int operatorIndex = inputField.LastIndexOf(this.operation);

            string leftSide = inputField.Substring(0, operatorIndex);
            string rightSide = inputField.Substring(operatorIndex + 1);

            Double.TryParse(leftSide, out leftInt);
            Double.TryParse(rightSide, out rightInt);
        }
    }
}
