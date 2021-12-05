using System.Collections.Generic;

namespace aoc2021.Solvers
{
    public class Day02
    {
        public int First(IEnumerable<string> input)
        {
            (int horizontal, int depth) me = (0, 0);
            foreach (var instruction in input)
            {
                var action = instruction.Split(" ");
                var delta = int.Parse(action[1]);
                switch (action[0])
                {
                    case "forward":
                        me.horizontal += delta;
                        break;
                    case "down":
                        me.depth += delta;
                        break;
                    case "up":
                        me.depth -= delta;
                        break;
                }
            }

            return me.depth * me.horizontal;
        }
        
        public int Second(IEnumerable<string> input)
        {
            (int horizontal, int depth, int aim) me = (0, 0, 0);
            foreach (var instruction in input)
            {
                var action = instruction.Split(" ");
                var delta = int.Parse(action[1]);
                switch (action[0])
                {
                    case "forward":
                        me.horizontal += delta;
                        me.depth += delta * me.aim;
                        break;
                    case "down":
                        me.aim += delta;
                        break;
                    case "up":
                        me.aim -= delta;
                        break;
                }
            }

            return me.depth * me.horizontal;
        }
    }
}