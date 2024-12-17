using Days.Common;
using Days.Enums;
using Days.Models;

namespace Days;

public static class Day4
{
    public static void Run()
    {
        PartOne("./Days/Data/4.S.txt");
        PartOne("./Days/Data/4.txt");
        PartTwo("./Days/Data/4.S.txt");
        PartTwo("./Days/Data/4.txt");
    }

    public static void PartTwo(string filename)
    {
        var grid = GetGrid(filename);
        var total = 0;

        for (double y = grid.TopLeft.Y; y <= grid.BottomRight.Y; y++)
        {
            for (double x = grid.TopLeft.X; x <= grid.BottomRight.X; x++)
            {
                total += HasMas(grid, x, y) ? 1 : 0;
            }
        }
        Console.WriteLine(total);
    }

    public static bool HasMas(Grid<char> grid, double x, double y)
    {
        var coord = new Coordinate(x, y);
        if (grid.Get(coord) != 'A')
        {
            return false;
        }

        var topLeft = new Coordinate(coord, Direction.UpLeft);
        var topRight = new Coordinate(coord, Direction.UpRight);
        var bottomRight = new Coordinate(coord, Direction.DownRight);
        var bottomLeft = new Coordinate(coord, Direction.DownLeft);

        if (grid.Get(topLeft) == 'M' && grid.Get(topRight) == 'M' && grid.Get(bottomLeft) == 'S' && grid.Get(bottomRight) == 'S')
        {
            return true;
        }
        if (grid.Get(topLeft) == 'M' && grid.Get(topRight) == 'S' && grid.Get(bottomLeft) == 'M'  && grid.Get(bottomRight) == 'S')
        {
            return true;
        }
        if (grid.Get(topLeft) == 'S' && grid.Get(topRight) == 'S' && grid.Get(bottomLeft) == 'M' &&  grid.Get(bottomRight) == 'M')
        {
            return true;
        }
        if (grid.Get(topLeft) == 'S' && grid.Get(topRight) == 'M' && grid.Get(bottomLeft) == 'S' && grid.Get(bottomRight) == 'M')
        {
            return true;
        }
        return false;
    }

    public static void PartOne(string filename)
    {
        var grid = GetGrid(filename);
        var word = "XMAS";
        var total = 0;

        for (double y = grid.TopLeft.Y; y <= grid.BottomRight.Y; y++)
        {
            for (double x = grid.TopLeft.X; x <= grid.BottomRight.X; x++)
            {
                total += CountXmases(grid, x, y, word);
            }
        }
        Console.WriteLine(total);
    }

    public static int CountXmases(Grid<char> grid, double x, double y, string word)
    {
        var count = 0;
        foreach (var direction in Enum.GetValues<Direction>())
        {
            if (ContainsWord(grid, x, y, word, direction))
            {
                count++;
            }
        }
        
        return count;
    }

    private static bool ContainsWord(Grid<char> grid, double x, double y, string word, Direction direction)
    {
        var coord = new Coordinate(x, y);
        if (grid.Get(coord) != word[0])
        {
            return false;
        }
        foreach (var letter in word)
        {
            if (grid.Get(coord) != letter) {
                return false;
            }
            Coordinate.Move(coord, direction);
        }
        return true;
    }

    public static Grid<char> GetGrid(string filename)
    {
        var grid = new Grid<char>()
        {
            DefaultValue = '.'
        };

        var y = 0;
        foreach (var line in AdventDay.ReadFromFile(filename))
        {
            var x = 0;
            foreach (var col in line)
            {
                grid.Set(x, y, col);
                x++;
            }
            y++;
        }

        return grid;
    }
}