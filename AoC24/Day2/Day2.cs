namespace AoC24.Day2;

public record Report(List<int> Levels);

public class Day2
{
    private const string FileName = "input.txt";
    private List<Report> Reports { get; set; } = null!;

    public void Part1()
    {
        Reports = Inputs.ParseFile(FileName);
        var safeReports = Reports.Count(
            report => CheckIncreaseOrDecrease(report) && CheckDifferences(report));

        Console.WriteLine("Safe Reports: "+safeReports);
    }

    public void Part2()
    {
        Reports = Inputs.ParseFile(FileName);

        var safeReports = 0;
        foreach (var report in Reports)
        {
            var passedDifference = CheckDifferences(report);
            var passedIncrease = CheckIncreaseOrDecrease(report);

            if (passedDifference && passedIncrease)
            {
                safeReports++;
            }
            else
            {
                for (var i = 0; i < report.Levels.Count(); i++)
                {
                    var levelsCopy = report.Levels.ToList();
                    levelsCopy.RemoveAt(i);
                    var newReport = new Report(levelsCopy);

                    var passedDifferenceDampened = CheckDifferences(newReport);
                    var passedIncreaseDampened = CheckIncreaseOrDecrease(newReport);

                    if (passedDifferenceDampened && passedIncreaseDampened)
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine("Safe Reports: " + safeReports);
    }

    public static bool CheckDifferences(Report report)
    {
        var levels = report.Levels;
        return levels.Zip(levels.Skip(1), (a, b) => 
                            Math.Abs(a - b) >= 1 && Math.Abs(a - b) <= 3)
                     .All(x => x);
    }

    public static bool CheckIncreaseOrDecrease(Report report)
    {
        var levels = report.Levels;
        var isIncreasing = levels.Zip(levels.Skip(1), (a, b) => a <= b).All(x => x);
        var isDecreasing = levels.Zip(levels.Skip(1), (a, b) => a >= b).All(x => x);
        return isIncreasing || isDecreasing;
    }
}

public class Inputs
{
    public static List<Report> ParseFile(string fileName)
    {
        var lines = File.ReadLines(fileName)
                        .Select(line => line.Split(' '))
                        .Select(parts => new Report(parts.Select(int.Parse).ToList()))
                        .ToList();

        return lines;
    }
}