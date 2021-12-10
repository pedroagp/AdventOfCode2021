using AdventOfCode2021.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day10 : Challenge {
        public Day10(bool testInput = false) : base(testInput) { }
        readonly Stack<char> chunk = new();
        readonly string open = "{[(<";
        readonly string close = "}])>";
        readonly Dictionary<char, char> match = new() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        readonly Dictionary<char,int> closingPoints = new() { { ')', 3} , { ']', 57 } , { '}',1197 }, { '>',25137 } };
        readonly Dictionary<char, int> openPoints = new() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
        readonly List<string> invalid = new();
        readonly List<long> scores = new();

        public override string FirstResult() {
            var count = 0;
            foreach(string s in base.input) {
                chunk.Clear();
                foreach(char c in s) {
                    if (!chunk.TryPeek(out char curr)) { //empty chunk list
                        if (close.Contains(c)) { //must open new chunk, if not
                            count += closingPoints[c];
                            invalid.Add(s);
                            break;
                        }
                    }

                    if (open.Contains(c)) {
                        chunk.Push(c);
                    }

                    if (close.Contains(c)) {
                        curr = chunk.Pop();
                        if (!(match[curr] == c)) {
                            count += closingPoints[c];
                            invalid.Add(s);
                            break;
                        }
                    }
                }
            }
            return $"{count}";
        }


        public override string SecondResult() {
            string[] valid = base.input.Except(invalid).ToArray();
            foreach (string s in valid) {
                long score = 0;
                chunk.Clear();
                foreach (char c in s) {
                    if (open.Contains(c)) {
                        chunk.Push(c);
                    }

                    if (close.Contains(c)) {
                        chunk.Pop();
                    }
                }
                foreach (char c in chunk) {
                    score = (score * 5) + openPoints[c];
                }
                scores.Add(score);
            }
            scores.Sort();
            return $"{scores[scores.Count / 2]}";
        }
    }
}
