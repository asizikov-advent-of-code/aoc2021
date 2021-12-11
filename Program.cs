using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using aoc2021.Solvers;

var day = new Day11();
Console.WriteLine(day.First(File.ReadAllLines("inputs/11-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/11-01.txt")));

public class Day11
{
    private readonly (int dr, int dc)[] dirs =
        { (0, 1), (1, 0), (0, -1), (-1, 0), (1, 1), (1, -1), (-1, 1), (-1, -1) };

    public long First(IEnumerable<string> input)
    {
        var grid = ToGrid(input);
        return Enumerable.Range(0, 100).Sum(_ => Step(grid));
    }

    public long Second(IEnumerable<string> input)
    {
        var grid = ToGrid(input);

        var day = 0;
        while (true)
        {
            day++;
            var count = Step(grid);
            if (count == grid.Length * grid[0].Length) return day;
        }
    }

    private static int[][] ToGrid(IEnumerable<string> input)
    {
        return input.Select(x => x.ToCharArray())
            .Select(arr => arr.Select(ch => ch - '0'))
            .Select(e => e.ToArray())
            .ToArray();
    }

    private int Count(int[][] grid)
    {
        var answer = 0;
        foreach (var row in grid)
        {
            for (int c = 0; c < row.Length; c++)
            {
                if (row[c] < 0)
                {
                    answer++;
                    row[c] = 0;
                }
            }
        }

        return answer;
    }

    private void Glow(int[][] grid, (int r, int c) octo)
    {
        if (octo.r < 0 || octo.c < 0 || octo.r >= grid.Length || octo.c >= grid[0].Length) return;
        if (grid[octo.r][octo.c] < 0) return;
        grid[octo.r][octo.c]++;
        if (grid[octo.r][octo.c] > 9)
        {
            grid[octo.r][octo.c] = -1;
            foreach (var dir in dirs)
            {
                Glow(grid, (octo.r + dir.dr, octo.c + dir.dc));
            }
        }
    }

    private static List<(int r, int c)> Increment(int[][] grid)
    {
        var readyToGlow = new List<(int r, int c)>();
        for (var r = 0; r < grid.Length; r++)
        {
            for (var c = 0; c < grid[0].Length; c++)
            {
                grid[r][c]++;
                if (grid[r][c] > 9) readyToGlow.Add((r, c));
            }
        }

        return readyToGlow;
    }

    private int Step(int[][] grid)
    {
        var readyToGlow = Increment(grid);
        foreach (var octopus in readyToGlow)
        {
            Glow(grid, octopus);
        }

        return Count(grid);
    }
}