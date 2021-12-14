namespace aoc2021.Solvers;
public class Day14
{
    public long First(IEnumerable<string> input)
    {
        var lines = input.ToList();

        var template = new LinkedList<char>(lines[0]);
        var rules = new Dictionary<string, char>();
        for (var i = 2; i < lines.Count; i++)
        {
            var current = lines[i];
            var key = current[0..2];
            var insert = current[^1];
            rules.Add(key, insert);
        }

        foreach (var _ in Enumerable.Range(0, 10))
        {
            var current = new LinkedList<char>();
            for (var i = 0; i < template.Count - 1; i++)
            {
                var first = template.ElementAt(i);
                current.AddLast(first);
                var second = template.ElementAt(i + 1);
                var pair = $"{first}{second}";
                if (rules.ContainsKey(pair))
                {
                    current.AddLast(rules[pair]);
                }
            }

            current.AddLast(template.Last.Value);
            template = current;
        }

        var hist = new int[26];
        foreach (var ch in template) hist[ch - 'A']++;

        return hist.Max() - hist.Where(x => x!= 0).Min();
    }

    public long Second(IEnumerable<string> input)
    {
        var lines = input.ToList();

        var template = new Dictionary<string, long>();
        for (var i = 0; i < lines[0].Length - 1; i++)
        {
            var pair = $"{lines[0][i]}{lines[0][i + 1]}";
            template.PutIfAbsent(pair, 0L);
            template[pair]++;
        }
        
        var rules = new Dictionary<string, char>();
        for (var i = 2; i < lines.Count; i++)
        {
            var current = lines[i];
            var key = current[0..2];
            var insert = current[^1];
            rules.Add(key, insert);
        }

        foreach (var _ in Enumerable.Range(0, 40))
        {
            var current = new Dictionary<string, long>();
            foreach (var tmplt in template)
            {
                var first = tmplt.Key[0];
                var second = tmplt.Key[1];
                if (rules.ContainsKey(tmplt.Key))
                {
                    var left = $"{first}{rules[tmplt.Key]}";
                    var right = $"{rules[tmplt.Key]}{second}";
                    current.PutIfAbsent(left, 0L);
                    current.PutIfAbsent(right, 0L);
                    current[left] += tmplt.Value;
                    current[right] += tmplt.Value;
                }
                else
                {
                    current.PutIfAbsent(tmplt.Key, 0L);
                    current[tmplt.Key] += tmplt.Value;
                }
            }
            template = current;
        }

        var hist = new long[26];
        foreach (var t in template.Keys) hist[t[1] - 'A'] += template[t];
        
        return hist.Max() - hist.Where(x => x!= 0).Min();
    }
}