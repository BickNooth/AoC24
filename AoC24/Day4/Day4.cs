using System;

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

        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _cols; j++)
            {
                // foreach character in wordsearch
                try
                {
                    CheckForXMAS(i, j);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
        }

        Console.WriteLine(_wordsFound);
    }

    public void Part2()
    {
        ParseFile("Day4\\" + FileName);

        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _cols; j++)
            {
                // foreach character in wordsearch
                try
                {
                    CheckForCrossMAS(i, j);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
        }

        Console.WriteLine(_wordsFound);
    }


    public void CheckForXMAS(int row, int col)
    {
        // Possible directions of traversal
        var directions = new[]
                         {
                             (-1, -1), (-1, 0), (-1, 1),
                             (0, -1),          (0, 1),
                             (1, -1), (1, 0), (1, 1)
                         };

        foreach (var (dr, dc) in directions)
        {
            // Foreach direction, check if XMAS is present
            if (IsXMAS(row, col, dr, dc))
            {
                _wordsFound++;
            }
        }
    }

    private bool IsXMAS(int row, int col, int dr, int dc)
    {
        var word = "XMAS";
        // Start by checking itself so i = 0
        for (var i = 0; i < 4; i++)
        {
            var newRow = row + i * dr;
            var newCol = col + i * dc;
            if (newRow < 0 || newRow >= _rows || newCol < 0 || newCol >= _cols || _wordsearch[newRow, newCol] != word[i])
            {
                // If out of bounds or character doesn't match, return false
                return false;
            }
        }
        // If not out of bounds and hasn't returned false, must be XMAS
        return true;
    }

    public void CheckForCrossMAS(int row, int col)
    {
        if (_wordsearch[row, col] != 'A')
        {
            return;
        }
        // Possible directions of traversal limited to X
        var directions = new (int row, int col)[]
                         {
                             (row-1, col-1),    (row-1, col+1),
                                        //Middle A
                             (row+1, col-1),    (row+1, col+1)
                         };
        var crosswaysMatch = 0;
        if ((_wordsearch[directions[0].row, directions[0].col] == 'S'
            && _wordsearch[directions[3].row, directions[3].col] == 'M')
            || _wordsearch[directions[0].row, directions[0].col] == 'M'
            && _wordsearch[directions[3].row, directions[3].col] == 'S')
        {
            // Check Leftways
            crosswaysMatch++;
        }

        if ((_wordsearch[directions[1].row, directions[1].col] == 'S'
             && _wordsearch[directions[2].row, directions[2].col] == 'M')
            || _wordsearch[directions[1].row, directions[1].col] == 'M'
            && _wordsearch[directions[2].row, directions[2].col] == 'S')
        {
            // Check Rightways
            crosswaysMatch++;
        }

        if (crosswaysMatch == 2)
        {
            _wordsFound++;
        }
    }


    public void ParseFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        _rows = lines.Length;
        _cols = lines[0].Length;
        _wordsearch = new char[_rows, _cols];

        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _cols; j++)
            {
                // Map to grid array
                _wordsearch[i, j] = lines[i][j];
            }
        }
    }
}
