using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Solvers
{
    public class Day04
    {
        private (IEnumerable<int> numbers, List<Board> boards) Init(IEnumerable<string> input)
        {
            IEnumerable<int> numbers = default;
            Board? current = default;
            List<Board> boards = new();

            foreach (var line in input)
            {
                if (numbers is null) numbers = line.Split(",").Select(int.Parse);
                else if (line is "")
                {
                    boards.Add(current = new Board());
                }
                else
                {
                    current.Add(line.Split(" ")
                        .Select(line => line.Trim())
                        .Where(line => !string.IsNullOrEmpty(line))
                        .Select(int.Parse).ToList());
                }
            }

            return (numbers, boards);
        }

        public int First(IEnumerable<string> input)
        {
            var (numbers, boards) = Init(input);

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    board.Play(number);
                    if (board.Won) return board.Sum * number;
                }
            }

            return -1;
        }

        public int Second(IEnumerable<string> input)
        {
            var (numbers, boards) = Init(input);

            Board lastWinner = default;
            int lastNumber = 0;
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.Won) continue;
                    board.Play(number);
                    if (board.Won)
                    {
                        lastWinner = board;
                        lastNumber = number;
                    }
                }
            }

            return lastWinner.Sum * lastNumber;
        }

        class Board
        {
            public List<List<int>> Rows { get; } = new();
            public int Sum { get; private set; } = 0;
            public bool Won { get; private set; }

            public void Add(List<int> row)
            {
                Rows.Add(row);
                Sum += row.Sum();
            }

            public void Play(int number)
            {
                foreach (var row in Rows)
                {
                    for (int i = 0; i < row.Count; i++)
                    {
                        if (row[i] == number)
                        {
                            row[i] -= 1000;
                            Sum -= number;
                        }
                    }
                }

                foreach (var row in Rows)
                {
                    if (row.All(x => x < 0))
                    {
                        Won = true;
                        return;
                    }
                }

                for (var i = 0; i < Rows[0].Count; i++)
                {
                    if (Rows.All(x => x[i] < 0))
                    {
                        Won = true;
                        return;
                    }
                }
            }
        }
    }
}