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

        public Game(char isComputerFirst)
        {
            state = new State();
            this.isComputerFirst = isComputerFirst == 'H' ? false : true;

            maxPlayer = isComputerFirst == 'H' ? Player.O : Player.X;
            minPlayer = isComputerFirst == 'H' ? Player.X : Player.O;
        }

        public void Play()
        {
            while (!state.IsFinished())
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
                    var input = Console.ReadLine().Split(' ');

                    if (input[0] == "R")
                    {
                        state = new State();
                        isComputerFirst = maxPlayer == 'X' ? true : false;
                        Play();
                    }

                    int[] numbers = Array.ConvertAll(input, int.Parse);

                    int i = 0;
                    int j = 0;

                    // Check number of inputs
                    if (numbers.Length != 2)
                    {
                        Console.WriteLine("Bad input!");
                        Play();
                        break;
                    }
                    else
                    {
                        i = numbers[0];
                        j = numbers[1];

                        i--;
                        j--;

                        // Check in board dimension
                        if (i >= 3 || i < 0 || j >= 3 || j < 0)
                        {
                            Console.WriteLine("Bad input!");
                            Play();
                            break;
                        }
                        // Check if it ain empty
                        else if (state.board[i][j] != Player.Empty)
                        {
                            Console.WriteLine("Bad input!");
                            Play();
                            break;
                        }
                    }

                    // Okay, go for it

                    state.board[i][j] = minPlayer;

                    // Did you beat me or are we draw?
                    if (state.IsFinished())
                    {
                        break;
                    }

                    // Let me try.
                    // Computero makes decision
                    state = MakeBestMove();

                    //// Did I beat you or are we draw?
                    //if (state.IsFinished())
                    //{
                    //    break;
                    //}
                }
            }
            state.depth = 0;
            if (Evaluate(state) == -10)
            {
                Console.WriteLine("Computero is glorious!");
            }
            else if (Evaluate(state) == 10)
            {
                Console.WriteLine("Humanity survives!");
            }
            else
            {
                Console.WriteLine("Draw!");
            }
            state.Print();
        }

        State MakeBestMove()
        {
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
                        var newState = new State(board, 0);
                        int result = Minimax(newState, 0, false);

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

            State bestState = new State(bestBoard, 0);

            return bestState;
        }

        public int Evaluate(State state)
        {
            if (state.isGameWon(maxPlayer))
                return 10 - state.depth;
            else if (state.isGameWon(minPlayer))
                return state.depth - 10;
            else
                return 0;
        }

        int Minimax(State state, int depth, bool isMaximizingPlayer)
        {
            state = new State(state.board, depth);
            int score = Evaluate(state);

            if (score != 0)
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
    }
}
