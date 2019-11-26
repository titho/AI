using System;
using System.Collections.Generic;
using System.Text;

namespace AI_HW3
{
    public class City
    {
        // The city's x coordinate.
        private readonly int x;

        // The city's y coordinate.
        private readonly int y;
        
        public City(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}
