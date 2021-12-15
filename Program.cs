var day = new Day16();
Console.WriteLine("Example:");
Console.WriteLine(day.First(File.ReadAllLines("inputs/16-example.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/16-example.txt")));
Console.WriteLine("Test:");
Console.WriteLine(day.First(File.ReadAllLines("inputs/16-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/16-01.txt")));

public class Day16
{
    public long First(IEnumerable<string> input) => 0;
    public long Second(IEnumerable<string> input) => 0;
}