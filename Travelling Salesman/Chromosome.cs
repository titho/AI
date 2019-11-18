using System;
using System.Text;

namespace AI_HW3
{
    public class Chromosome
    {
        double _fitness;
        int _distance;
        int[] _order;
        City[] cities;

        int _size;
        Random Random = new Random();

        //public Chromosome(double fitness, int distance, int N, City[] cities)
        //{
        //    this._fitness = fitness;
        //    this._distance = distance;
        //    this._size = N;
        //    this.cities = (City[])(cities).Clone();

        //    Fill(N);
        //    Shuffle();
        //}

        public Chromosome(int N, City[] cities)
        {
            this._size = N;

            Fill(N);
            this.cities = (City[])(cities).Clone();

            this.cities = cities;
        }

        public Chromosome(Chromosome copy)
        {
            this._fitness = copy._fitness;
            this._distance = copy._distance;
            this._size = copy._size;
            this.cities = (City[])(copy.cities).Clone();

            _order = (int[])(copy._order).Clone();
        }
        
        public int GetDistance()
        {
            return _distance;
        }

        public int[] GetOrder()
        {
            return _order;
        }

        //public int GetCity(int i)
        //{
        //    return _order[i];
        //}

        public void SetFitness(double fitness)
        {
            this._fitness = fitness;
        }

        private void Fill(int N)
        {
            _order = new int[N];
            for (int i = 0; i < N; i++)
            {
                _order[i] = i;
            }

            // Shuffle it 20 times
            for(int i = 0; i < 20; i++)
            {
                Shuffle();
            }
        }

        private void Shuffle()
        {
            var IndexA = Random.Next(_size - 1);
            var IndexB = Random.Next(_size - 1);
            Swap(_order, IndexA, IndexB);
        }

        void Swap(int[] arr, int a, int b)
        {
            var temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        private int Distance(City A, City B)
        {
            int xdiff = A.GetX() - B.GetX();
            int ydiff = A.GetY() - B.GetY();
            return (int)Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
        }

        public double CalculateDistance()
        {
            int sum = 0;
            for (int i = 0; i < _size - 1; i++)
            {
                int cityAIndex = _order[i];
                City cityA = cities[cityAIndex];

                int cityBIndex = i + 1;
                City cityB = cities[cityBIndex];

                var d = Distance(cityA, cityB);
                sum += d;
            }
            this._distance = sum;
            return (double)sum;
        }

        public double CalculateFitness()
        {
            var distance = CalculateDistance();
            _fitness = 1 / (distance + 1);
            return _fitness;
        }

        public void Mutate()
        {
            this.Shuffle();
        }

        public void PrintOrder()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    for(int k = 0; k < _size; k++)
                    {
                        if (cities[k].GetX() == j && cities[k].GetY() == i)
                        {
                            Console.Write(k);
                        }
                    }
                    Console.Write("-");

                }
                Console.WriteLine();
            }
        }

    }

}
