using System;

class Program
{
    static void Main(string[] args)
    {
        bool continueCalculating = true;
        while (continueCalculating)
        {
            try
            {
                double num1 = GetValidNumber("Enter the first number:");
                double num2 = GetValidNumber("Enter the second number:");
                Console.WriteLine("Enter operation (+, -, *, /):");
                char operation = Console.ReadLine()[0];

                double result = PerformCalculation(num1, num2, operation);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Do you want to perform another calculation? (yes/no)");
            continueCalculating = Console.ReadLine().ToLower() == "yes";
        }
    }

    static double GetValidNumber(string prompt)
    {
        double number;
        Console.WriteLine(prompt);
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Invalid input. Please enter a valid number:");
        }
        return number;
    }

    static double PerformCalculation(double num1, double num2, char operation)
    {
        switch (operation)
        {
            case '+':
                return num1 + num2;
            case '-':
                return num1 - num2;
            case '*':
                return num1 * num2;
            case '/':
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Division by zero is not allowed.");
                }
                return num1 / num2;
            default:
                throw new InvalidOperationException("Invalid operation.");
        }
    }
}
