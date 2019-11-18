using System;
using System.Collections.Generic;
using System.Text;

namespace AI_HW3
{
    public static class Driver
    {
        private static void Main(string[] args)
        {

            int n = 10;
            var lab = new Lab(n);

            // Time for the game of life
            // For 100 rounds
            for (int i = 0; i < 100; i++)
            {
                lab.Survival();
            }
        }
    }
}