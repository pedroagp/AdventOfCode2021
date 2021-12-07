using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day06 : Challenge {
        private readonly Dictionary<int, long> fish;

        public Day06(bool testInput = false) : base(testInput) {
            if (fish is null) {
                fish = input.First().Split(',').Select(int.Parse).GroupBy(x => x).ToDictionary(x=> x.Key, x=> (long)x.Count());
                fish.Add(7, 0);
                fish.Add(8, 0);
                fish.Add(6, 0);
                fish.Add(0, 0);
            }
        }

        public override string FirstResult() {
            for(int i= 0; i<80; i++) {
                ProcessFish();
            }
            return $"{fish.Values.Sum()}";
        }

        private void ProcessFish() {
            var temp = fish[0];

            for (int i = 0; i < fish.Keys.Count-1; i++) {
                fish[i] = fish[i + 1];
            }

            fish[8] = temp;
            fish[6]+= temp;
        }

        public override string SecondResult() {
            for (int i = 80; i < 256; i++) {
                ProcessFish();
            }
            return $"{fish.Values.Sum()}";
        }
    }
}
