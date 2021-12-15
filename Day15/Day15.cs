using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day15 : Challenge {
        private struct Coord {
            public int x; public int y;

            public Coord(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }
        private Dictionary<Coord, int> graph;
        private readonly int[][] intput;
        public Day15(bool testInput = false) : base(testInput) {
            intput = base.input.Select(x => x).Select(x => x.ToCharArray().Select(x => (int)char.GetNumericValue(x)).ToArray()).ToArray();
            graph = new Dictionary<Coord, int>();
            for (int i = 0; i < intput.Length; i++) {
                for (int j = 0; j < intput[i].Length; j++) {
                    graph[new(j, i)] = intput[j][i];
                }
            }
        }

        public override string FirstResult() {
            return $"{Dijkstra(graph, new Coord(0,0), new Coord(intput.Length-1,intput[^1].Length-1))}";
        }


        public override string SecondResult() {
            BlowUpGraph(5);
            return $"{Dijkstra(graph, new Coord(0, 0), new Coord(5*intput.Length - 1, 5*intput[^1].Length-1))}";
        }

        private void BlowUpGraph(int size) {
            int[][] newIntput = new int[size * intput.Length][];
            for (int i = 0; i < size * intput.Length; i++) {
                newIntput[i] = new int[size * intput[0].Length];
            }
            for (int i = 0; i < newIntput.Length; i++) {
                for (int j = 0; j < newIntput[i].Length; j++) {
                    var dist = (i / intput.Length) + (j / intput[0].Length);
                    var newValue = intput[j % intput[0].Length][i % intput.Length] + dist - 1;
                    newIntput[j][i] =  (newValue % 9) + 1;
                }
            }
            graph = new Dictionary<Coord, int>();
            for (int i = 0; i < newIntput.Length; i++) {
                for (int j = 0; j < newIntput[i].Length; j++) {
                    graph[new(j, i)] = newIntput[j][i];
                }
            }
        }

        int Dijkstra(Dictionary<Coord, int> graph, Coord start, Coord end) {
            var queue = new PriorityQueue<Coord, int>();
            var pathCosts = new Dictionary<Coord, int> {
                [start] = 0
            };

            queue.Enqueue(start, 0);

            while (queue.Count > 0) {
                var p = queue.Dequeue();

                foreach (var n in borders(p)) {
                    if (graph.ContainsKey(n) && !pathCosts.ContainsKey(n)) {
                        var cost = pathCosts[p] + graph[n];
                        pathCosts[n] = cost;
                        queue.Enqueue(n, cost);
                    }
                }
            }

            return pathCosts[end];
        }
        
        Coord[] borders(Coord point) {
            return new[] {
                   new Coord(point.x+1, point.y),
                   new Coord(point.x-1, point.y),
                   new Coord(point.x, point.y+1),
                   new Coord(point.x, point.y-1),
            };
        }
    }
}
