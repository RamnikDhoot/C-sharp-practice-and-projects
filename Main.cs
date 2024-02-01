using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Simple Calculator!");
        Console.WriteLine("Supported operations: +, -, *, /, ^ (exponent), % (modulo)");
        Console.WriteLine("Type 'exit' to close the calculator.");

        while (true)
        {
            try
            {
                Console.WriteLine("\nEnter the first number or 'exit' to close:");
                string input = Console.ReadLine().ToLower();
                if (input == "exit") break;

                double num1 = GetValidNumber(input);
                double num2 = GetValidNumber("Enter the second number:");

                Console.WriteLine("Enter operation:");
                char operation = GetValidOperation(Console.ReadLine());

                double result = PerformCalculation(num1, num2, operation);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    static double GetValidNumber(string input)
    {
        if (double.TryParse(input, out double number))
        {
            return number;
        }
        else
        {
            throw new FormatException("Invalid number. Please enter a valid number.");
        }
    }

    static char GetValidOperation(string input)
    {
        if (input.Length == 1 && "+-*/^%".Contains(input))
        {
            return input[0];
        }
        else
        {
            throw new FormatException("Invalid operation. Please enter a valid operation.");
        }
    }

    static double PerformCalculation(double num1, double num2, char operation)
    {
        return operation switch
        {
            '+' => num1 + num2,
            '-' => num1 - num2,
            '*' => num1 * num2,
            '/' => num2 == 0 ? throw new DivideByZeroException("Division by zero is not allowed.") : num1 / num2,
            '^' => Math.Pow(num1, num2),
            '%' => num1 % num2,
            _ => throw new InvalidOperationException("Invalid operation."),
        };
    }
}
