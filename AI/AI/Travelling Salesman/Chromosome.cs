using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_HW3
{
    public class Chromosome
    {
        int size;
        double fitness;
        int distance;

        double mutationRate = 0.04;

        List<City> order;

        Random Random = new Random();

        public Chromosome(int size, List<City> order)
        {
            this.size = size;
            this.order = new List<City>(order);
            Shuffle(42);
            this.distance = CalculateDistance();
            this.fitness = CalculateFitness();
        }

        public Chromosome(Chromosome copy)
        {
            this.fitness = copy.fitness;
            this.distance = copy.distance;
            this.size = copy.size;
            this.order = new List<City>(copy.GetOrder());
        }

        public int GetDistance()
        {
            return distance;
        }

        public List<City> GetOrder()
        {
            return order;
        }

        public double GetFitness()
        {
            return fitness;
        }

        public void SetOrder(List<City> order)
        {
            this.order = new List<City>(order);
        }

        public void SetFitness(double fitness)
        {
            this.fitness = fitness;
        }

        private void Shuffle(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var IndexA = Random.Next(size - 1);
                var IndexB = Random.Next(size - 1);
                Swap(order, IndexA, IndexB);
            }
        }

        void Swap(List<City> arr, int a, int b)
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

        // Calculate the distance of the current route
        public int CalculateDistance()
        {
            int sum = 0;
            for (int i = 0; i < size - 1; i++)
            {
                City cityA = order[i];

                City cityB = order[i + 1];

                var d = Distance(cityA, cityB);
                sum += d;
            }
            this.distance = sum;
            return sum;
        }

        // Calculate the fitness of the current route
        public double CalculateFitness()
        {
            var distance = CalculateDistance();
            fitness = 1.0 / (distance + 1.0);
            return fitness;
        }

        // "Let's do it how they do it on discovery channel."
        public Chromosome Crossover(Chromosome partner)
        {
            var IndexA = Random.Next(0, size);
            var IndexB = Random.Next(IndexA, size);

            var selected = order.GetRange(IndexA, IndexB - IndexA + 1);
            var rest = partner.GetOrder().Except(selected).ToList();
            
            var generation = rest.Take(IndexA)
                                 .Concat(selected)
                                 .Concat(rest.Skip(IndexA))
                                 .ToList();

            return new Chromosome(size, generation);
        }

        public Chromosome Mutate()
        {
            var mutated = new List<City>(order);
            if (Random.NextDouble() < mutationRate)
            {
                Swap(mutated, Random.Next(size), Random.Next(size));
            }
            return new Chromosome(size, mutated);
        }
    }
}
