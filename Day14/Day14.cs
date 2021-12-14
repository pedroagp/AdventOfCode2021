using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Challenges {
    internal class Day14 : Challenge {
        private readonly Dictionary<string, char> rules;
        private readonly Dictionary<string, (string,string)> outcomes = new();
        private Dictionary<string, long> polymerCount = new();

        private readonly string template;

        public Day14(bool testInput = false) : base(testInput) {
            template = input[0];

            rules = base.input.Where(x => x.Contains("->")).Select(x => x.Split(" -> ")).ToDictionary(x => x[0], x => x[1][0]);

            //unfold rules in possible outcome pairs
            foreach (var rule in rules) {
                //CH -> B 

                //CB
                string pairA = string.Concat(rule.Key[0], rule.Value);
                //BH
                string pairB = string.Concat(rule.Value, rule.Key[1]);
                outcomes[rule.Key] = new(pairA, pairB);
            }

            for (int i = 0; i < template.Length - 1; i++) {
                var pair = template.Substring(i, 2);

                if (!polymerCount.ContainsKey(pair)) {
                    polymerCount[pair] = 0;
                }

                polymerCount[pair] += 1;
            }
        }

        public override string FirstResult() {
            return $"{RunSub(10)}";
        }

        public override string SecondResult() {
            return $"{RunSub(30)}";
        }
        private long RunSub(int steps) {
            for (int i = 0; i< steps; i++) {
                Dictionary<string, long> tmpCount = new();
                foreach (var polymer in polymerCount) {
                    var pairA = string.Concat(polymer.Key[0], rules[polymer.Key]);
                    var pairB = string.Concat(rules[polymer.Key], polymer.Key[1]);

                    if (!tmpCount.ContainsKey(pairA)) {
                        tmpCount[pairA] = 0;
                    }
                    if (!tmpCount.ContainsKey(pairB)) {
                        tmpCount[pairB] = 0;
                    }

                    tmpCount[pairA] += polymer.Value;
                    tmpCount[pairB] += polymer.Value;
                }

                polymerCount = tmpCount;
            }


            Dictionary<char, long> result = new();
            foreach (var pc in polymerCount) {
                var polyFirstChar = pc.Key[0];
                if (!result.ContainsKey(polyFirstChar)) {
                    result[polyFirstChar] = 0;
                }
                result[polyFirstChar] += pc.Value;
            }

            //final pair has its right char never considered
            result[template[template.Length - 1]] += 1;

            return result.Values.Max() - result.Values.Min();
        }
    }
}