using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day07 : Challenge {
        private new readonly int[] input;

        public Day07(bool testInput = false) : base(testInput) {
            if (input is null) {
                input = base.input.First().Split(',').Select(int.Parse).ToArray();
                Array.Sort(input);
            }
        }

        public override string FirstResult() {
            var cost = int.MaxValue;

            for (int i = input[0]; i < input[input.Length - 1]; i++) {
                var tmp = 0;
                for(int j = 0; j < input.Length; j++){
                    tmp += Math.Abs(input[j]-i);
                }
                
                if (tmp < cost) cost = tmp;
            }
     

            return $"{cost}";
        }


        public override string SecondResult() {
            var cost = int.MaxValue;
            int[] costs = new int[input[input.Length-1]+1];
            
            costs[0] = 0;
            costs[1] = 1;
            for (int i = 2; i <= input[input.Length - 1]; i++) {
                costs[i] = i + costs[i - 1];
            }

            for (int i = input[0]; i < input[input.Length - 1]; i++) {
                var tmp = 0;
                for (int j = 0; j < input.Length; j++) {
                    tmp += costs[Math.Abs(input[j] - i)];
                }
                if (tmp < cost) cost = tmp;
            }


            return $"{cost}";
        }
    }
}
