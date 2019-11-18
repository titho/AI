using System;
using System.Collections.Generic;
using System.Text;

namespace AI_HW3
{
    // You secret lab for evil DNA experiments
    //               >:)
    public class Lab
    {
        // Number of cities
        int _size;

        // Hard code on 200 for now
        int _N = 200;

        // The top notch heartbreaker
        Chromosome _bestEver;
        double _recordDistance;

        // All possible orders (200)
        Chromosome[] _population;
        City[] _allCities;

        Random Random = new Random();

        // Best distance
        //int _recordDistance;
        //int[] order;

        //double[] Fitness;


        public Lab(int size)
        {
            _size = size;
            _recordDistance = Int32.MaxValue;
            FillCities();
            _bestEver = new Chromosome(_size, _allCities);

            CreateFirstborns();
        }

        public Chromosome GetBestEver()
        {
            return _bestEver;
        }

        public int GetRecordDistance()
        {
            return _bestEver.GetDistance();
        }
        public double GetDistance(Chromosome chromosome)
        {
            return chromosome.CalculateDistance();
        }

        public double GetFitness(Chromosome chromosome)
        {
            return chromosome.CalculateFitness();
        }

        // Give birth to the Generation Zero
        void CreateFirstborns()
        {
            _population = new Chromosome[_N];
            for (int i = 0; i < _N; i++)
            {
                _population[i] = new Chromosome(_size, _allCities);
            }
        }

        // Create a random map of cities.
        private void FillCities()
        {
            _allCities = new City[_size];
            for (int i = 0; i < _size; i++)
            {
                _allCities[i] = new City(Random.Next(_size), Random.Next(_size));
            }
        }

        // Equality is set-up. End of communism.
        // Everyone is a percent of the 100%.
        public void NormalizeFitness()
        {
            double sum = 0;
            for (int i = 0; i < _N; i++)
            {
                sum += GetFitness(_population[i]);
            }
            for (int i = 0; i < _N; i++)
            {
                _population[i].SetFitness(GetFitness(_population[i]) / sum);
            }
        }

        void Mutate()
        {
            //var IndexA = Random.Next(_size - 1);
            //var IndexB = Random.Next(_size - 1);
            //Swap(order, IndexA, IndexB);
        }

        // "And may the strongest win."
        //                              - somebody
        //
        public void Survival()
        {
            NormalizeFitness();

            Chromosome bestNow = new Chromosome(_size, _allCities);

            double minDistance = Int32.MaxValue;
            double maxDistance = 0;

            for (int i = 0; i < _N; i++)
            {
                var d = GetDistance(_population[i]);

                // Is this the most vicious
                // of the all?
                if(d < _recordDistance)
                {
                    _recordDistance = d;
                    _bestEver =  new Chromosome(_population[i]);
                }

                // Is it atleast the alpha of the generation?
                if(d < minDistance)
                {
                    minDistance = d;
                    bestNow = new Chromosome(_population[i]);
                }

                // Are you the weakling?
                if(d > maxDistance)
                {
                    maxDistance = d;
                }
            }
            // SHOW YOURSELF, OH MIGHTY WARRIOR!
            Console.Write($"Baddest mf for Round N: ");
            for (int j = 0; j < _size; j++)
            {
                Console.Write(bestNow.GetOrder()[j].ToString());
                Console.Write(" ");
            }
            Console.Write(" | With " + _recordDistance);
            Console.WriteLine();

            bestNow.PrintOrder();
            

            NormalizeFitness();
            Selection();
        }

        // Time to get pickedy-picky
        void Selection()
        {
            var newPopulation = new Chromosome[_N];
            for (int i = 0; i < _N; i++)
            {
                var index = 0;
                var r = Random.NextDouble();

                while (r > 0)
                {
                    r -= GetFitness(_population[i]);
                    index++;
                }
                index--;

                newPopulation[i] = new Chromosome(_population[index]);
            }

            _population = (Chromosome[])(newPopulation).Clone();
        }

        //int[] Shuffle(int[] arr)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var IndexA = random.Next(arr.Length - 1);
        //        var IndexB = random.Next(arr.Length - 1);
        //        Swap(arr, IndexA, IndexB);
        //    }
        //    return arr;
        //}

        //void Swap(int[] arr, int a, int b)
        //{
        //    var temp = arr[a];
        //    arr[a] = arr[b];
        //    arr[b] = temp;
        //}

        //public City GetCity(int i)
        //{
        //    return Cities[i];
        //}



        //public int Distance(City A, City B)
        //{
        //    int xdiff = A.GetX() - B.GetX();
        //    int ydiff = A.GetY() - B.GetY();
        //    return (int)Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
        //}



        //private double CalculateDistance(int[] order)
        //{
        //    int sum = 0;
        //    for (int i = 0; i < order.Length - 1; i++)
        //    {
        //        int cityAIndex = order[i];
        //        City cityA = GetCity(cityAIndex);
        //        int cityBIndex = order[i + 1];
        //        City cityB = GetCity(cityBIndex);
        //        var d = Distance(cityA, cityB);
        //        sum += d;
        //    }
        //    return (double)sum;
        //}

        //public void CalculateFitness()
        //{
        //    for (int i = 0; i < population.Length - 1; i++)
        //    {
        //        var distance = CalculateDistance(population[i]);
        //        if (distance < recordDistance)
        //        {
        //            recordDistance = (int)distance;
        //            bestEver = (int[])(population[i]).Clone();
        //        }
        //        // Take a number and make it the higher it is, the lower value
        //        // And the lower it is, a higher value
        //        Fitness[i] = 1 / (distance + 1);
        //    }
        //}



        // WTF at 21:41
        //int[] PickOne(int[][] population, double[] probability)
        //{
        //    var index = 0;
        //    double r = random.NextDouble();

        //    while (r > 0)
        //    {
        //        r = r - probability[index];
        //        index++;
        //    }
        //    index--;
        //    return (int[])(population[index]).Clone();
        //}

        //public void NextGeneration()
        //{
        //    var newPopulation = new int[N][];
        //    for (int i = 0; i < population.Length - 1; i++)
        //    {
        //        var newOrder = new int[N];
        //        var index = 0;
        //        double r = random.NextDouble();

        //        while (r > 0)
        //        {
        //            r = r - Fitness[index];
        //            index++;
        //        }
        //        index--;

        //        newOrder[index] = (int[])(population[index]).Clone();
        //        Mutate(order);


        //        return (int[])(population[index]).Clone();

        //        newPopulation[i] = order;
        //    }
        //    population = (int[][])(newPopulation).Clone();
        //}


    }
}
