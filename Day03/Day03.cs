using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day03 : Challenge {
        private readonly string[] input;

        public Day03() {
            if (input is null) {
                input = File.ReadAllLines(Path.Combine(this.GetType().Name, Constants.DefaultFileName));
            }
        }

        public override string FirstResult() {
            string gamma = string.Empty, epsilon = string.Empty;
            int[][] count = new int[2][] {new int[12], new int[12]};

            foreach (string s in input) {
                for (int i = 0; i < s.Length; i++) {
                    if (s[i] == '0')
                        count[0][i]++;
                    else {
                        count[1][i]++;
                    }
                }
            }
            
            for (int i = 0; i < count[0].Length; i++) {
                if (count[0][i] > count[1][i]) {
                    gamma += "0";
                    epsilon += "1";
                } else {
                    gamma += "1";
                    epsilon += "0";
                }
            }

            var gammaDec = Convert.ToInt32(gamma, 2);
            var epsilonDec = Convert.ToInt32(epsilon, 2);

            return $"{gammaDec * epsilonDec}";
        }

        public override string SecondResult() {
            List<string> templines = new();
            List<string> linesList = input.ToList();

            var index = 0;
            while (true) {
                int[] count = new int[2];

                foreach (string s in linesList) {

                    if (s[index] == '0')
                        count[0]++;
                    else {
                        count[1]++;
                    }
                }
                foreach (string s in linesList) {
                    if (count[1] >= count[0]) {
                        if (s[index] == '1') {
                            templines.Add(s);
                        }
                    } else {
                        if (s[index] == '0') {
                            templines.Add(s);
                        }
                    }
                }
                if (templines.Count == 1) {
                    break;
                } else {
                    linesList = new(templines);
                    templines.Clear();
                    index++;
                }
            }
            int gammaDec = Convert.ToInt32(templines[0], 2);
            linesList = input.ToList();
            index = 0;
            while (true) {
                int[] count = new int[2];

                foreach (string s in linesList) {

                    if (s[index] == '0')
                        count[0]++;
                    else {
                        count[1]++;
                    }
                }
                foreach (string s in linesList) {
                    if (count[0] <= count[1]) {
                        if (s[index] == '0') {
                            templines.Add(s);
                        }
                    } else {
                        if (s[index] == '1') {
                            templines.Add(s);
                        }
                    }
                }
                if (templines.Count == 1) {
                    break;
                } else {
                    linesList = new(templines);
                    templines.Clear();
                    index++;
                }
            }
           
            int epsilonDec = Convert.ToInt32(templines[0], 2);

            return $"{gammaDec * epsilonDec}";
        }
    }
}
