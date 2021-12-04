using AdventOfCode2021.Common;
using AdventOfCode2021.Day04;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day05 : Challenge {
        private readonly string[] input;

        public Day05() {
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
