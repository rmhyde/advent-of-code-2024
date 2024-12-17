using System.ComponentModel;
using System.Text;
using Days.Common;

namespace Days.Models;
public class Bathroom
{
    private Grid<HashSet<string>> grid = new();
    public Dictionary<string, Robot> Robots = new();
    public int Width { get; set; }
    public int Height{ get; set; }

    public Bathroom(string filename, int width, int height)
    {
        Width = width;
        Height = height;
        grid.TopLeft = new Coordinate(0, 0);
        grid.BottomRight = new Coordinate(width, height);
        foreach(var line in AdventDay.ReadFromFile(filename))
        {
            var split = line.Split(" ");
            var pos = split[0][2..].Split(',');
            var vel = split[1][2..].Split(',');
            var robot = new Robot
            {
                Position = new Coordinate(int.Parse(pos[0]), int.Parse(pos[1])),
                Velocity = new Coordinate(int.Parse(vel[0]), int.Parse(vel[1]))
            };
            Robots.Add(robot.Id, robot);
            PlaceRobot(robot);
        }
    }

    public void PrintGrid()
    {
        foreach (var item in grid.Storage) {
            var items = string.Join(",", item.Value);
            Console.WriteLine($"{item.Key} - {items}");
        }
        Console.WriteLine();
    }
    public void MoveRobot(Robot robot)
    {
        PickupRobot(robot);

        robot.Position.X += robot.Velocity.X;
        robot.Position.Y += robot.Velocity.Y;

        if (robot.Position.X < 0) { robot.Position.X += Width; }
        if (robot.Position.X >= Width) { robot.Position.X -= Width; }
        if (robot.Position.Y < 0) { robot.Position.Y += Height; }
        if (robot.Position.Y >= Height) { robot.Position.Y -= Height; }
    
        PlaceRobot(robot);
    }

    public void PlaceRobot(Robot robot)
    {
            var current = grid.Get(robot.Position);
            current.Add(robot.Id);
            grid.Set(robot.Position, current);
    }

    public void PickupRobot(Robot robot)
    {
            var current = grid.Get(robot.Position);
            if (!current.Contains(robot.Id)) {
                throw new Exception("not here");
            }
            current.Remove(robot.Id);
            if (current.Count != 0) 
            { 
                grid.Set(robot.Position, current);
                return;
            }
            grid.Remove(robot.Position);
    }

    public int GetRobotCount(Coordinate coord)
    {
        return grid.Get(coord).Count;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var r = grid.Get(new Coordinate(x, y));
                sb.Append(r.Count == 0 ? "." : r.Count);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}