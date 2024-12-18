using System.Security.Cryptography.X509Certificates;
using Days.Common;
using Days.Enums;
using Days.Models;

namespace Days;

public static class Day14
{
    public static void Run()
    {
        PartOne("./Days/Data/14.S.txt", 11, 7);
        PartOne("./Days/Data/14.txt", 101, 103);
        PartTwo("./Days/Data/14.txt", 101, 103);
    }

    public static void PartTwo(string filename, int width, int height)
    {
        var bathroom = new Bathroom(filename, width, height);
        double steps = 0;

        var found = false;
        do
        {
            steps++;

            foreach (var robot in bathroom.Robots)
            {
                bathroom.MoveRobot(robot.Value);
            }

            if (IsMirror(bathroom))
            {
                found = true;
                Console.WriteLine(bathroom);
                Console.WriteLine(steps);
            }
        } while (!found);
    }


    private static bool IsMirror(Bathroom bathroom)
    {
        var segments = 2;
        var midW = bathroom.Width / segments;
        var midH = bathroom.Height / segments;
        Dictionary<string, int> totals = [];

        foreach (var robot in bathroom.Robots)
        {
            if (bathroom.GetRobotCount(robot.Value.Position) > 1)
            {
                return false;
            }
        }

        return true;
    }

    public static void PartOne(string filename, int width, int height)
    {
        var bathroom = new Bathroom(filename, width, height);
        Console.WriteLine(bathroom);

        for (int i = 0; i < 100; i++)
        {
            foreach (var robot in bathroom.Robots)
            {
                bathroom.MoveRobot(robot.Value);
            }
        }

        Console.WriteLine(bathroom);
        Console.WriteLine(CalculateTotal(bathroom));
    }

    private static int CalculateTotal(Bathroom bathroom)
    {
        var segments = 2;
        var midW = bathroom.Width / segments;
        var midH = bathroom.Height / segments;
        Dictionary<string, int> totals = [];

        foreach (var robot in bathroom.Robots)
        {
            if (robot.Value.Position.X == midW)
            {
                continue;
            }
            if (robot.Value.Position.Y == midH)
            {
                continue;
            }

            var segX = robot.Value.Position.X > midW ? 1 : 0;
            var segY = robot.Value.Position.Y > midH ? 1 : 0;
            var key = $"{segX}:{segY}";
            if (!totals.ContainsKey(key))
            {
                totals[key] = 0;
            }
            totals[key] += 1;
        }

        var sum = 1;
        foreach (var total in totals)
        {
            sum *= total.Value;
        }
        return sum;
    }
}