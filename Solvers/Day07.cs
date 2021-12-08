using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Solvers
{
    internal class Day07
    {
        private static int FindMin(IEnumerable<string> input, Func<int, int, int, int> costFunc)
        {
            var crabs = input.First().Split(',').Select(int.Parse).ToList();
            var group = crabs.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var (max, min) = (crabs.Max(), crabs.Min());

            return Enumerable.Range(min, max - min)
                .Select(targetPosition => group.Sum(g => costFunc(g.Value, g.Key, targetPosition)))
                .Prepend(int.MaxValue)
                .Min();
        }

        public int First(IEnumerable<string> input) =>
            FindMin(input,
                (groupSize, groupPosition, targetPosition) => Math.Abs(targetPosition - groupPosition) * groupSize);

        public int Second(IEnumerable<string> input) =>
            FindMin(input,
                (groupSize, groupPosition, targetPosition) =>
                {
                    var dist = Math.Abs(targetPosition - groupPosition);
                    return dist * (dist + 1) / 2 * groupSize;
                }
            );
    }
}