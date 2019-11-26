using AI.Tick_Tack_Toe;
using AI_HW3;
using System;

namespace AI
{
    class Driver
    {
        static void Main(string[] args)
        {
            //int n = Int32.Parse(Console.ReadLine());

            // Run Life
            //var lab = new Lab();
            //lab.Life(n);

            // TIME TO PLAY THE GAME
            Console.WriteLine("Who will go first? (C)omputer or (H)uman?");
            char first = Console.ReadLine()[0];
            while(first != 'C' && first != 'H')
            {
                Console.WriteLine("Please enter C for computer and H for human.");
                first = Console.ReadLine()[0];
            }
            var ticktacktoe = new Game(first);

            ticktacktoe.Play();
        }
    }
}
