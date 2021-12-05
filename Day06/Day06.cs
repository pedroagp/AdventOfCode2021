using AdventOfCode2021.Common;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day06 : Challenge {
        private readonly string[] input;

        public Day06() {
            if (input is null) {
                input = File.ReadAllLines(Path.Combine(this.GetType().Name, Constants.DefaultFileName));
            }
        }

        public override string FirstResult() {
            return default;
        }

        public override string SecondResult() {
            return default;
        }
    }
}
