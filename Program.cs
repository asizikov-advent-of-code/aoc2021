var day = new Day15();
Console.WriteLine(day.First(File.ReadAllLines("inputs/14-example.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/14-example.txt")));
Console.WriteLine(day.First(File.ReadAllLines("inputs/14-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/14-01.txt")));

public class Day15
{
    public long First(IEnumerable<string> input) => 0;
    public long Second(IEnumerable<string> input) => 0;
}