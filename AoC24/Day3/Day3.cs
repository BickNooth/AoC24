using System.Text.RegularExpressions;

namespace AoC24.Day3;

public class Day3
{
    private const string InputFilePath = "Day3\\input.txt";
    private const string DoOrDontPattern = @"(do\(\))|(don't\(\))";
    private const string MultiplesPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

    public void Part1()
    {
        var memory = File.ReadAllText(InputFilePath);
        var total = ProcessInstructions(memory);
        Console.WriteLine("Total: " + total);
    }

    public void Part2()
    {
        var memory = File.ReadAllText(InputFilePath);
        var filteredMemory = FilterMemory(memory);
        var total = filteredMemory.Sum(ProcessInstructions);
        Console.WriteLine("Total: " + total);
    }

    private static int ProcessInstructions(string memory)
    {
        var matches = Regex.Matches(memory, MultiplesPattern);

        var executedInstruction = 0;
        foreach (Match match in matches)
        {
            executedInstruction +=
                (Convert.ToInt32(match.Groups[1].Value) *
                 Convert.ToInt32(match.Groups[2].Value));
        }

        Console.WriteLine("Executed Instructions; " + executedInstruction);
        return executedInstruction;
    }

    private static IEnumerable<string> FilterMemory(string memory)
    {
        var matches = Regex.Matches(memory, DoOrDontPattern);
        var result = new List<string>();
        var capture = true;
        var lastIndex = 0;

        foreach (Match match in matches)
        {
            var part = memory[lastIndex..match.Index];
            if (capture)
                result.Add(part);

            capture = match.Value switch
            {
                "don't()" => false,
                "do()" => true,
                _ => capture
            };

            lastIndex = match.Index + match.Length;
        }

        if (capture && lastIndex < memory.Length)
        {
            result.Add(memory[lastIndex..]);
        }

        return result;
    }
}