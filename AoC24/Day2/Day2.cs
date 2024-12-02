namespace AoC24.Day2;

public record Report(List<int> Levels);

public class Part1
{
    private List<Report> Reports { get; }

    public Part1(string fileName)
    {
        Reports = Inputs.ParseFile(fileName);
    }

    public void Run()
    {
        var safeReports = Reports.Count(
            report => CheckIncreaseOrDecrease(report) && CheckDifferences(report));

        Console.WriteLine("Safe Reports: "+safeReports);
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

public class Part2
{
    private List<Report> Reports { get; }

    public Part2(string fileName)
    {
        Reports = Inputs.ParseFile(fileName);
    }

    public void Run()
    {
        var safeReports = 0;
        foreach (var report in Reports)
        {
            var passedDifference = Part1.CheckDifferences(report);
            var passedIncrease = Part1.CheckIncreaseOrDecrease(report);

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

                    var passedDifferenceDampened = Part1.CheckDifferences(newReport);
                    var passedIncreaseDampened = Part1.CheckIncreaseOrDecrease(newReport);
                    
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