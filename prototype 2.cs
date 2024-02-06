using System;
using System.Collections.Generic;
using System.Numerics;

class Program
{
    static double? memoryValue = null;
    static List<string> history = new List<string>();
    static Dictionary<string, Func<double, double, double>> userDefinedMacros = new Dictionary<string, Func<double, double, double>>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Super Advanced Calculator!");
        Console.WriteLine("Type 'help' for instructions on operations and special commands.");

        while (true)
        {
            try
            {
                Console.WriteLine("\nEnter command ('help', 'exit', 'mem', 'clear', 'history') or the first number:");
                string firstInput = Console.ReadLine().ToLower();

                switch (firstInput)
                {
                    case "exit":
                        return;
                    case "clear":
                        memoryValue = null;
                        continue;
                    case "history":
                        DisplayHistory();
                        continue;
                    case "help":
                        DisplayHelp();
                        continue;
                    case "mem" when memoryValue.HasValue:
                        Console.WriteLine($"Using memory value: {memoryValue.Value}");
                        break;
                }

                if (firstInput == "mem" && !memoryValue.HasValue)
                {
                    Console.WriteLine("Memory is empty.");
                    continue;
                }

                double num1 = firstInput == "mem" && memoryValue.HasValue ? memoryValue.Value : GetValidNumber(firstInput);
                string operation = GetValidOperation();

                double result;
                if (IsUnaryOperation(operation))
                {
                    result = PerformUnaryCalculation(num1, operation);
                }
                else if (userDefinedMacros.ContainsKey(operation))
                {
                    double num2 = GetValidNumber("Enter the second number (or 'mem' to use memory):", true);
                    result = userDefinedMacros[operation].Invoke(num1, num2);
                }
                else
                {
                    double num2 = GetValidNumber("Enter the second number (or 'mem' to use memory):", true);
                    result = PerformBinaryCalculation(num1, num2, operation);
                }

                Console.WriteLine("Result: " + result);
                memoryValue = result;
                history.Add($"{num1} {operation} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


    static void DisplayHelp()
    {
        Console.WriteLine("Available Commands:");
        Console.WriteLine("- 'exit': Close the calculator.");
        Console.WriteLine("- 'mem': Use the value stored in memory.");
        Console.WriteLine("- 'clear': Clear the memory.");
        Console.WriteLine("- 'history': View the calculation history.");
        Console.WriteLine("- 'help': Display this help menu.");
        Console.WriteLine("Supported Operations:");
        Console.WriteLine("- Basic arithmetic: +, -, *, /");
        Console.WriteLine("- Scientific: sin, cos, tan, sqrt, log");
        Console.WriteLine("- Advanced: ! (factorial), nPr (permutations), nCr (combinations)");
        Console.WriteLine("You can define custom macros using the 'define' command followed by the macro name and its operation.");
    }

}
