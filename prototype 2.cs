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

                double num1 = firstInput == "mem" && memoryValue.HasValue ? memoryValue.Value : GetValidNumber(firstInput);
                string operation = GetValidOperation();

                double result = 0;

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

                Console.WriteLine($"Result: {result}");
                memoryValue = result; // Store the result in memory
                history.Add($"{num1} {operation} {result}"); // Add to history
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
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
     static bool IsValidOperation(string operation)
    {
        return new List<string> { "+", "-", "*", "/", "^", "%", "sin", "cos", "tan", "sqrt", "log" }.Contains(operation);
    }

    static bool IsUnaryOperation(string operation)
    {
        return new List<string> { "sin", "cos", "tan", "sqrt", "log" }.Contains(operation);
    }

    static double PerformUnaryCalculation(double num, string operation)
{
    return operation switch
    {
        "sin" => Math.Sin(num),
        "cos" => Math.Cos(num),
        "tan" => Math.Tan(num),
        "sqrt" => num < 0 ? throw new ArgumentException("Cannot calculate the square root of a negative number.") : Math.Sqrt(num),
        "log" => num <= 0 ? throw new ArgumentException("Logarithm of non-positive numbers is undefined.") : Math.Log(num),
        "!" => Factorial(num),
        _ => throw new InvalidOperationException("Invalid operation."),
    };
    while (true)
{
    Console.Write("\nEnter command or calculation: ");
    string input = Console.ReadLine().ToLower();

    if (input == "exit") break;
    else if (input.StartsWith("define")) DefineMacro(input);
    else if (input == "help") DisplayHelp();
    
}
}

static double PerformBinaryCalculation(double num1, double num2, string operation)
{
    return operation switch
    {
        "+" => num1 + num2,
        "-" => num1 - num2,
        "*" => num1 * num2,
        "/" => num2 == 0 ? throw new DivideByZeroException("Division by zero is not allowed.") : num1 / num2,
        "^" => Math.Pow(num1, num2),
        "%" => num1 % num2,
        "nPr" => Permutations(num1, num2),
        "nCr" => Combinations(num1, num2),
        _ => throw new InvalidOperationException("Invalid operation."),
    };
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

    static double Factorial(double n)
{
    if (n < 0) throw new ArgumentException("Factorial of negative number is undefined.");
    if (n == 0) return 1;

    BigInteger result = 1;
    for (int i = 1; i <= n; i++)
    {
        result *= i;
    }
    return (double)result;
}

static double Permutations(double n, double r)
{
    if (n < 0 || r < 0 || n < r) throw new ArgumentException("Invalid arguments for permutations.");
    return Factorial(n) / Factorial(n - r);
}

static double Combinations(double n, double r)
{
    if (n < 0 || r < 0 || n < r) throw new ArgumentException("Invalid arguments for combinations.");
    return Factorial(n) / (Factorial(r) * Factorial(n - r));
}

static void DefineMacro(string macroName, Func<double, double, double> operation)
{
    if (!userDefinedMacros.ContainsKey(macroName))
    {
        userDefinedMacros.Add(macroName, operation);
    }
    else
    {
        Console.WriteLine($"Macro {macroName} is already defined. Overwriting.");
        userDefinedMacros[macroName] = operation;
    }
}

static void ProcessCommand(string command)
{
    if (command.StartsWith("define"))
    {
        Console.WriteLine("Macro defined successfully.");
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

    static void DefineMacro(string input)
{
    // Example input: "define add3 x + 3"
    var parts = input.Split(new[] { ' ' }, 4);
    if (parts.Length < 4 || parts[0] != "define")
    {
        Console.WriteLine("Invalid macro definition. Use: define [name] [expression]");
        return;
    }

    var macroName = parts[1];
    var operation = parts[3]; // Simplified: Directly use the operation string in this example

    // Storing the operation as a macro (simplified version for demonstration)
    if (!userDefinedMacros.ContainsKey(macroName))
    {
        userDefinedMacros[macroName] = (num1, _) => num1; // Placeholder for actual operation
        Console.WriteLine($"Macro '{macroName}' defined.");
    }
    else
    {
        Console.WriteLine($"Macro '{macroName}' is already defined.");
    }
}

static void ProcessCommand(string command)
{
    if (command.StartsWith("define"))
    {
        DefineMacro(command);
    }
    else
}


}
