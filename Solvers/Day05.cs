using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Solvers
{
    class Day05
    {
        private (int[][] grid, List<List<(int r, int c)>> points) ConvertToGrid(IEnumerable<string> input, bool diags)
        {
            var rows = 0;
            var lines = new List<List<(int r, int c)>>();
            foreach (var line in input)
            {
                var points = Parse(line, diags);
                if (points.Count == 0) continue;
                var maxC = points.Max(x => x.c);
                var maxR = points.Max(x => x.r);
                rows = Math.Max(rows, maxR);
                rows = Math.Max(rows, maxC);
                lines.Add(points);
            }

            var grid = new int[rows + 1][];
            for (var r = 0; r <= rows; r++) grid[r] = new int[rows + 1];
            return (grid, lines);
        }

        private List<(int r, int c)> Parse(string line, bool diags)
        {
            var separatorStart = line.IndexOf('-');
            var separatorEnd = line.IndexOf('>');
            var first = line.Substring(0, separatorStart).Split(',');
            var second = line.Substring(separatorEnd + 1, line.Length - separatorEnd - 1).Split(',');
            (int r, int c) from = (int.Parse(first[0]), int.Parse(first[1]));
            (int r, int c) to = (int.Parse(second[0]), int.Parse(second[1]));

            var points = new List<(int r, int c)>();

            if (from.r == to.r)
            {
                var mn = Math.Min(to.c, from.c);
                var mx = Math.Max(to.c, from.c);
                for (var c = 0; c <= mx - mn; c++)
                {
                    points.Add((from.r, mn + c));
                }
            }
            else if (from.c == to.c)
            {
                var mn = Math.Min(to.r, from.r);
                var mx = Math.Max(to.r, from.r);
                for (var r = 0; r <= mx - mn; r++)
                {
                    points.Add((mn + r, from.c));
                }
            }
            else if (diags)
            {
                for (var i = 0; i <= Math.Max(from.r, to.r) - Math.Min(from.r, to.r); ++i)
                {
                    points.Add((from.r + (from.r < to.r ? 1 : -1) * i, from.c + (from.c < to.c ? 1 : -1) * i));
                }
            }

            return points;
        }

        public int First(IEnumerable<string> input)
        {
            var (grid, lines) = ConvertToGrid(input, false);
            foreach (var line in lines)
            {
                foreach (var p in line)
                {
                    grid[p.r][p.c]++;
                }
            }

            var answer = 0;
            foreach (var row in grid)
            {
                foreach (var c in row)
                {
                    if (c > 1) answer++;
                }
            }

            return answer;
        }

        public int Second(IEnumerable<string> input)
        {
            var (grid, lines) = ConvertToGrid(input, true);
            foreach (var line in lines)
            {
                foreach (var p in line)
                {
                    grid[p.r][p.c]++;
                }
            }

            var answer = 0;
            foreach (var row in grid)
            {
                foreach (var c in row)
                {
                    if (c > 1) answer++;
                }
            }

            return answer;
        }
    }
}