using AdventOfCode2021.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day01 : Challenge {
        private new readonly int[] input;

        public Day01(bool testInput = false) : base(testInput) {
            if (input is null) {
                input = base.input.Select(int.Parse).ToArray();
            }
        }

        public override string FirstResult() {
            var count = 0;
            for (var i = 0; i < (input.Length - 1); i++) {
                if (input[i] < input[i + 1])
                    count++;
            }
            return $"{count}";  
        }

        public override string SecondResult() {
            var sums = new List<int>();
            for (int i = 0; i < (input.Length - 2); i++) {
                sums.Add(input[i] + input[i + 1] + input[i + 2]);
            }
            var count = 0;
            for (int i = 0; i < (sums.Count - 1); i++) {
                if (sums[i] < sums[i + 1])
                    count++;
            }
            return $"{count}";
        }
    }
}
