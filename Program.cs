using AdventOfCode2021.Challenges;
using AdventOfCode2021.Common;
using System.Collections.Generic;

var challenges = new List<Challenge> { new Day01(), new Day02(), new Day03(), new Day04(), new Day05() };

foreach (var challenge in challenges) {
    challenge.Run();
}