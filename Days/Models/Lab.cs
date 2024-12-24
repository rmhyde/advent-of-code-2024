using System.Text;
using Days.Common;
using Days.Enums;

namespace Days.Models;

public class Lab
{
    public Grid<char> Map { get; set; } = new() { DefaultValue = '.' };
    public Coordinate Guard {get; set; } = new(0, 0);
    private readonly HashSet<string> visited = [];
    private readonly Dictionary<string, Direction> path = [];
    private readonly List<string> corner = [];

    public double Width {get; set; }
    public double Height {get; set; }


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

    public int GetCornerCount()
    {
        var potentialLoopPoints = new List<string>();
        List<Coordinate> cornerList = [new Coordinate(Guard.X, Guard.Y)];
        

        var direction = Direction.Up;
        do
        {
            // Add to path (if not already there)
            if (!path.ContainsKey(Guard.ToString()))
            {
                path.Add(Guard.ToString(), direction);
            }


            var potentialObstacleAhead = false;


            // Crossed an existing path
            if (path[Guard.ToString()] == TurnRight(direction))
            {
                potentialObstacleAhead = true;
            }

            // Check everything to the right, if it comes across something that is already in the path and is going the same way then add obstacle
            var continueCheck = true;
            var ptr = new Coordinate(Guard.ToString());
            var tmpDir = TurnRight(direction);
            do
            {
                ptr.Move(tmpDir);
                if (path.ContainsKey(ptr.ToString()) && (path[ptr.ToString()] == tmpDir || path[ptr.ToString()] == TurnRight(tmpDir)))
                {
                    continueCheck = false;
                    potentialObstacleAhead = true;
                }
                if (Map.Get(ptr) != '.' || !InsideLab(ptr))
                {
                    continueCheck = false;
                }
            } while (continueCheck);

            if (potentialObstacleAhead)
            {
                var obstacle = new Coordinate(Guard.ToString());
                obstacle.Move(direction);
                if (Map.Get(obstacle) == '.')
                {
                    potentialLoopPoints.Add(obstacle.ToString());
                }
            }

            // Move Guard
            var peek = Guard.Peek(direction);
            if (Map.Get(peek) == '.')
            {
                Guard.Move(direction);
                continue;
            }
            direction = TurnRight(direction);
            path[Guard.ToString()] = direction;


        } while (InsideLab(Guard));

        var count = potentialLoopPoints.Distinct().Count();

        return count;
    }

    public int GetDistinctPositionCount()
    {
        visited.Clear();
        var direction = Direction.Up;
        do
        {
            visited.Add(Guard.ToString());
            var peek = Guard.Peek(direction);
            if (Map.Get(peek) == '.')
            {
                Guard.Move(direction);
                continue;
            }
            direction = TurnRight(direction);
        } while (InsideLab(Guard));

        return visited.Count;
    }

    public bool InsideLab(Coordinate pos)
    {
        return !(pos.X < 0 || pos.X >= Width) && !(pos.Y < 0 || pos.Y >= Height);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Guard.X == x && Guard.Y == y)
                {
                    sb.Append('G');
                    continue;
                }
                var c = new Coordinate(x, y);
                if (visited.Contains(c.ToString()))
                {
                    sb.Append('X');
                    continue;
                }
                sb.Append(Map.Get(c));
            }
            sb.AppendLine();
        }
        return sb.ToString();
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