using System;
using System.Collections.Generic;
using System.Numerics;

class Program
{
    static double? memoryValue = null;
    static List<string> history = new List<string>();
    static Dictionary<string, Func<double, double>> userDefinedMacros = new Dictionary<string, Func<double, double>>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Super Advanced Calculator!");
        Console.WriteLine("Type 'help' for instructions on operations and special commands.");

        while (true)
        {
            Console.WriteLine("\nEnter command ('help', 'exit', 'mem', 'clear', 'history', 'define') or the first number:");
            string input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "exit":
                    return;
                case "clear":
                    memoryValue = null;
                    Console.WriteLine("Memory cleared.");
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
                case "mem":
                    Console.WriteLine("Memory is empty.");
                    continue;
                default:
                    ProcessInput(input);
                    continue;
            }
        }
    }

    static void ProcessInput(string input)
    {
        if (input.StartsWith("define"))
        {
            DefineMacro(input);
        }
        else
        {
            try
            {
                Console.WriteLine("Assuming direct calculation or macro invocation...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


    static void DefineMacro(string input)
{
    var parts = input.Split(new[] { '=' }, 2);
    if (parts.Length != 2 || !parts[0].StartsWith("define"))
    {
        Console.WriteLine("Invalid macro definition syntax. Expected format: 'define macroName = expression'");
        return;
    }

    string macroName = parts[0].Substring("define".Length).Trim();
    string expression = parts[1].Trim();

    if (expression.StartsWith("x +"))
    {
        double numberToAdd = double.Parse(expression.Substring(3).Trim());
        userDefinedMacros[macroName] = x => x + numberToAdd;
        Console.WriteLine($"Macro '{macroName}' defined.");
    }
    else
    {
        Console.WriteLine("Unsupported macro expression. Currently, only 'x + number' is supported.");
    }
}


    static void DisplayHelp()
    {
        Console.WriteLine("Help information...");
    }

}

static void ProcessInput(string input)
{
    if (userDefinedMacros.ContainsKey(input.Split(' ')[0]))
    {
        var parts = input.Split(' ');
        if (parts.Length == 2 && double.TryParse(parts[1], out double num))
        {
            double result = userDefinedMacros[parts[0]].Invoke(num);
            Console.WriteLine($"Macro result: {result}");
            history.Add($"{parts[0]}({num}) = {result}");
            memoryValue = result;
        }
        else
        {
            Console.WriteLine("Invalid macro usage. Expected format: 'macroName number'");
        }
    }
    else if (input.Contains(" "))
    {
        Console.WriteLine("Direct calculation functionality to be implemented...");
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid calculation, macro invocation, or command.");
    }
}

