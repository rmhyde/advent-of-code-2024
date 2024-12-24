using Days.Common;
using Days.Models;

namespace Days;

public static class Day6
{
    public static void Run()
    {
        PartTwo("./Days/Data/6.S.txt");
        PartTwo("./Days/Data/6.txt");
        PartOne("./Days/Data/6.S.txt");
        PartOne("./Days/Data/6.txt");
    }

    public static void PartOne(string filename)
    {
        var lab = new Lab(filename);
        var count = lab.GetDistinctPositionCount();
        Console.WriteLine(count);
    }

    public static void PartTwo(string filename)
    {
        var lab = new Lab(filename);
        var count = lab.GetCornerCount();
        Console.WriteLine(count);
    }
}