namespace Days.Common;

public static class AdventDay
{
    public static IEnumerable<string> ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            yield return string.Empty;
        }

        using var file = File.OpenRead(filename);
        using var sr = new StreamReader(file);

        while(!sr.EndOfStream)
        {
            yield return sr.ReadLine() ?? string.Empty;
        }
    }
}
