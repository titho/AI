﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Tick_Tack_Toe
{
    public class Game
    {
        State state;
        bool isComputerFirst;
        char maxPlayer;
        char minPlayer;

        public Game(char isComputerFirst)
        {
            state = new State();
            this.isComputerFirst = isComputerFirst == 'H' ? false : true;

            maxPlayer = isComputerFirst == 'H' ? Player.O : Player.X;
            minPlayer = isComputerFirst == 'H' ? Player.X : Player.O;
        }

        public void Play(char first)
        {
            //state.board = new char[,]
            //{
            //    {'X', 'O', 'X' },
            //    {'X', 'O', '_' },
            //    {'O', '_', '_' },
            //};


            while (true)
            {
                state.Print();
                if (isComputerFirst)
                {
                    state = MakeBestMove();
                    isComputerFirst = false;
                }
                else
                {
                    // Humanoid makes decision
                    var input = Console.ReadLine().Split(' ');

                    int[] numbers = Array.ConvertAll(input, int.Parse);

                    int i = 0;
                    int j = 0;

                    if (numbers.Length != 2)
                    {
                        Console.WriteLine("Bad input!");
                        Play(first);
                        break;
                    }
                    else
                    {
                        i = numbers[0];
                        j = numbers[1];

                        if (i >= 3 || i < 0 || j >= 3 || j < 0)
                        {
                            Console.WriteLine("Bad input!");
                            Play(first);
                            break;
                        }
                        else if (state.board[i][j] != Player.Empty)
                        {
                            Console.WriteLine("Bad input!");
                            Play(first);
                            break;
                        }
                    }

                    state.board[i][j] = minPlayer;

                    if (state.IsFinished())
                    {
                        break;
                    }

                    // Computero makes decision
                    state = MakeBestMove();

                    if (state.IsFinished())
                    {
                        break;
                    }
                }
            }
            state.Print();
        }

        State MakeBestMove()
        {
            State bestState = new State();
            List<char[][]> boards = new List<char[][]>();
            List<int> results = new List<int>();
            int bestVal = Int32.MinValue;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state.board[i][j] == Player.Empty)
                    {
                        state.board[i][j] = maxPlayer;

                        var board = new char[3][];

                        for (int k = 0; k < 3; k++)
                        {
                            board[k] = (char[])state.board[k].Clone();
                        }

                        boards.Add(board);
                        var newState = new State(board);
                        int result = Minimax(newState, 0, !isComputerFirst);

                        results.Add(result);


                        if (result > bestVal)
                        {
                            bestVal = result;
                        }

                        state.board[i][j] = Player.Empty;
                    }
                }
            }
            int maxIndex = results.IndexOf(results.Max());
            var bestBoard = boards[maxIndex];
            bestState.board = bestBoard;

            return bestState;
        }

        int Minimax(State state, int depth, bool isMaximizingPlayer)
        {
            state = new State(state.board);

            int score = state.Evaluate();

            if (score == 10 || score == -10)
                return score;
            if (state.IsFinished())
                return 0;

            if (isMaximizingPlayer)
            {
                int bestVal = -1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var cell = state.board[i][j];

                        if (cell == Player.Empty)
                        {
                            state.board[i][j] = maxPlayer;
                            state.depth += 1;

                            int value = Minimax(state, depth + 1, false);/**/

                            bestVal = Math.Max(bestVal, value);

                            state.board[i][j] = Player.Empty;
                        }
                    }
                }
                return bestVal;
            }
            else
            {
                int bestVal = 1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var cell = state.board[i][j];

                        if (cell == Player.Empty)
                        {
                            state.board[i][j] = minPlayer;
                            state.depth += 1;

                            int value = Minimax(state, depth + 1, true);

                            bestVal = Math.Min(bestVal, value);

                            state.board[i][j] = Player.Empty;
                        }
                    }
                }
                return bestVal;
            }
        }
    }
}
