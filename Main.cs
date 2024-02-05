using System;
using System.Collections.Generic;

class Program
{
    static double? memoryValue = null;
    static List<string> history = new List<string>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Advanced Calculator!");
        Console.WriteLine("Supported operations: +, -, *, /, ^, %, sin, cos, tan, sqrt, log");
        Console.WriteLine("Special commands: 'exit' to close, 'mem' to use stored memory, 'clear' to clear memory, 'history' to view past calculations.");

        while (true)
        {
            try
            {
                Console.WriteLine("\nEnter the first number, 'mem' to use memory, 'history' to view history, or 'exit' to close:");
                string firstInput = Console.ReadLine().ToLower();
                if (firstInput == "exit") break;
                if (firstInput == "clear") { memoryValue = null; continue; }
                if (firstInput == "history") { DisplayHistory(); continue; }

                if (firstInput == "mem" && memoryValue.HasValue)
                {
                    Console.WriteLine($"Using memory value: {memoryValue.Value}");
                }

                double num1 = firstInput == "mem" && memoryValue.HasValue ? memoryValue.Value : GetValidNumber(firstInput);

                string operation = GetValidOperation();

                double result;
                if (IsUnaryOperation(operation))
                {
                    result = PerformUnaryCalculation(num1, operation);
                }
                else
                {
                    double num2 = GetValidNumber("Enter the second number (or 'mem' to use memory):", true);
                    result = PerformBinaryCalculation(num1, num2, operation);
                }

                Console.WriteLine("Result: " + result);
                memoryValue = result; // Store the result in memory
                history.Add($"{num1} {operation} = {result}"); // Add to history
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    .WriteLine("No history yet.");
        }
        else
        {
            Console.WriteLine("Calculation History:");
            foreach (var entry in history)
            {
                Console.WriteLine(entry);
            }
        }
    }

    static bool IsValidOperation(string operation)
    {
        return new List<string> { "+", "-", "*", "/", "^", "%", "sin", "cos", "tan", "sqrt", "log" }.Contains(operation);
    }

    static bool IsUnaryOperation(string operation)
    {
        return new List<string> { "sin", "cos", "tan", "sqrt", "log" }.Contains(operation);
    }

    static double PerformBinaryCalculation(double num1, double num2, string operation)
    {
        switch (operation)
        {
            case "+":
                return num1 + num2;
            case "-":
                return num1 - num2;
            case "*":
                return num1 * num2;
            case "/":
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Division by zero is not allowed.");
                }
                return num1 / num2;
            case "^":
                return Math.Pow(num1, num2);
            case "%":
                return num1 % num2;
            default:
                throw new InvalidOperationException("Invalid operation.");
        }
    }

    static double PerformUnaryCalculation(double num, string operation)
    {
        switch (operation)
        {
            case "sin":
                return Math.Sin(num);
            case "cos":
                return Math.Cos(num);
            case "tan":
                return Math.Tan(num);
            case "sqrt":
                if (num < 0)
                {
                    throw new ArgumentException("Cannot calculate the square root of a negative number.");
                }
                return Math.Sqrt(num);
            case "log":
                if (num <= 0)
                {
                    throw new ArgumentException("Logarithm of non-positive numbers is undefined.");
                }
                return Math.Log(num);
            default:
                throw new InvalidOperationException("Invalid operation.");
        }
    }

    static string GetValidOperation()
    {
        while (true)
        {
            Console.WriteLine("Enter operation (+, -, *, /, ^, %, sin, cos, tan, sqrt, log):");
            string input = Console.ReadLine().ToLower();
            if (IsValidOperation(input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Invalid operation. Please enter a valid operation.");
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
}


    static void DisplayHistory()
    {
        if (history.Count == 0)
        {
            Console
