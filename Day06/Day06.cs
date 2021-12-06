using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day06 : Challenge {
        private readonly string[] input;
        private Dictionary<int, long> fish;

        public Day06() {
            if (input is null) {
                input = File.ReadAllLines(Path.Combine(this.GetType().Name, Constants.DefaultFileName));
            }
            if (fish == null) {
                fish = input.First().Split(',').Select(int.Parse).GroupBy(x => x).ToDictionary(x=> x.Key, x=> (long)x.Count());
                fish.Add(7, 0);
                fish.Add(8, 0);
                fish.Add(6, 0);
                fish.Add(0, 0);
            }
        }

        public override string FirstResult() {
            for(int i= 0; i<80; i++) {
                fish = ProcessFish();
            }
            return $"{fish.Values.Sum()}";
        }

        private Dictionary<int, long> ProcessFish() {
            var tempFish = new Dictionary<int, long>() {
                [0] = fish[1],
                [1] = fish[2], 
                [2] = fish[3],
                [3] = fish[4],
                [4] = fish[5],
                [5] = fish[6],
                [6] = fish[7] + fish[0],
                [7] = fish[8],
                [8] = fish[0],
            };

            return tempFish;
        }

        public override string SecondResult() {
            for (int i = 80; i < 256; i++) {
                fish = ProcessFish();
            }
            return $"{fish.Values.Sum()}";
        }
    }
}
