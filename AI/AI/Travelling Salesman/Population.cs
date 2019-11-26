using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_HW3
{
    public class Population
    {
        private List<Chromosome> chromosomes;
        private List<City> cities;
        private Chromosome fittest;

        Random Random = new Random();
        
        // Hard-code the rules of life
        int elites = 6;
        int populationSize = 200;

        public Population(int numberOfCities)
        {
            FillCities(numberOfCities);
            CreateFirstborns(numberOfCities, populationSize);
            this.fittest = FindBestInPopulation();
        }

        public Population(List<Chromosome> chromosomes)
        {
            this.chromosomes = new List<Chromosome>(chromosomes);
            this.fittest = FindBestInPopulation();
        }

        // Getters
        public List<Chromosome> GetChromosomes()
        {
            return chromosomes;
        }

        public Chromosome GetFittest()
        {
            return fittest;
        }
       
        // Find the fittest one
        public Chromosome FindBestInPopulation()
        {
            var best = 0.0;

            var bestChromo = new Chromosome(chromosomes[0]);

            foreach (Chromosome c in chromosomes)
            {
                if (c.GetFitness() > best)
                {
                    best = c.GetFitness();
                    bestChromo = new Chromosome(c);
                }
            }

            return bestChromo;
        }

        // Create a random map of cities.
        private void FillCities(int numberOfPoints)
        {
            cities = new List<City>();
            for (int i = 0; i < numberOfPoints; i++)
            {
                cities.Add(new City(Random.Next(numberOfPoints), Random.Next(numberOfPoints)));
            }
        }

        // Give birth to the Generation Zero
        void CreateFirstborns(int size, int sizeOfPopulation)
        {
            chromosomes = new List<Chromosome>();
            for (int i = 0; i < sizeOfPopulation; i++)
            {
                chromosomes.Add(new Chromosome(size, cities));
            }
        }

        // Equality is set-up. End of communism.
        // Everyone is a different their part of the 100%.
        public void NormalizeFitness()
        {
            double sum = 0;
            for (int i = 0; i < chromosomes.Count(); i++)
            {
                sum += chromosomes[i].GetFitness();
            }
            for (int i = 0; i < chromosomes.Count(); i++)
            {
                chromosomes[i].SetFitness(chromosomes[i].GetFitness() / sum);
            }
        }

        // Put up a fight.
        // Return the mightiest of them all and mess the rest up a little.
        public Population Survive()
        {
            Population best = Elitism();
            Population newPopulation = NewGeneration(populationSize - elites);

            return new Population(best.GetChromosomes()
                                        .Concat(newPopulation.GetChromosomes())
                                        .ToList());
        }

        // Time of the next era.
        Population NewGeneration(int size)
        {
            List<Chromosome> population = new List<Chromosome>();
            for (int i = 0; i < size; i++)
            {
                var mother = Selection();
                var father = Selection();

                Chromosome c = mother.Crossover(father);

                // Grow a few tails
                foreach (City n in c.GetOrder())
                {
                    c = c.Mutate();
                }

                population.Add(c);
            }

            return new Population(population);
        }

        // Get the absolute elite of the generation.
        public Population Elitism()
        {
            List<Chromosome> bestOnes = new List<Chromosome>();
            Population tempPopulation = new Population(chromosomes);

            for (int i = 0; i < elites; i++)
            {
                var bestInPop = tempPopulation.FindBestInPopulation();
                bestOnes.Add(bestInPop);
                tempPopulation = new Population(tempPopulation.GetChromosomes()
                                                              .Except(bestOnes)
                                                              .ToList());
            }

            return new Population(bestOnes);
        }

        // Select a worthy chromo.
        Chromosome Selection()
        {
            while (true)
            {
                int i = Random.Next(0, chromosomes.Count());
                var rndm = (double)Random.NextDouble();
                var compare = chromosomes[i].GetFitness() / fittest.GetFitness();

                if (rndm < compare)
                {
                    return new Chromosome(chromosomes[i]);
                }
            }
        }

        public void Display(int generation)
        {
            Chromosome bestSolution = FindBestInPopulation();

            Console.WriteLine($"Generation {generation}");
            Console.WriteLine($"Best fitness: {bestSolution.GetFitness()}");
            Console.WriteLine($"Best distance: {bestSolution.GetDistance()}\n");
        }
    }
}
