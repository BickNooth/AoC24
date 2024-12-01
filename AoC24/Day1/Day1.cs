namespace AoC24.Day1;

public class Part1
{
    private List<int> FirstLocation { get; }
    private List<int> SecondLocation { get; }

    private int Distance { get; set; }

    public Part1(string fileName)
    {
        (FirstLocation, SecondLocation) = Inputs.ParseFile(fileName);
    }

    public void Run()
    {
        FirstLocation.Sort();
        SecondLocation.Sort();
        PairHistoricLocations();
        Console.WriteLine(Distance);
    }

    private void PairHistoricLocations()
    {
        while (true)
        {
            var values = new List<int> { FirstLocation[0], SecondLocation[0] }.Order().ToArray();

            Distance += Math.Abs(values[1] - values[0]);

            FirstLocation.RemoveAt(0);
            SecondLocation.RemoveAt(0);

            if (FirstLocation.Count != 0) continue;
            break;
        }
    }
}

public class Part2
{
    private List<int> FirstLocation { get; }
    private List<int> SecondLocation { get; }

    private int SimilarityScore { get; set; }

    public Part2(string fileName)
    {
        (FirstLocation, SecondLocation) = Inputs.ParseFile(fileName);
    }

    public void Run()
    {
        CalculateSimilarityScore();
        Console.WriteLine(SimilarityScore);
    }

    private void CalculateSimilarityScore()
    {
        while (true)
        {
            var firstLocation = FirstLocation.ElementAt(0);
            var occurrences = SecondLocation.Count(l => l == firstLocation);
            SimilarityScore += occurrences * firstLocation;
            FirstLocation.RemoveAt(0);

            if (FirstLocation.Count != 0) continue;
            break;
        }
    }
}

public class Inputs
{
    public static (List<int> FirstLocation, List<int> SecondLocation) ParseFile(string fileName)
    {
        var lines = File.ReadLines(fileName)
                        .Select(line => line.Split(' '))
                        .Where(parts => parts.Length >= 4)
                        .Select(locations => (First: int.Parse(locations[0]), Second: int.Parse(locations[3])))
                        .ToList();

        var firstLocation = lines.Select(x => x.First).ToList();
        var secondLocation = lines.Select(x => x.Second).ToList();

        return (firstLocation, secondLocation);
    }
}