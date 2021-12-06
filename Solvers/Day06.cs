using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Solvers
{
    public class Day06
    {
        public int First(IEnumerable<string> input)
        {
            var school = GetSchool(input);
            for (var i = 0; i < 80; i++)
            {
                var current = school.Count;
                for (var fish = 0; fish < current; fish++)
                {
                    school[fish]--;
                    if (school[fish] < 0)
                    {
                        school[fish] = 6;
                        school.Add(8);
                    }
                }
            }

            return school.Count;
        }

        public long Second(IEnumerable<string> input)
        {
            var school = GetSchool(input);
            var timers = new long[9];
            foreach (var fish in school) timers[fish]++;
        
            var day = 256;
            while (day --> 0)
            {
                var next = new long[9];
                for(var i = 1; i < 9; i++)
                {
                    next[i - 1] = timers[i];
                }
                next[6] += timers[0];
                next[8] += timers[0];
                timers = next;
            }
        
            return timers.Sum();
        }

        private static List<int> GetSchool(IEnumerable<string> input)
        {
            var school = input.First().Split(',').Select(int.Parse).ToList();
            return school;
        }
    }
}