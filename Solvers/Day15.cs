namespace aoc2021.Solvers;

public class Day15
{
    private long FindPath(int[][] cave)
    {
        var dp = cave.Select(x => new int[x.Length]).ToArray();
        for (var c = 1; c < dp[0].Length; c++) dp[0][c] = cave[0][c] + dp[0][c-1];
        
        for (var r = 1; r < dp.Length; r++)
        {
            for (var c = 0; c < dp[0].Length; c++)
            {
                if (c == 0) dp[r][c] = cave[r][c] + dp[r-1][c];
                else dp[r][c] = cave[r][c] + Math.Min(dp[r-1][c], dp[r][c-1]);
            }
        }
        
        return dp[^1][^1];
    }
    private long FindPath2(int[][] cave)
    {
        (int dr, int dc)[] dirs = {(0, 1), (1, 0), (-1, 0), (0, -1) };
        var risks = cave.Select(x => new int[x.Length]).ToArray();
        
        (int r, int c) goal = (cave.Length -1, cave[0].Length - 1);
        
        var queue = new PriorityQueue<(int r, int c), int>();
        queue.Enqueue((0, 0), 0);
        
        while (queue.Count is not 0)
        {
            var current = queue.Dequeue();
            foreach (var dir in dirs)
            {
                (int r, int c) nbr = (current.r + dir.dr, current.c + dir.dc);
                if(nbr.c < 0 || nbr.r < 0 || nbr.r >= risks.Length || nbr.c >= risks[0].Length) continue;
                if(risks[nbr.r][nbr.c] is not 0) continue;
                
                var soFar = risks[current.r][current.c] + cave[nbr.r][nbr.c];
                risks[nbr.r][nbr.c] = soFar;
                if(nbr == goal) break;
                queue.Enqueue(nbr, soFar);
            }
        }
        
        return risks[^1][^1];
    }
    
    public long First(IEnumerable<string> input)
    {
        var cave = input.Select(x => x.ToCharArray())
            .Select(x => x.Select(y => y - '0').ToArray())
            .ToArray();
        return FindPath2(cave);
    }

    public long Second(IEnumerable<string> input)
    {
        var cave = input.Select(x => x.ToCharArray())
            .Select(x => x.Select(y => y - '0').ToArray())
            .ToArray();
        cave = Expand(cave, 5);
        return FindPath2(cave);
    }

    private int[][] Expand(int[][] cave, int times)
    {
        var map = new int[cave.Length * times][];
        for (var r = 0; r < map.Length; r++) map[r] = new int[cave[0].Length * times];
        for (var step = 0; step < times; step++)
        {
            for (var c = 0; c < times; c++)
            {
                Copy(step*cave.Length, c*cave[0].Length);    
            }
        }
        
        return map;

        void Copy(int top_r,int top_c)
        {
            for (var r = 0; r < cave.Length; r++)
            {
                for (var c = 0; c < cave[0].Length; c++)
                {
                    int value;
                    if (top_r == 0 && top_c == 0)
                    {
                        value = cave[r][c];
                    }
                    else
                    {
                        if (top_r == 0)
                        {
                            value = map[r + top_r][c + top_c - cave[0].Length] + 1;
                        }
                        else
                        {
                            value = map[r + top_r - cave.Length][c + top_c] + 1;
                        }
                        if (value == 10) value = 1;
                    }
                    map[r + top_r][c + top_c] = value;
                }
            }
        }
    }
}