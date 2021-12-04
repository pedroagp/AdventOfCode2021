using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Day04 {
    internal struct BingoNumber {
        public int Value;
        public bool Check;
        public BingoNumber(int value) {
            Value = value;
            Check = false;
        }
    }
    internal class Board {
        public bool winner = false;
        public BingoNumber[,] board = new BingoNumber[5, 5];

        public Board(string[] input) {
            for (int i = 0; i < input.Length; i++) {
                string[] parts = input[i].Trim().Replace("  ", " ").Split(' '); //Trim = remove leading spaces, Replace = remove double spaces due to single numbers
                for (int j = 0; j < parts.Length; j++) {
                    board[i, j] = new(int.Parse(parts[j]));
                }
            }
        }

        public void CheckNumber(int ball) {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    BingoNumber num = (board[i, j]);
                    if (num.Value == ball) {
                        num.Check = true;
                        board[i, j] = num;
                        return;
                    }
                }
            }
        }
        private bool HasLine(IEnumerable<BingoNumber> bingoNumbers) {
            foreach (var number in bingoNumbers) {
                if (!number.Check)
                    return false;
            }
            return true;
        }

        public bool HasBingo {
            get {
                for (int i = 0; i < 5; i++) {
                    var col = Enumerable.Range(0, board.GetLength(0)).Select(x => board[x, i]); 
                    if (HasLine(col))
                        return true;
                    var row = Enumerable.Range(0, board.GetLength(1)).Select(x => board[i, x]);
                    if (HasLine(row))
                        return true;

                }
                return false;
            }
        }
    }
}
