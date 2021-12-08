using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Solvers
{
    internal class Day08
    {
        private static (string[]left, string[] right ) Parse(string line)
        {
            var segments = line.Split('|');
            return (
                segments[0].Split(" ").Select(t => t.Trim()).Where(x => !string.IsNullOrEmpty(x))
                    .Select(x =>
                    {
                        var buffer = x.ToCharArray();
                        Array.Sort(buffer);
                        return new string(buffer);
                    }).ToArray(),
                segments[1].Split(" ").Select(t => t.Trim()).Where(x => !string.IsNullOrEmpty(x))
                    .Select(x =>
                    {
                        var buffer = x.ToCharArray();
                        Array.Sort(buffer);
                        return new string(buffer);
                    })
                    .ToArray()
            );
        }

        public int First(IEnumerable<string> input)
        {
            var outputValues = input.Select(Parse);
            var counter = new Dictionary<int, int>
            {
                { 2, 0 }, //1
                { 4, 0 }, //4
                { 3, 0 }, //7
                { 7, 0 } //8
            };
            foreach (var values in outputValues)
            {
                foreach (var value in values.right)
                {
                    var count = value.Length;
                    if (counter.ContainsKey(count)) counter[count]++;
                }
            }

            return counter.Values.Sum();
        }

        private int Decode((string[] left, string[]right) line)
        {
            var knownPatterns = new Dictionary<int, int>
                { { 2, 1 }, { 4, 4 }, { 3, 7 }, { 7, 8 } };

            var signalsToDecode = line.right.ToHashSet();
            var currentMapping = new Dictionary<string, int>();
            var digits = new Dictionary<int, string>();
            foreach (var signal in line.left)
            {
                if (currentMapping.ContainsKey(signal)) continue;
                var pattern = signal.Length;
                if (knownPatterns.ContainsKey(pattern))
                {
                    currentMapping.Add(signal, knownPatterns[pattern]);
                    digits.Add(knownPatterns[pattern], signal);
                    if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                }
            }

            if (signalsToDecode.Count == 0)
            {
                return Convert(line.right, currentMapping);
            }

            foreach (var signal in line.left)
            {
                if (currentMapping.ContainsKey(signal)) continue;
                var pattern = signal.Length;
                if (pattern == 6)
                {
                    if (digits.ContainsKey(4))
                    {
                        if (digits[4].All(x => signal.Contains(x)))
                        {
                            currentMapping.Add(signal, 9);
                            digits.Add(9, signal);
                            if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                            continue;
                        }
                    }

                    if (digits.ContainsKey(1) || digits.ContainsKey(7))
                    {
                        var existing = digits.ContainsKey(1) ? digits[1] : digits[7];
                        if (!existing.All(x => signal.Contains(x)))
                        {
                            currentMapping.Add(signal, 6);
                            digits.Add(6, signal);
                            if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                            continue;
                        }
                    }

                    if (digits.ContainsKey(6))
                    {
                        if (!digits[6].All(x => signal.Contains(x)))
                        {
                            currentMapping.Add(signal, 0);
                            digits.Add(0, signal);
                            if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                            continue;
                        }
                    }

                    currentMapping.Add(signal, 0);
                    digits.Add(0, signal);
                    if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                }
            }

            if (signalsToDecode.Count == 0)
            {
                return Convert(line.right, currentMapping);
            }

            foreach (var signal in line.left)
            {
                if (currentMapping.ContainsKey(signal)) continue;
                var pattern = signal.Length;
                if (pattern == 5)
                {
                    if (digits.ContainsKey(1) || digits.ContainsKey(7))
                    {
                        var existing = digits.ContainsKey(1) ? digits[1] : digits[7];
                        if (existing.All(x => signal.Contains(x)))
                        {
                            currentMapping.Add(signal, 3);
                            digits.Add(3, signal);
                            if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                            continue;
                        }
                    }

                    if (digits.ContainsKey(6) || digits.ContainsKey(9))
                    {
                        var existing = digits.ContainsKey(6) ? digits[6] : digits[9];
                        if (signal.All(x => existing.Contains((char)x)))
                        {
                            currentMapping.Add(signal, 5);
                            digits.Add(5, signal);
                            if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                            continue;
                        }
                    }

                    currentMapping.Add(signal, 2);
                    digits.Add(2, signal);
                    if (signalsToDecode.Contains(signal)) signalsToDecode.Remove(signal);
                }
            }

            return signalsToDecode.Count == 0 ? Convert(line.right, currentMapping) : -1;
        }

        private static int Convert(string[] signals, Dictionary<string, int> currentMapping)
        {
            var result = 0;
            foreach (var t in signals)
            {
                result *= 10;
                result += currentMapping[t];
            }

            return result;
        }

        public long Second(IEnumerable<string> input)
            => input.Select(Parse).Aggregate(0L, (current, line) => current + Decode(line));
    }
}