using System.Text;
using Days.Common;
using Days.Enums;

namespace Days.Models;

public class Lab
{
    public Grid<char> Map { get; set; } = new() { DefaultValue = '.' };
    public Coordinate Guard { get; set; } = new(0, 0);
    private Dictionary<string, Direction> Path = [];

    public double Width { get; set; }
    public double Height { get; set; }

    public Lab(string filename)
    {
        var y = 0;
        foreach (var line in AdventDay.ReadFromFile(filename))
        {
            Width = line.Length > Width ? line.Length : Width;
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '.')
                {
                    continue;
                }
                if (line[x] == '^')
                {
                    Guard = new Coordinate(x, y);
                    continue;
                }
                Map.Set(new Coordinate(x, y), line[x]);
            }
            y++;
        }
        Height = y;
    }

    public int GetObstacleCount()
    {
        var count = 0;

        var path = IdentifyPath(Guard.X, Guard.Y);

        foreach (var point in path)
        {
            Map.Set(point.Key, '#');
            if (IsLoop(Guard.X, Guard.Y))
            {
                count++;
            }
            
            Map.Remove(point.Key);
        }

        return count;
    }

    public int GetDistinctPositionCount()
    {
        Path = IdentifyPath(Guard.X, Guard.Y);
        return Path.Count;
    }

    public Dictionary<string, Direction> IdentifyPath(double x, double y)
    {
        var path = new Dictionary<string, Direction>();
        var direction = Direction.Up;
        var guard = new Coordinate(x, y);
        
        do
        {
            if (!path.ContainsKey(guard.ToString()))
            {
                path.Add(guard.ToString(), direction);
            }
            var peek = guard.Peek(direction);
            if (Map.Get(peek) == '.')
            {
                guard.Move(direction);
                continue;
            }
            direction = TurnRight(direction);
        } while (InsideLab(guard));
        return path;
    }

    public bool IsLoop(double x, double y)
    {
        var path = new Dictionary<string, Direction>();
        var direction = Direction.Up;
        var guard = new Coordinate(x, y);
        var samePathCount = 0;
        
        do
        {
            if (!path.ContainsKey(guard.ToString()))
            {
                samePathCount = 0;
                path.Add(guard.ToString(), direction);
            }
            else
            {
                if (path[guard.ToString()] == direction)
                {
                    samePathCount++;
                }
            }
            var peek = guard.Peek(direction);
            if (Map.Get(peek) == '.')
            {
                guard.Move(direction);
                continue;
            }
            direction = TurnRight(direction);
        } while (InsideLab(guard) && samePathCount <= 10);

        return samePathCount > 10;
    }

    public bool InsideLab(Coordinate pos)
    {
        return !(pos.X < 0 || pos.X >= Width) && !(pos.Y < 0 || pos.Y >= Height);
    }

    private static Direction TurnRight(Direction direction)
        => direction switch {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentException("Cannot turn right..."),
        };
}