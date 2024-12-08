namespace AoC24.Day6;

public class Day6
{
    private char[,] _map = null!;
    private int _rows;
    private int _cols;

    private (char cursor, (int row, int col) direction)[] _movements =
    [
        ('^', (-1, 0)),
        ('>', (0, 1)),
        ('v', (1, 0)),
        ('<', (0, -1))
    ];

    //private const string FileName = "example.txt";
    private const string FileName = "input.txt";

    private int _movesMade = 0;
    private List<char[,]> maps = [];
    private bool _breakLoop = false;

    public void Part1()
    {
        ParseFile("Day6\\"+FileName);

        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _cols; j++)
            {
                foreach (var direction in _movements)
                {
                    if (_map[i, j] == direction.cursor)
                    {
                        MoveGuard(_map, i, j, direction);
                        break;
                    }
                }
            }
        }

        Console.WriteLine("final:");
        PrintMap(_map);
        int count = 0;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                if (_map[i, j] == 'X')
                {
                    count++;
                }
            }
        }
        Console.WriteLine($"Number of 'X' in the map: {count+1}");
    }

    public void Part2()
    {
        ParseFile("Day6\\" + FileName);

        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _cols; j++)
            {
                var newMap = new char[_rows, _cols];
                Array.Copy(_map, newMap, _map.Length);
                var curCar = newMap[i, j];
                if (curCar != '#' && curCar != '^')
                {
                    newMap[i, j] = '#';
                    maps.Add(newMap);
                }
            }
        }

        var loopsFound = 0;
        for (var index = 0; index < maps.Count; index++)
        {
            var map = maps[index];
            Console.WriteLine($"Map {index} of {maps.Count}");
            _movesMade = 0;
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    foreach (var direction in _movements)
                    {
                        if (map[i, j] == direction.cursor)
                        {
                            MoveGuard(map, i, j, direction);
                            break;
                        }
                    }
                }
            }

            if (_breakLoop)
            {
                _breakLoop = false;
                loopsFound++;
            }
        }

        Console.WriteLine($"Temporaly insertable objects: {loopsFound}");
    }

    private void MoveGuard(char[,] map, int currentRow, int currentCol, (char cursor, (int row, int col) direction) movement)
    {
        //Console.WriteLine("Before:");
        //PrintMap();
        _movesMade++;
        if (_movesMade == 7300 || _breakLoop)
        {
            _breakLoop = true;
            return;
        }

        var nextRow = currentRow + movement.direction.row;
        var nextCol = currentCol + movement.direction.col;
        if (nextRow >= _rows || nextRow < 0 || nextCol >= _cols || nextCol < 0)
        {
            return;
        }

        var currentChar = movement.cursor;
        var nextLocation = map[nextRow, nextCol];
        if (nextLocation == '#')
        {
            var nextChar = Turn90(currentChar);
            map[currentRow, currentCol] = nextChar;
            nextRow = currentRow;
            nextCol = currentCol;
        }
        else
        {
            map[nextRow, nextCol] = currentChar;
            map[currentRow, currentCol] = 'X';
        }

        foreach (var direction in _movements)
        {
            if (map[nextRow, nextCol] != direction.cursor)
            {
                continue;
            }

            MoveGuard(map, nextRow, nextCol, direction);
            break;
        }
    }

    private char Turn90(char currentChar)
    {
        var movementIndex = Array.FindIndex(_movements, m => m.cursor == currentChar);
        var newMovementIndex = movementIndex + 1;
        
        if (newMovementIndex == _movements.Length)
        {
            newMovementIndex = 0;
        }

        return _movements[newMovementIndex].cursor;
    }

    private void PrintMap(char[,] map)
    {
        var rows = map.GetLength(0);
        var columns = map.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine(); // Move to the next line after each row
        }
    }

    private void ParseFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        _rows = lines.Length;
        _cols = lines[0].Length;
        _map = new char[_rows, _cols];

        Enumerable.Range(0, _rows).ToList()
                  .ForEach(i =>
                    Enumerable.Range(0, _cols).ToList()
                     .ForEach(j =>
                        _map[i, j] = lines[i][j]));
    }
}
