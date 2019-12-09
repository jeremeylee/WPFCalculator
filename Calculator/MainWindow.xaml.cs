﻿using System;
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
                    break;
                case "Del":
                    if (fieldText.Length > 0)
                    {
                        inputField.Text = inputField.Text.Remove(inputField.Text.Length - 1);
                    }
                    break;
                default:
                    inputField.Text = String.Concat(inputField.Text, InputHandler(buttonContent, inputField.Text));
                    //inputField.Text = String.Concat(inputField.Text, buttonContent);
                    break;
            }
            
        }

        private string InputHandler(string buttonContent, string inputField)
        {
            int fieldLength = inputField.Length;
            if (Regex.IsMatch(buttonContent, @"[\+\-\/\*]"))
            {
                if (fieldLength > 0 && Char.IsNumber(inputField[fieldLength - 1]))
                {
                    return buttonContent;
                } else
                {
                    return "";
                }
                
            }

            return buttonContent;

        }
    }
}
