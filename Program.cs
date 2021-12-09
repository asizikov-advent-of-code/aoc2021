using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using aoc2021.Solvers;

var day = new Day09();
Console.WriteLine(day.First(File.ReadAllLines("inputs/09-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/09-01.txt")));

public class Day09
{
    private readonly (int dx, int dy)[] dirs = { (0, 1), (1, 0), (-1, 0), (0, -1) };

    private List<(int r, int c)> GetLowPoints(IReadOnlyList<char[]> grid)
    {
        var lowerPoints = new List<(int r, int c)>();
        for (var r = 0; r < grid.Count; r++)
        {
            for (var c = 0; c < grid[0].Length; c++)
            {
                var current = grid[r][c] - '0';
                if (dirs.Select(dir => Value(r + dir.dx, c + dir.dy)).All(nbr => nbr > current))
                {
                    lowerPoints.Add((r,c));
                }
            }
        }

        return lowerPoints;
        
        int Value( int r, int c)
        {
            if (r < 0 || r >= grid.Count || c < 0 || c >= grid[0].Length) return 10;
            return grid[r][c] - '0';
        }
    }

    public int First(IEnumerable<string> input)
    {
        var grid = input.Select(x => x.ToCharArray()).ToList();
        var lowerPoints = GetLowPoints(grid);
        return lowerPoints.Select(x => grid[x.r][x.c] - '0' + 1).Sum();
    }

    public int Second(IEnumerable<string> input)
    {
        var grid = input.Select(x => x.ToCharArray()).ToList();
        var lowerPoints = GetLowPoints(grid);
        var basins = new List<int>();
        foreach (var point in lowerPoints)
        {
            if (grid[point.r][point.c] == '#') continue;
            basins.Add(Count(point));
        }
        basins.Sort();
        return basins.TakeLast(3).Aggregate(1, (acc, item) => acc * item);

        int Count((int r, int c) point)
        {
            if (point.r < 0 || point.c < 0 || point.r >= grid.Count || point.c >= grid[0].Length) return 0;
            if (grid[point.r][point.c] is '9' or '#') return 0;
            grid[point.r][point.c] = '#';
            return 1 + dirs.Sum(dir => Count((point.r + dir.dx, point.c + dir.dy)));
        }
    }
}

