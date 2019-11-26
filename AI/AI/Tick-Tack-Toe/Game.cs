using System;
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

        public Game(char first)
        {
            state = new State();
            isComputerFirst = first == 'H' ? false : true;

            maxPlayer = isComputerFirst ? 'X' : 'O';
            minPlayer = !isComputerFirst ? 'X' : 'O';
        }

        private State MakeBestMove()
        {
            List<char[][]> boards = new List<char[][]>();
            List<int> results = new List<int>();


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

                        var tempState = new State(board, 0);

                        int result = Minimax(tempState, 0, false);

                        results.Add(result);

                        state.board[i][j] = Player.Empty;
                    }
                }
            }

            int maxIndex = results.IndexOf(results.Max());
            var bestBoard = boards[maxIndex];

            State bestState = new State(bestBoard, 0);

            return bestState;
        }


        private void Conclusion(State state)
        {
            state.depth = 0;

            state.Print();

            if (Evaluate(state) == -10)
            {
                Console.WriteLine("Humanity survives!");
            }
            else if (Evaluate(state) == 10)
            {
                Console.WriteLine("Computero is glorious!");
            }
            else
            {
                Console.WriteLine("Draw!");
            }
        }

        private int Evaluate(State state)
        {
            if (state.isGameWon(maxPlayer))
                return 10 - state.depth;
            else if (state.isGameWon(minPlayer))
                return state.depth - 10;
            else
                return 0;
        }

        private int Minimax(State state, int depth, bool isMaximizingPlayer)
        {
            state = new State(state.board, depth);
            int score = Evaluate(state);

            if (score != 0)
                return score;
            if (state.IsGameFinished())
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

                            int value = Minimax(state, depth + 1, false);

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

                            int value = Minimax(state, depth + 1, true);

                            bestVal = Math.Min(bestVal, value);

                            state.board[i][j] = Player.Empty;
                        }
                    }
                }
                return bestVal;
            }
        }

        private int[] CheckInput()
        {
            var input = Console.ReadLine().Split(' ');

            // Check if its a request for a restart
            if (input[0] == "R")
            {
                state = new State();
                isComputerFirst = maxPlayer == 'X' ? true : false;
                return new int[] { -1, -1 };
            }

            int[] numbers = Array.ConvertAll(input, int.Parse);


            // Check number of inputs
            if (numbers.Length != 2)
            {
                Console.WriteLine("Bad input!");
                return new int[] { -1, -1 };
            }
            else
            {
                int i = numbers[0];
                int j = numbers[1];

                // Normalize
                i--;
                j--;

                // Check in board dimension
                if (i >= 3 || i < 0 || j >= 3 || j < 0)
                {
                    Console.WriteLine("Bad input!");
                    return new int[] { -1, -1 };
                }
                // Check if it ain empty
                else if (state.board[i][j] != Player.Empty)
                {
                    Console.WriteLine("Bad input!");
                    return new int[] { -1, -1 };
                }
                return new int[] { i, j };
            }
        }

        public void Play()
        {
            while (!state.IsGameFinished())
            {
                // Print state
                state.Print();

                // Let computer start first once
                if (isComputerFirst)
                {
                    state = MakeBestMove();
                    isComputerFirst = false;
                }
                else
                {
                    // Humanoid makes decision
                    var checker = CheckInput();

                    int i = checker[0];
                    int j = checker[1];

                    if (i == -1)
                    {
                        Play();
                        break;
                    }

                    state.board[i][j] = minPlayer;

                    // Did you beat me or are we draw?
                    if (state.IsGameFinished())
                    {
                        Conclusion(state);
                        break;
                    }

                    // Let me try.
                    // Computero makes decision
                    state = MakeBestMove();


                    // Did you beat me or are we draw?
                    if (state.IsGameFinished())
                    {
                        Conclusion(state);
                        break;
                    }
                }
            }
        }
    }
}
