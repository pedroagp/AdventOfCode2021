using AdventOfCode2021.Common;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Challenges {
    internal class Day05 : Challenge {
        private readonly int[,] oceanFloor;

        public Day05(bool testInput = false) : base(testInput) {
            if(oceanFloor is null) {
                oceanFloor = new int[1000, 1000]; 
            }
        }

        public override string FirstResult() {
            foreach (string line in input) {
                int[] coords = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToArray();
                if (Utils.IsStraightLine(coords)) { 
                    MapOceanFloor(coords);
                }
            }

            var count = 0;
            for (int i = 0; i < 1000; i++) {
                for(int k = 0; k < 1000; k++) {
                    if(oceanFloor[i, k] >= 2) {
                        count++;
                    }
                }
            }

            return $"{count}";
        }

        private void MapOceanFloor(int[] coords) {
            int x1, x2, y1, y2;
            x1 = coords[0]; 
            y1 = coords[1];
            x2 = coords[2];
            y2 = coords[3];

            var dx = x2 - x1;
            var dy = y2 - y1;

            while (x1 != x2 || y1 != y2) {
                oceanFloor[x1, y1] += 1;
                x1 += Math.Sign(dx);
                y1 += Math.Sign(dy);
            }

            oceanFloor[x1, y1] += 1;
        }

        public override string SecondResult() {
            foreach (string line in input) {
                int[] coords = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToArray();
                if (!Utils.IsStraightLine(coords)) { 
                    MapOceanFloor(coords);
                }
            }

            var count = 0;
            for (int i = 0; i < 1000; i++) {
                for (int k = 0; k < 1000; k++) {
                    if (oceanFloor[i, k] >= 2) {
                        count++;
                    }
                }
            }

            return $"{count}";
        }

    }
}
