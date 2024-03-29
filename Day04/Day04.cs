﻿using AdventOfCode2021.Common;
using AdventOfCode2021.Day04;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Challenges {
    internal class Day04 : Challenge {
        private readonly Queue<int> balls;
        private readonly List<Board> boards;

        public Day04(bool testInput = false) : base(testInput) {
            if (balls is null) {
                balls = new Queue<int>();
                foreach (int number in input[0].Split(',').Select(int.Parse)) {
                    balls.Enqueue(number);
                }
            }
            if (boards is null) {
                boards = new();
                for (int i = 2; i < input.Length; i += 6) {
                    var board = new string[5];
                    Array.Copy(input,i,board,0,board.Length);
                    boards.Add(new(board));
                }
            }
        }

        public override string FirstResult() {
            var tempBalls = new Queue<int>(balls);
            while (tempBalls.Count > 0) {
                int ball = tempBalls.Dequeue();
                foreach (Board board in boards) {
                    board.CheckNumber(ball);
                    if (board.HasBingo) {
                        return Board.GetBoardAnswer(board, ball);
                    }
                }
            }
            return default;
        }

        public override string SecondResult() {
            Board winnerBoard = null;
            int winnerBall = -1;
            var tempBalls = new Queue<int>(balls);

            while (tempBalls.Count > 0) {
                int ball = tempBalls.Dequeue();
                foreach (Board board in boards) {
                    if (!board.winner) {
                        board.CheckNumber(ball);
                        if (board.HasBingo) {
                            board.winner = true;
                            winnerBoard = board;
                            winnerBall = ball;
                        }
                    }
                }
            }
            if(winnerBoard is not null && winnerBall != -1) {
                return Board.GetBoardAnswer(winnerBoard, winnerBall);
            }
            return default;
        }
    }
}
