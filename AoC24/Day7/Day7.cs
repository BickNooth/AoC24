namespace AoC24.Day7;

public class Day7
{
    private List<(long target, int[] numbers)> _lines = new();
    public void Part1()
    {
        //ParseFile("Day7\\example.txt");
        ParseFile("Day7\\input.txt");

        long testValues = 0;
        foreach (var line in _lines)
        {
            var workingEquation = InitialTestEquation(line.target, line.numbers);
            if (workingEquation) testValues += line.target;
        }

        Console.WriteLine(testValues);
    }

    public void Part2()
    {
        //ParseFile("Day7\\example.txt");
        ParseFile("Day7\\input.txt");

        long testValues = 0;
        foreach (var line in _lines)
        {
            var workingEquation = InitialTestEquation(line.target, line.numbers, true);
            if (workingEquation) testValues += line.target;
        }

        Console.WriteLine(testValues);
    }

    private bool InitialTestEquation(long target, int[] numbers, bool withConcat = false)
    {
        // need to handle first instance so it's not multiplying by zero
        return TestEquation(target, numbers, 0, numbers[0], withConcat);
    }

    private bool TestEquation(long target, int[] numbers, int index, long currentSum, bool withConcat = false)
    {
        if (target < currentSum)
        {
            // only adding and multiplying so return early
            return false;
        }

        if (numbers.Length == index + 1)
        {
            // final number so evaluate
            return target == currentSum;
        }

        // else recurse over operators again
        var added = currentSum + numbers[index + 1];
        var multiplied = currentSum * numbers[index + 1];

        if (TestEquation(target, numbers, index + 1, added, withConcat))
        {
            return true;
        }

        if (TestEquation(target, numbers, index + 1, multiplied, withConcat))
        {
            return true;
        }

        if (withConcat)
        {
            var concatenated = long.Parse($"{currentSum}{numbers[index + 1]}");
            if (TestEquation(target, numbers, index + 1, concatenated, withConcat))
            {
                return true;
            }
        }

        return false;
    }

    private void ParseFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            var targetValue = long.Parse(parts[0].Trim());
            var integers = parts[1].Trim().Split(' ').Select(int.Parse).ToArray();
            _lines.Add((targetValue, integers));
        }
    }

    public enum Operator
    {
        Add,
        Multiply
    }
}
