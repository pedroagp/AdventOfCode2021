using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day09 : Challenge {
        readonly int[][] intput;
        readonly List<Coord> basinOrgins = new();

        public Day09(bool testInput = false) : base(testInput) {
            intput = new int[base.input.Length][];
            for (int i = 0; i < base.input.Length; i++) {
                intput[i] = new int[base.input[i].Length];
                for (int j = 0; j < base.input[i].Length; j++) {
                    intput[i][j] =(int)char.GetNumericValue(base.input[i][j]);
                }
            }
        }

        public override string FirstResult() {
            var count = 0;
            for (int i = 0; i < intput.Length; i++) {
                for (int j = 0; j < intput[0].Length; j++) {
                    var c = intput[i][j];

                    if (i != 0 && c >= intput[i - 1][j])  continue;
                    if (i != intput.Length - 1 && c >= intput[i + 1][j]) continue;
                    if (j != 0 && c >= intput[i][j - 1]) continue;
                    if (j != intput[0].Length - 1 && c >= intput[i][j + 1]) continue;

                    basinOrgins.Add(new Coord() { x = i, y = j });
                    count += ++c;
                }
            }
            return $"{count}";
        }

        public struct Coord {
            public int x;
            public int y;

            public Coord(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }
        public override string SecondResult() {
            var basins = new List<List<Coord>>();
            foreach (var origin in basinOrgins) {
                var toVisit = new Queue<Coord>();
                var visited = new List<Coord>();
                toVisit.Enqueue(origin);

                while (toVisit.Any()) {
                    var currentTile = toVisit.Dequeue();
                    if(!visited.Contains(currentTile))visited.Add(currentTile);
                    var x = currentTile.x;
                    var y = currentTile.y;

                    //move left
                    if (x != 0 && intput[x - 1][y] != 9) {
                        var p = new Coord(x - 1, y);
                        if (!visited.Contains(p)) {
                            toVisit.Enqueue(p);
                        }
                    }
                    //move right
                    if (x != intput.Length - 1 && intput[x + 1][y] != 9) {
                        var p = new Coord(x + 1, y);
                        if (!visited.Contains(p)) {
                            toVisit.Enqueue(p);
                        }
                    }
                    //move top
                    if (y != 0 && intput[x][y - 1] != 9) {
                        var p = new Coord(x, y - 1);
                        if (!visited.Contains(p)) {
                            toVisit.Enqueue(p);
                        }
                    }
                    //move bottom
                    if (y != intput[0].Length - 1 && intput[x][y + 1] != 9) {
                        var p = new Coord(x, y + 1);
                        if (!visited.Contains(p)) {
                            toVisit.Enqueue(p);
                        }
                    }
                }
                if (!basins.Contains(visited)) basins.Add(visited);
            }
            long total = 1;
            foreach (var basin in basins.OrderByDescending(x => x.Count).Take(3)) {
                total *= basin.Count;
            }
            return $"{total}";
        }
    }
}
