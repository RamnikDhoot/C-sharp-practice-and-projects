using System;

class Program
{
    static double? memoryValue = null;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Enhanced Calculator!");
        Console.WriteLine("Supported operations: +, -, *, /, ^ (exponent), % (modulo)");
        Console.WriteLine("Special commands: 'exit' to close, 'mem' to use stored memory, 'clear' to clear memory.");

        while (true)
        {
            try
            {
                Console.WriteLine("\nEnter the first number, 'mem' to use memory, or 'exit' to close:");
                string firstInput = Console.ReadLine().ToLower();
                if (firstInput == "exit") break;
                if (firstInput == "clear") { memoryValue = null; continue; }

                double num1 = firstInput == "mem" && memoryValue.HasValue ? memoryValue.Value : GetValidNumber(firstInput);

                double num2 = GetValidNumber("Enter the second number (or 'mem' to use memory):", true);

                char operation = GetValidOperation();

                double result = PerformCalculation(num1, num2, operation);
                Console.WriteLine("Result: " + result);

                memoryValue = result; // Store the result in memory after every successful calculation
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    static double GetValidNumber(string input, bool allowMemory = false)
    {
        if (allowMemory && input.ToLower() == "mem" && memoryValue.HasValue)
        {
            return memoryValue.Value;
        }
        else if (double.TryParse(input, out double number))
        {
            return number;
        }
        else
        {
            throw new FormatException("Invalid number. Please enter a valid number.");
        }
    }

    static char GetValidOperation()
    {
        while (true)
        {
            Console.WriteLine("Enter operation (+, -, *, /, ^, %):");
            string input = Console.ReadLine();
            if (input.Length == 1 && "+-*/^%".Contains(input))
            {
                return input[0];
            }
            else
            {
                Console.WriteLine("Invalid operation. Please enter a valid operation.");
            }
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
