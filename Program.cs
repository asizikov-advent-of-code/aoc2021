using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using aoc2021.Solvers;

var day = new Day07();
Console.WriteLine(day.First(File.ReadAllLines("inputs/07-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/07-01.txt")));

internal class Day07
{
    private static int FindMin(IEnumerable<string> input, Func<int, int, int, int> costFunc)
    {
        var crabs = input.First().Split(',').Select(int.Parse).ToList();
        var group = crabs.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        var (max, min) = (crabs.Max(), crabs.Min());

        return Enumerable.Range(min, max - min)
            .Select(targetPosition => group.Sum(g => costFunc(g.Value, g.Key, targetPosition)))
            .Prepend(int.MaxValue)
            .Min();
    }

    public int First(IEnumerable<string> input) =>
        FindMin(input,
            (groupSize, groupPosition, targetPosition) => Math.Abs(targetPosition - groupPosition) * groupSize);

    public int Second(IEnumerable<string> input) =>
        FindMin(input,
            (groupSize, groupPosition, targetPosition) =>
            {
                var dist = Math.Abs(targetPosition - groupPosition);
                return dist * (dist + 1) / 2 * groupSize;
            }
        );
}