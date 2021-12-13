using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Challenges {
    internal class Day13 : Challenge {
        public readonly int[][] intput;
        public List<string> folds;
        public Day13(bool testInput = false) : base(testInput) {
            intput = new int[1500][];
            folds = new List<string>();

            for (int i = 0; i < intput.Length; i++) {
                intput[i] = new int[1500];
            }
            foreach (var line in base.input) {
                if (!line.Contains("fold along") && line.Length > 2) {
                    var coords = line.Split(',').Select(int.Parse).ToArray();
                    intput[coords[1]][coords[0]]++;
                } else if (line.Length > 2) {
                    folds.Add(line);
                }
            }
        }

        public override string FirstResult() {
            ApplyFold(folds.First());
            return $"{Count()}";
        }

        private int Count() {
            var count = 0;
            for (var x = 0; x < intput.Length; x++) {
                for (var y = 0; y < intput[x].Length; y++) {
                    if (intput[x][y] == 1)
                        count++;
                }
            }
            return count;
        }

        private string printBoard() {
            StringBuilder ret = new();
            ret.AppendLine();
            for (int x = 0; x < 6; x++) {
                for (int y = 0; y < 40; y++) {
                    ret.Append(intput[x][y]==1?"X":" ");
                }
                ret.AppendLine();
            }
            return ret.ToString();
        }

        private void ApplyFold(string v) {
            var fold = v.Replace("fold along ", "").Split("=");
            var axis = fold[0];
            var coord = int.Parse(fold[1]);
            if (axis == "x") {
                for (var i = 0; i < intput.Length; i++) {
                    for (var j = coord+1; j < intput[i].Length; j++) {
                        if (intput[i][j] > 0) {
                            intput[i][j] = 0;
                            intput[i][coord - (j - coord)] = 1;
                        }
                    }
                }
            } else { // y
                for (var i = coord+1; i < intput.Length; i++) {
                    for (var j = 0; j < intput[i].Length; j++) {
                        if (intput[i][j] > 0) {
                            intput[i][j] = 0;
                            intput[coord - (i - coord)][j] = 1;
                        }
                    }
                }
            }
            folds.RemoveAt(0);
        }

        public override string SecondResult() {
            while (folds.Any()) {
                ApplyFold(folds.First());
            }
            return printBoard();
        }
    }
}
