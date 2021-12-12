namespace aoc2021.Solvers;

public class Day12
{
    public long First(IEnumerable<string> input)
    {
        var graph = ToGraph(input);
        var answer = 0;
        var visited = new HashSet<string>();
        dfs("start");
        return answer;

        void dfs(string current)
        {
            if (current == "end") answer++;
            else
            {
                if (visited.Contains(current)) return;
                if (current.All(char.IsLower)) visited.Add(current);
                foreach (var next in graph[current].Where(x => x != "start"))
                {
                    dfs(next);
                }

                if (current.All(char.IsLower)) visited.Remove(current);
            }
        }
    }

    private Dictionary<string, HashSet<string>> ToGraph(IEnumerable<string> input)
    {
        var graph = new Dictionary<string, HashSet<string>>();
        foreach (var line in input)
        {
            var parts = line.Split('-');
            var (current, next) = (parts[0], parts[1]);
            if (!graph.ContainsKey(current)) graph.Add(current, new HashSet<string>());
            graph[current].Add(next);
            if (!graph.ContainsKey(next)) graph.Add(next, new HashSet<string>());
            graph[next].Add(current);
        }

        return graph;
    }

    public long Second(IEnumerable<string> input)
    {
        var graph = ToGraph(input);
        var answer = new HashSet<string>();
        var visited = new HashSet<string>();
        dfs("start", false, new LinkedList<string>());
        return answer.Count;

        void dfs(string current, bool isTwiceRuleSet, LinkedList<string> path)
        {
            if (current == "end")
            {
                answer.Add(string.Join(",",path));
            }
            else
            {
                if (visited.Contains(current)) return;
                foreach (var next in graph[current].Where(x => x != "start"))
                {
                    path.AddLast(next);
                    if (current.All(char.IsLower))
                    {
                        visited.Add(current);
                    }
                    dfs(next, isTwiceRuleSet, path);
                    if (!isTwiceRuleSet && current.All(char.IsLower))
                    {
                        visited.Remove(current);
                        dfs(next, true, path);
                    }
                    path.RemoveLast();
                }

                if (current.All(char.IsLower) && visited.Contains(current)) visited.Remove(current);
            }
        }
    }
}