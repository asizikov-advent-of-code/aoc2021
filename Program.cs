using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using aoc2021.Solvers;

var day = new Day06();
Console.WriteLine(day.First(File.ReadAllLines("inputs/06-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/06-01.txt")));
