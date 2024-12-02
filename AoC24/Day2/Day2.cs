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

    private bool CheckDifferences(Report report)
    {
        var levels = report.Levels;
        return levels.Zip(levels.Skip(1), (a, b) => 
                            Math.Abs(a - b) >= 1 && Math.Abs(a - b) <= 3)
                     .All(x => x);
    }

    private bool CheckIncreaseOrDecrease(Report report)
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
            var (passedDifferences, removedValueDifferences) = CheckDifferences(report);
            var (passedUpOrDown, removedUpOrDown) = CheckIncreaseOrDecrease(report);
            if (passedDifferences && passedUpOrDown)
            {
                var removals = Convert.ToInt32(removedValueDifferences) + Convert.ToInt32(removedUpOrDown);
                if (removals < 2)
                {
                    safeReports++;
                }
            }
        }

        Console.WriteLine("Safe Reports: " + safeReports);
    }

    private (bool, bool) CheckDifferences(Report report)
    {
        var levels = report.Levels;
        var differences= levels.Zip(levels.Skip(1), (a, b) =>
                        Math.Abs(a - b) >= 1 && Math.Abs(a - b) <= 3).ToArray();
        var passes = differences.Count(x => x);
        var limit = differences.Length - 1;
        return (passes >= limit, passes != limit);
    }

    private (bool, bool) CheckIncreaseOrDecrease(Report report)
    {
        var levels = report.Levels;

        var increasings = levels.Zip(levels.Skip(1), (a, b) => a <= b).ToArray();
        var passesIncrease = increasings.Count(x => x);
        var limitIncrease = increasings.Length - 1;
        if(passesIncrease >= limitIncrease)
        {
            return (passesIncrease >= limitIncrease, passesIncrease != limitIncrease);
        }

        var decreasings = levels.Zip(levels.Skip(1), (a, b) => a >= b).ToArray();
        var passesDecrease = decreasings.Count(x => x);
        var limitDecrease = decreasings.Length - 1;
        return (passesDecrease >= limitDecrease, passesDecrease != limitDecrease);
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