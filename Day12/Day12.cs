using AdventOfCode2021.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day12 : Challenge {
        Dictionary<string, List<string>> graph;

        public Day12(bool testInput = false) : base(testInput) {
            graph = new();
            foreach(var line in base.input.Select(x => x.Split('-'))){
                if (!graph.ContainsKey(line[0])) {
                    graph[line[0]] = new();
                }
                if (!graph.ContainsKey(line[1])) {
                    graph[line[1]] = new();
                }
                graph[line[0]].Add(line[1]);
                graph[line[1]].Add(line[0]);
            }
        }

        int CountPathsOne(string start, List<string> visitedCaves) {

            if (start == "end") {
                return 1;
            }

            var count = 0;
            foreach (var connection in graph[start]) {
                var isBig = connection.All(char.IsUpper);
                var visited = visitedCaves.Contains(connection);

                // we can visit anything not visited before or a big cave
                if (!visited || isBig) {
                    var newVisited = new List<string>(visitedCaves);
                    newVisited.Add(connection);
                    count += CountPathsOne(connection, newVisited);
                }
            }
            return count;
        }

        int CountPathsTwo(string start, List<string> visitedCaves) {

            if (start == "end") {
                return 1;
            }

            var count = 0;
            foreach (var connection in graph[start]) {
                var isBig = connection.All(char.IsUpper);
                var visited = visitedCaves.Contains(connection);

                // we can visit anything not visited before or a big cave
                if (!visited || isBig) {
                    var newVisited = new List<string>(visitedCaves);
                    newVisited.Add(connection);
                    count += CountPathsTwo(connection, newVisited);
                } else if (!isBig && !connection.Equals("start") && !visitedCaves.Contains("pqpAsGrutas")) {
                    //we can visit a single small cave (assuming it is not the start) only once, so I add this virtual cave to the list
                    //to prevent going again in the branch
                    var newVisited = new List<string>(visitedCaves);
                    newVisited.Add(connection);
                    newVisited.Add("pqpAsGrutas");
                    count += CountPathsTwo(connection, newVisited);
                }
                
            }
            return count;
        }

        public override string FirstResult() {
            var result = CountPathsOne("start", new List<string> { "start" });
            return $"{result}";
        }

        public override string SecondResult() {
            var result = CountPathsTwo("start", new List<string> { "start" });
            return $"{result}";
        }       
    }
}
