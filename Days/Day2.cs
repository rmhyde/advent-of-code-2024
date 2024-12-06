using Days.Common;

namespace Days;

public static class Day2
{
    public static void Run()
    {
        Analyze("./Days/Data/2.S.txt");
        Analyze("./Days/Data/2.1.txt");
        Analyze("./Days/Data/2.S.txt", true);
        Analyze("./Days/Data/2.1.txt", true);
    }

    public static void Analyze(string filename, bool dampener = false)
    {
        var safe = 0;
        foreach (var report in AdventDay.ReadFromFile(filename))
        {
            var values = report.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse).ToList();
            
            if (IsSafeProgression(values)) {
                safe++;
                continue;
            }

            if (!dampener)
            {
                continue;
            }

            for (int i = 0; i < values.Count; i++)
            {
                var testVals = values.Take(i).ToList();
                testVals.AddRange(values.Skip(i+1));
                if (IsSafeProgression(testVals))
                {
                    safe++;
                    break;
                }
            }
        }
        Console.WriteLine(safe);
    }

    private static bool IsSafeProgression(List<long> values)
    {
        var maxDiff = 3;
        var minDiff = 1;
        var increasing = false;
        var decreasing = false;
        for (int i = 1; i < values.Count; i++)
        {
            var diff = values[i] - values[i-1];
            if (Math.Abs(diff) < minDiff || Math.Abs(diff) > maxDiff) {
                return false;
            }
            increasing |= diff > 0;
            decreasing |= diff < 0;
            if (increasing && decreasing) {
                return false;
            }
        }
        return true;
    }
}