﻿using System.Xml.XPath;

namespace Calculator;

public partial class SecondPage : ContentPage
{

    public SecondPage()
    {
        InitializeComponent();
        OnClear(this, null);

    }

    string currentEntry = "";
    string mathExpression = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";

    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;
        mathExpression += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            this.resultText.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.resultText.Text += pressed;
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        mathExpression += pressed;
        mathOperator = pressed;
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        this.CurrentCalculation.Text = "";
        currentEntry = string.Empty;
        mathExpression = "";
    }

    void OnSelectSquare(object sender, EventArgs e) {

    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (e == null)
        {
            if (secondNumber == 0)
                LockNumberValue(resultText.Text);
            double result;
            if (mathOperator == "√")
            {
                result = Calculator.CalculateSquareRoot(secondNumber, mathOperator);
                this.CurrentCalculation.Text = $"{mathOperator} {secondNumber}";
            }
            else
            {

                result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);
                this.CurrentCalculation.Text = $"{firstNumber} {mathOperator} {secondNumber}";
            }
            this.resultText.Text = result.ToTrimmedString(decimalFormat);
            firstNumber = result;
            secondNumber = 0;
            currentState = -1;
            currentEntry = string.Empty;
        }
        else {

            try
            {
                double result = Calculator.EvaluateMathExpression(mathExpression);
                resultText.Text = result.ToString();
                this.CurrentCalculation.Text = mathExpression;
                currentEntry = "";
            }
            catch
            {
                resultText.Text = "Invalid";
                currentEntry = "";
                this.CurrentCalculation.Text = mathExpression;
                mathExpression = "";
            }
        }
    }

    void OnNegative(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            secondNumber = -1;
            mathOperator = "×";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "×";
            currentState = 2;
            OnCalculate(this, null);
        }
    }
}