using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using aoc2021.Solvers;

var day = new Day10();
Console.WriteLine(day.First(File.ReadAllLines("inputs/10-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/10-01.txt")));

public class Day10
{
    private readonly Dictionary<char, int> ErrorReward = new()
    {  { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
    
    private readonly Dictionary<char, int> FixReward = new()
    { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

    private readonly Dictionary<char, char> Pairs = new()
    { {'(',')'},{'[',']'},{'{','}'},{'<','>'}, };
    
    private readonly HashSet<char> Opening = new() { '(', '[', '{', '<' };

    private (bool IsValid, char ErrorToken) Validate(string line)
    {
        var stack = new Stack<char>();
        foreach (var token in line)
        {
            if(Opening.Contains(token)) stack.Push(token);
            else
            {
                if (stack.Count == 0 || Pairs[stack.Peek()] != token) return (false, token);
                stack.Pop();
            }
        }

        return (true, char.MinValue);
    }
    
    public int First(IEnumerable<string> input) => 
        input.Select(Validate).Where(result => !result.IsValid).Sum(result => ErrorReward[result.ErrorToken]);

    public long Second(IEnumerable<string> input)
    {
        var linesToRepair = input.Where(x => Validate(x).IsValid);
        var scores = linesToRepair.Select(Score).ToList();
        scores.Sort();
        return scores[scores.Count / 2];
    }

    private long Score(string line)
    {
        var stack = new Stack<char>();
        foreach (var token in line)
        {
            if(Opening.Contains(token)) stack.Push(token);
            else
            {
                stack.Pop();
            }
        }

        var repairs = new List<char>();
        while (stack.Count != 0)
        {
            repairs.Add(Pairs[stack.Pop()]);
        }

        var score = 0L;
        foreach (var t in repairs)
        {
            score *= 5;
            score += FixReward[t];
        }

        return score;
    }
}

