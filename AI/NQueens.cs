using System;
using System.Collections.Generic;

public class Board
{
    private int[] queens; // Queens placements
    private int[] dp; // Positive diagonal
    private int[] dn; // Negative diagonal
    private int[] row; // The number of queens on the [index] row
    private int N;
    private int diagonalSize = 0;

    Board(int N)
    {
        this.N = N;
        diagonalSize = 2 * N - 1;
        queens = new int[N];
        dp = new int[diagonalSize];
        dn = new int[diagonalSize];
        row = new int[N];

        FillQueens(N);
        FillConflicts();
    }

    int GetRow(int col)
    {
        return queens[col];
    }

    private int GetRandom(int n)
    {
        return new Random().Next(n);
    }

    void Restart()
    {
        FillQueens(N);
        row = new int[N];
        dp = new int[diagonalSize];
        dn = new int[diagonalSize];
        FillConflicts();
    }

    void FillQueens(int N)
    {
        for (int i = 0; i < N; i++)
        {
            row[i]++;
            row[queens[i]]++; // 

            dn[i + queens[i]]++;
            dp[(i - queens[i] + N - 1)]++;
        }
    }

    void FillConflicts()
    {
        for (int i = 0; i < N; i++)
        {
            row[queens[i]]++; // 
            dn[i + queens[i]]++; // Col + Row gets the negative diagonal 
            dp[(i - queens[i]) + N - 1]++; // Col - Row gets the negative diagonal 
        }
    }

    // Gets the collumn with max conflicts
    int MaxConflictCollumn()
    {
        var maxIndex = new List<int>();
        var maxVal = 0;
        var sum = 0;

        // Check each queen (collumn)
        for (int i = 0; i < N; i++)
        {
            sum = row[queens[i]] + dp[i - queens[i] + N - 1] + dn[i + queens[i]] - 3;
            if (sum > maxVal)
            {
                maxIndex = new List<int>();
                maxVal = sum;
            }
            if (sum == maxVal)
            {
                maxIndex.Add(i);
            }
        }
        // Check if its solved
        if (maxVal == 0)
        {
            return -1;
        }
        // Return a random queen from those with the most conflicts
        else
        {
            return maxIndex[GetRandom(maxIndex.Count)];
        }
    }

    // Gets the row with the minimum conflicts
    int MinConflictRow(int colIndex)
    {
        var minVal = Int32.MaxValue;
        var minIndex = new List<int>();
        var sum = 0;

        for (int i = 0; i < N; i++)
        {
            if (i == queens[colIndex])
            {
                continue;
            }
            sum = this.row[i] + this.dp[colIndex - i + this.N - 1] + this.dn[colIndex + i];

            if (sum < minVal)
            {
                minVal = sum;
                minIndex = new List<int>();
            }
            if (sum == minVal)
            {
                minIndex.Add(i);
            }
        }
        return minIndex[GetRandom(minIndex.Count)];
    }

    // Move the rows up/down
    void Swap(int col, int nextRow)
    {
        var prevRow = GetRow(col);
        queens[col] = nextRow;
        row[prevRow]--;
        row[nextRow]++;
        dp[col - prevRow + N - 1]--;
        dp[col - nextRow + N - 1]++;
        dn[col + prevRow]--;
        dn[col + nextRow]++;
    }

    void Print()
    {
        for (int i = 0; i < N; i++)
        {
            var line = "";
            for (int j = 0; j < N; j++)
            {
                if (i == queens[j])
                    line += "*";
                else
                {
                    line += "_";
                }
            }
            Console.WriteLine(line);
        }
    }

    void Solve()
    {
        var i = 1;
        var start = DateTime.Now;

        while (true)
        {
            var maxConf = MaxConflictCollumn();
            if (i > 0.50 * N)
            {
                i = 1;
                Restart();
                continue;
            }
            if (maxConf == -1)
            {
                var end = DateTime.Now;
                Console.WriteLine(end - start);
                break;
            }
            i++;
            // Swap one of the minimum conflict ones with one of the max conflict
            var minRow = this.MinConflictRow(maxConf);
            Swap(maxConf, minRow);
        }
    }
    static void Main(string[] args)
    {
        //int n = Int32.Parse(Console.ReadLine());
        int n = 10000;
        var q = new Board(n);
        q.Solve();
        //q.Print();
    }
}


