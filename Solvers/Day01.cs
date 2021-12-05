using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Solvers
{
    public class Day01
    {
        public int First(IEnumerable<string> input)
        {
            var data = input.Select(i => int.Parse(i)).ToList();
            var answer = 0;
            for (var i = 1; i < data.Count; i++)
            {
                if (data[i - 1] < data[i]) answer++;
            }

            return answer;
        }

        public int Second(IEnumerable<string> input)
        {
            var data = input.Select(i => int.Parse(i)).ToList();
            var answer = 0;

            for (var i = 3; i < data.Count; i++)
            {
                var current = data[i] + data[i - 1] + data[i - 2];
                var prev = data[i - 1] + data[i - 2] + data[i - 3];
                if (prev < current) answer++;
            }

            return answer;
        }
    }
}