using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Challenges {
    internal class Day11 : Challenge {
        public readonly int[][][] intput;
        public Day11(bool testInput = false) : base(testInput) {
            intput = new int[base.input.Length][][];
            for (int i = 0; i < base.input.Length; i++) {
                intput[i] = new int[base.input[i].Length][];
                for (int j = 0; j < base.input[i].Length; j++) {
                    intput[i][j] = new int[2];
                    intput[i][j][0] = (int)char.GetNumericValue(base.input[i][j]);
                }
            }
        }
        

        public override string FirstResult() {
            var total = 0;
            total += Process(100);
            return $"{total}";
        }

        private int Process(int totalSteps) {
            var result = 0;
            for (int step = 0; step < totalSteps; step++) {
                bool hadFlash = false;
                resetFlashes();

                // increment 
                for (int x = 0; x < intput.Length; x++) {
                    for (int y = 0; y < intput[x].Length; y++) {
                        if (intput[x][y][0] == 9) {
                            intput[x][y][0] = 0;
                            intput[x][y][1] = -1;
                            hadFlash = true;
                        } else {
                            intput[x][y][0]++;
                        }
                    }
                }

                if (hadFlash) {
                    // process flashes
              for (int x = 0; x < intput.Length; x++) {
                        for (int y = 0; y < intput[x].Length; y++) {
                            if (intput[x][y][1] == -1) {
                                var t = ProcessFlash(x, y);
                                // if processing a flash caused new flashes 
                                if (t != 0) {
                                    x = -1; 
                                    break;
                                }
                            }
                        }
                    }
                }
                for (int x = 0; x < intput.Length; x++) {
                    for (int y = 0; y < intput[x].Length; y++) {
                        if(intput[x][y][1] == -2) {
                            result++;
                        }
                    }
                }
            }
            return result;
        }

        private int Process2() {
            var step = 0;
            while (true) { 
                step++;
                bool hadFlash = false;

                // increment 
                for (int x = 0; x < intput.Length; x++) {
                    for (int y = 0; y < intput[x].Length; y++) {
                        if (intput[x][y][0] == 9) {
                            intput[x][y][0] = 0;
                            intput[x][y][1] = -1;
                            hadFlash = true;
                        } else {
                            intput[x][y][0]++;
                        }
                    }
                }

                if (hadFlash) {
                // process flashes
                for (int x = 0; x < intput.Length; x++) {
                        for (int y = 0; y < intput[x].Length; y++) {
                            if (intput[x][y][1] == -1) {
                                var t = ProcessFlash(x, y);
                                // if processing a flash caused new flashes 
                                if (t != 0) {
                                    x = -1;
                                    break;
                                }
                            }
                        }
                    }
                }
                resetFlashes();
                bool allzero = true;
                for (int x = 0; x < intput.Length; x++) {
                    for (int y = 0; y < intput[x].Length; y++) {
                        if(intput[x][y][0] != 0) {
                            allzero = false;
                            break;
                        }
                    }
                }
                if (allzero) {
                    return step;
                }
            }
            
        }

        private string printBoard() {
            StringBuilder ret = new();
            for (int x = 0; x < intput.Length; x++) {
                for (int y = 0; y < intput[x].Length; y++) {
                    ret.Append($" {intput[x][y][0]} ");
                }
                ret.AppendLine();
            }
            return ret.ToString();
        }

        private void resetFlashes() {
            for (int x = 0; x < intput.Length; x++) {
                for (int y = 0; y < intput[x].Length; y++) {
                    intput[x][y][1] = 0;
                }
            }
        }

        private int ProcessFlash(int x, int y) {
            var temp = 0;
            intput[x][y][1] = -2;
            
            //not on left edge
            if (x != 0) {
                temp += Increment(x - 1, y);
                if (y != 0) {
                    temp += Increment(x - 1, y - 1);
                }
                if(y != intput[0].Length - 1) {
                    temp += Increment(x - 1, y + 1);
                }
            }
            
            //not on right edge
            if (x != intput.Length - 1) {
                temp += Increment(x + 1, y);
                if (y != 0) {
                    temp += Increment(x + 1, y - 1);
                }
                if (y != intput[0].Length - 1) {
                    temp += Increment(x + 1, y + 1);
                }
            }
            //not on top 
            if (y != 0) {
                temp += Increment(x, y - 1);
            }
            //not on bottom
            if (y != intput[0].Length - 1) {
                temp += Increment(x, y + 1);
            }

            return temp;
        }

        private int Increment(int x, int y) {
            var temp = 0;
            if (intput[x][y][0] == 0) {
                return temp;
            }
            if (intput[x][y][0] == 9) {
                intput[x][y][0] = 0;
                intput[x][y][1] = -1;
                temp++;
            } else {
                intput[x][y][0]++;
            }
            return temp;
        }

        public override string SecondResult() {
            
            return $"{Process2()+100}";
        }

    }
}
