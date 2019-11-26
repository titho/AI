﻿using System;

namespace AI.Tick_Tack_Toe
{
    public class State
    {
        public char[][] board;
        public int depth;

        public State()
        {
            board = new char[3][];
            depth = 0;
            Initialise();
        }

        public State(char[][] board)
        {
            this.board = new char[3][];
            for (int i = 0; i < 3; i++)
            {
                this.board[i] = new char[3];
                for (int k = 0; k < 3; k++)
                {
                    this.board[i][k] = board[i][k];
                }
            }
            depth = 0;
        }

        public void SetBoard(char[][] board)
        {
            this.board = (char[][])board.Clone();
        }

        private void Initialise()
        {
            for (int i = 0; i < 3; i++)
            {
                board[i] = new char[3];
                for (int j = 0; j < 3; j++)
                {
                    board[i][ j] = '_';
                }
            }
        }

        public void Print()
        {
            Console.WriteLine("  ________");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(" |");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" " + board[i][ j]);
                }
                Console.Write(" |");
                Console.WriteLine();
            }
            Console.WriteLine("  ________");
            Console.WriteLine();

        }

        public bool IsFinished()
        {
            if (Evaluate() == -10 || Evaluate() == 10)
            {
                return true;
            }

            foreach (char[] row in board)
            {
                foreach (var position in row)
                {
                    if (position == '_')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int Evaluate()
        {
            if (isGameWon(Player.O))
                return 10 - depth;
            else if (isGameWon(Player.X))
                return depth - 10;
            else
                return 0;
        }

        bool isGameWon(char pl)
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row][0] == board[row][ 1] && board[row][ 1] == board[row][ 2] && board[row][ 0] == pl)
                {
                    return true;
                }
            }

            // Check cols
            for (int col = 0; col < 3; col++)
            {
                if (board[0][col] == board[1][col] && board[1][ col] == board[2][ col] && board[0][ col] == pl)
                {
                    return true;
                }
            }

            // Check diagonals
            if (board[0][0] == board[1][ 1] && board[1][ 1] == board[2][ 2] && board[0][0] == pl)
            {
                return true;
            }

            if (board[0][ 2] == board[1][ 1] && board[1][ 1] == board[2][ 0] && board[0][ 2] == pl)
            {
                return true;
            }

            return false;
        }

    }
}
