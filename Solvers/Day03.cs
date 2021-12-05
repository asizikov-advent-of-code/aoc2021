using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2021.Solvers
{
    public class Day03
    {
        private (string epsilon, string gamma) Process(IEnumerable<string> input)
        {
            var mask = new int[input.First().Length];
            foreach (var measurement in input)
            {
                for (var i = 0; i < measurement.Length; i++)
                {
                    mask[i] += measurement[i] == '0' ? 1 : -1;
                }
            }

            var epsilon = new StringBuilder();
            var gamma = new StringBuilder();
            for (var i = 0; i < mask.Length; i++)
            {
                epsilon.Append(mask[i] > 0 ? '1' : '0');
                gamma.Append(mask[i] > 0 ? '0' : '1');
            }

            return (epsilon.ToString(), gamma.ToString());
        }
        
        public int First(IEnumerable<string> input)
        {
            var (epsilon, gamma) = Process(input);
            return Convert.ToInt32(epsilon, 2) * Convert.ToInt32(gamma, 2);
        }

        public int Second(IEnumerable<string> input)
        {
            var list = input.ToList();
            var oxygen = 0;
            for (var i = 0; i < input.First().Length; i++)
            {
                var (epsilon, _) = Process(list);
                list = list.Where(l => l[i] == epsilon[i]).ToList();
                if (list.Count == 1)
                {
                    oxygen = Convert.ToInt32(list.First(), 2);
                    break;
                }
            }

            var co2 = 0;
            list = input.ToList();
            for (var i = 0; i < input.First().Length; i++)
            {
                var (_, gamma) = Process(list);
                list = list.Where(l => l[i] == gamma[i]).ToList();
                if (list.Count == 1)
                {
                    co2 = Convert.ToInt32(list.First(), 2);
                    break;
                }
            }

            return co2 * oxygen;
        }
    }
}

