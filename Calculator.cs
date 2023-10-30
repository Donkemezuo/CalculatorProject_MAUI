using System.Data;
using System.Diagnostics;
using System.Xml.XPath;
using NCalc;

namespace Calculator;

public static class Calculator
{
    public static double Calculate(double value1, double value2, string mathOperator)
    {
        double result = 0;
        switch (mathOperator)
        {
            case "÷":
                result = value1 / value2;
                break;
            case "×":
                result = value1 * value2;
                break;
            case "+":
                result = value1 + value2;
                break;
            case "-":
                result = value1 - value2;
                break;
            case "%":
                result = value1 % value2;
                break;
            case "^":
                result = Math.Pow(value1, value2);
                break;
        }

        return result;
    }

    public static double CalculateSquareRoot(double value, string mathOperator) {
        return Math.Sqrt(value);
    }

    public static double EvaluateMathExpression(string expression) {
        expression = expression.Replace('–', '-').Replace('×', '*');
        DataTable table = new DataTable();
        table.Columns.Add("expression", typeof(string), expression);
        DataRow row = table.NewRow();
        table.Rows.Add(row);
        double result = double.Parse((string)row["expression"]);
        return result;
    }
}

public static class DoubleExtensions
{
    public static string ToTrimmedString(this double target, string decimalFormat)
    {
        string strValue = target.ToString(decimalFormat); //Get the stock string

        //If there is a decimal point present
        if (strValue.Contains("."))
        {
            //Remove all trailing zeros
            strValue = strValue.TrimEnd('0');

            //If all we are left with is a decimal point
            if (strValue.EndsWith(".")) //then remove it
                strValue = strValue.TrimEnd('.');
        }

        return strValue;
    }
}