using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day08 : Challenge {

        public Day08(bool testInput = false) : base(testInput) { }

        public override string FirstResult() {

            var count = base.input.Select(x => x.Split("|").Last()).SelectMany(x => x.Split()).Where(x => (x.Length > 1 && x.Length < 5 ) || x.Length == 7).Count();

            return $"{count}";
        }


        public override string SecondResult() {
            var total = 0;
            foreach (var line in input) {
                var split = line.Split(" | ");
                var input = split[0].Split(' ').OrderBy(x => x.Length).ToList();
                var output = split[1].Split(' ');

                var one = input.Single(x => x.Length == 2);
                var seven = input.Single(x => x.Length == 3);
                var four = input.Single(x => x.Length == 4);
                var eight = input.Single(x => x.Length == 7);

                var top = seven.Except(one).ToList()[0];
                var six = input.Where(x => x.Length == 6).Single(x => x.Intersect(one).Count() == 1);
                var lowRight = one.Intersect(six).Single();
                var topRight = one.Single(x => x != lowRight);

                var two = input.Where(x => x.Length == 5).Single(x => x.Contains(topRight) && !x.Contains(lowRight));
                var three = input.Where(x => x.Length == 5).Single(x => x.Contains(topRight) && x.Contains(lowRight));
                var five = input.Where(x => x.Length == 5).Single(x => !x.Contains(topRight) && x.Contains(lowRight));

                var lowLeft = two.Except(five).Single(x => x != topRight);
                var zero = input.Where(x => x.Length == 6 && x != six).Single(x => x.Contains(lowLeft));
                var nine = input.Single(x => x.Length == 6 && x != six && x != zero);

                string lineNumber = string.Empty;
                foreach (string segmentString in output) {
                    int currentNumber = -1;

                    if (currentNumber is -1 && zero.Length == segmentString.Length && zero.Intersect(segmentString).Count() == segmentString.Length)   currentNumber = 0;
                    if (currentNumber is -1 && one.Length == segmentString.Length && one.Intersect(segmentString).Count() == segmentString.Length)     currentNumber = 1;
                    if (currentNumber is -1 && two.Length == segmentString.Length && two.Intersect(segmentString).Count() == segmentString.Length)     currentNumber = 2;
                    if (currentNumber is -1 && three.Length == segmentString.Length && three.Intersect(segmentString).Count() == segmentString.Length) currentNumber = 3;
                    if (currentNumber is -1 && four.Length == segmentString.Length && four.Intersect(segmentString).Count() == segmentString.Length)   currentNumber = 4;
                    if (currentNumber is -1 && five.Length == segmentString.Length && five.Intersect(segmentString).Count() == segmentString.Length)   currentNumber = 5;
                    if (currentNumber is -1 && six.Length == segmentString.Length && six.Intersect(segmentString).Count() == segmentString.Length)     currentNumber = 6;
                    if (currentNumber is -1 && seven.Length == segmentString.Length && seven.Intersect(segmentString).Count() == segmentString.Length) currentNumber = 7;
                    if (currentNumber is -1 && eight.Length == segmentString.Length && eight.Intersect(segmentString).Count() == segmentString.Length) currentNumber = 8;
                    if (currentNumber is -1 && nine.Length == segmentString.Length && nine.Intersect(segmentString).Count() == segmentString.Length)   currentNumber = 9;

                    lineNumber += currentNumber;
                }
                total += int.Parse(lineNumber);
            }

            return $"{total}";
        }
    }
}

