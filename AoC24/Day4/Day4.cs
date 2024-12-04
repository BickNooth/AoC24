namespace AoC24.Day4;

public class Day4
{
    private char[,] _wordsearch = null!;
    private int _rows;
    private int _cols;
    private int _wordsFound;

    //private const string FileName = "example.txt";
    private const string FileName = "input.txt";

    public void Part1()
    {
        ParseFile("Day4\\"+FileName);

        Enumerable.Range(0, _rows)
                  .SelectMany(_ => Enumerable.Range(0, _cols), (i, j) => new { i, j })
                  .ToList()
                  .ForEach(pos => CheckForXMAS(pos.i, pos.j));

        Console.WriteLine(_wordsFound);
    }

    public void Part2()
    {
        ParseFile("Day4\\" + FileName);

        Enumerable.Range(0, _rows)
                  .SelectMany(_ => Enumerable.Range(0, _cols), (i, j) => new { i, j })
                  .ToList()
                  .ForEach(pos => CheckForCrossMAS(pos.i, pos.j));

        Console.WriteLine(_wordsFound);
    }

    private void CheckForXMAS(int row, int col)
    {
        if (!IsInBounds(row, col))
        {
            return;
        }

        var directions = new (int row, int col)[]
                         {
                             (-1, -1), (-1, 0), (-1, 1),
                             (0, -1),          (0, 1),
                             (1, -1), (1, 0), (1, 1)
                         };

        _wordsFound += 
            directions
                .Count(direction => 
                    IsXMAS(row, col, direction.row, direction.col));
    }

    private bool IsXMAS(int row, int col, int dr, int dc)
    {
        var word = "XMAS";
        return Enumerable.Range(0, word.Length).All(i =>
        {
            var newRow = row + i * dr;
            var newCol = col + i * dc;
            return newRow >= 0 && newRow < _rows && newCol >= 0 && newCol < _cols 
                   && _wordsearch[newRow, newCol] == word[i];
        });
    }

    private void CheckForCrossMAS(int row, int col)
    {
        if (_wordsearch[row, col] != 'A' || !IsInBounds(row, col))
        {
            return;
        }

        var pairs = new[]
                    {
                        ((row-1, col-1), (row+1, col+1)),
                        ((row-1, col+1), (row+1, col-1))
                    };

        var crosswaysMatch =
            pairs.Count(pair =>
                            (_wordsearch[pair.Item1.Item1, pair.Item1.Item2] == 'S'
                             && _wordsearch[pair.Item2.Item1, pair.Item2.Item2] == 'M') ||
                            (_wordsearch[pair.Item1.Item1, pair.Item1.Item2] == 'M'
                             && _wordsearch[pair.Item2.Item1, pair.Item2.Item2] == 'S')
                       );

        if (crosswaysMatch == 2)
            _wordsFound++;
    }

    private bool IsInBounds(int row, int col)
    {
        return row > 0 && row < _rows - 1
            && col > 0 && col < _cols - 1;
    }

    private void ParseFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        _rows = lines.Length;
        _cols = lines[0].Length;
        _wordsearch = new char[_rows, _cols];

        Enumerable.Range(0, _rows).ToList()
                  .ForEach(i =>
                    Enumerable.Range(0, _cols).ToList()
                     .ForEach(j =>
                        _wordsearch[i, j] = lines[i][j]));
    }
}
