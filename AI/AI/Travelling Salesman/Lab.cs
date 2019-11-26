using System;

namespace AI_HW3
{
    // The secret lab for evil DNA experiments
    //                      >:)
    public class Lab
    {
        private Population currentPopulation;
        private readonly Random Random = new Random();

        public void Life(int size)
        {
            bool isBetterSolution = false;
            currentPopulation = new Population(size);

            int count = 0;

            for (int generation = 0; generation < 1000; generation++)
            {
                if (generation == 0 || generation == 10)
                {
                    currentPopulation.Display(generation);
                }
                else if (isBetterSolution && Random.NextDouble() > 0.3 && count < 3 && generation > 10)
                {
                    count++;
                    currentPopulation.Display(generation);
                }

                isBetterSolution = false;

                double prevFittest = currentPopulation.FindBestInPopulation().GetFitness();

                Population newPopulation = currentPopulation.Survive();

                double newFittest = newPopulation.FindBestInPopulation().GetFitness();


                if (prevFittest < newFittest)
                {
                    isBetterSolution = true;
                }

                currentPopulation = newPopulation;
            }
            currentPopulation.Display(1000);
        }
    }
}
