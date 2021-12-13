using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks.Dataflow;
using aoc2021.Solvers;

var day = new Day13();
Console.WriteLine(day.First(File.ReadAllLines("inputs/13-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/13-01.txt")));

public class Day13
{
    private bool[][] Process(IEnumerable<string> input, int depth)
    {
        var lines = input.ToList();
        var dots = new List<(int c, int r)>();
        var i = 0;
        for (; i < lines.Count; i++)
        {
            var current = lines[i];
            if(current is "") break;
            var parts = current.Split(',');
            dots.Add((int.Parse(parts[0]), int.Parse(parts[1]) ));
        }
        var grid = BuildGrid(dots);

        i++;
        var folds = new List<(bool up , int val)>();
        for (; i < lines.Count; i++)
        {
            var current = lines[i];
            var parts = current.Split('=');
            folds.Add((parts[0][^1] == 'y',int.Parse(parts[1])));
        }

        for (var f = 0; f < Math.Min(depth, folds.Count); f++)
        {
            grid = Fold(grid, folds[f]);
        }

        return grid;
    }
    public long First(IEnumerable<string> input)
    {
        var grid = Process(input, 1);
        return grid.Sum(row => row.Count(x => x));
    }

    public long Second(IEnumerable<string> input)
    {
        var grid = Process(input, int.MaxValue);
        Print(grid);
        return grid.Sum(row => row.Count(x => x));
    }

    private bool[][] Fold(bool[][] grid, (bool up, int val) fold)
    {
        if (fold.up)
        {
            var result = new bool[fold.val][];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new bool[grid[i].Length];
                for (var j = 0; j < result[i].Length; j++) result[i][j] = grid[i][j];
            }
            
            for (var r = 0; r < fold.val; r++)
            {
                for (var c = 0; c < result[0].Length; c++)
                {
                    var next = 2 * fold.val - r;
                    if(next < grid.Length) result[r][c] = result[r][c] || grid[next][c];
                }
            }
            
            return result;
        }
        else
        {
            var result = new bool[grid.Length][];
            for (var r = 0; r < result.Length; r++)
            {
                result[r] = new bool[fold.val];
                for (var c = 0; c < result[r].Length; c++) result[r][c] = grid[r][c];
            }

            for (var r = 0; r < result.Length; r++)
            {
                for (var c = 0; c < result[r].Length; c++)
                {
                    var next = 2 * fold.val - c;
                    if(next < grid[r].Length) result[r][c] = result[r][c] || grid[r][next];
                }
            }
            return result;
        }
    }
    
    private static void Print(bool[][] grid)
    {
        foreach (var row in grid)
        {
            Console.WriteLine(string.Join("", row.Select(x => x ? '#' : '.')));
        }
    }

    private static bool[][] BuildGrid(List<(int c, int r)> dots)
    {
        var row = dots.Max(x => x.r);
        var cols = dots.Max(x => x.c);
        var grid = Enumerable.Range(0, row+1).Select(_ => new bool[cols+1]).ToArray();
        foreach (var dot in dots)
        {
            grid[dot.r][dot.c] = true;
        }

        return grid;
    }
}