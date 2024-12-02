using System.Reflection;

namespace AoC24;

public class Program
{
    static void Main()
    {
        // Get the current date
        var today = DateTime.Today;
        var day = today.Day;
        var year = today.Year - 2000;

        // Construct the namespace and class names
        var namespaceName = $"AoC{year}.Day{day}";
        var dayClassName = $"{namespaceName}.Day{day}";

        // Load the assembly (assuming the current assembly)
        var assembly = Assembly.GetExecutingAssembly();

        // Get the type of the Day class
        var dayType = assembly.GetType(dayClassName);
        if (dayType == null)
        {
            Console.WriteLine($"Class {dayClassName} not found.");
            return;
        }

        // Create an instance of the Day class
        var instance = Activator.CreateInstance(dayType);

        // Check if Part2 method exists
        var part2Method = dayType.GetMethod("Part2");
        if (part2Method != null)
        {
            // Run Part2
            part2Method.Invoke(instance, null);
        }
        else
        {
            // Run Part1
            var part1Method = dayType.GetMethod("Part1");
            if (part1Method == null)
            {
                Console.WriteLine($"Part1 method not found in class {dayClassName}.");
                return;
            }
            part1Method.Invoke(instance, null);
        }

        Console.ReadKey();
    }
}