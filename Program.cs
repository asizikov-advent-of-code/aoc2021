using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using aoc2021.Solvers;

var day = new Day08();
Console.WriteLine(day.First(File.ReadAllLines("inputs/08-01.txt")));
Console.WriteLine(day.Second(File.ReadAllLines("inputs/08-01.txt")));