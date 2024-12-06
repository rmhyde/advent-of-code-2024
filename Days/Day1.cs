using Days.Common;

namespace Days;

public static class Day1
{
    public static void Run() {
        PartOne("./Days/Data/1.S.txt");
        PartOne("./Days/Data/1.1.txt");
        PartTwo("./Days/Data/1.S.txt");
        PartTwo("./Days/Data/1.1.txt");
    }


    private static void PartOne(string filename) {
        
        var lists = ParseInput(filename);
        var left = lists[0].Order().ToList();
        var right = lists[1].Order().ToList();

        long total = 0;
        for (int i = 0; i < lists[0].Count; i++)
        {
            total += Math.Abs(right[i] - left[i]);
        }
        Console.WriteLine(total);
    }

    private static void PartTwo(string filename) {
        var lists = ParseInput(filename);
        long total = 0;
        foreach(var number in lists[0])
        {
            total += number * lists[1].Count(x => number == x);
        }
        Console.WriteLine(total);
    }


    private static List<List<long>> ParseInput(string filename) {
        var file = AdventDay.ReadFromFile(filename);

        var lists = new List<List<long>>
        {
            ([]),
            ([])
        };

        foreach(var line in file)
        {
            var inputs = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 2; i++)
            {
                lists[i].Add(long.Parse(inputs[i]));
            }
        }
        return lists;
    }
}