using AdventOfCode2021.Common;
using System.IO;

namespace AdventOfCode2021.Challenges {
    internal class Day02 : Challenge {

        public Day02(bool testInput = false) : base(testInput) {}

        public override string FirstResult() {
            var horizontal = 0;
            var depth = 0;

            foreach (string s in input) {
                var command = s.Split(' ');
                var direction = command[0]; 
                var amount = int.Parse(command[1]);

                switch (direction) {
                    case "forward":
                        horizontal += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                }
            }
            return $"{horizontal * depth}";
        }

        public override string SecondResult() {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (string s in input) {
                var command = s.Split(' ');
                var direction = command[0];
                var amount = int.Parse(command[1]);

                switch (direction) {
                    case "forward":
                        horizontal += amount;
                        depth += (aim * amount);
                        break;
                    case "up":
                        aim -= amount;
                        break;
                    case "down":
                        aim += amount;
                        break;
                }
            }
            return $"{horizontal * depth}";
        }
    }
}
