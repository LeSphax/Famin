using UnityEngine;
using System.Collections;
using System;

public class FoodUpdater : IUpdatingStrategy
{

    Ressources ressources;
    const double FARMING_EFFICIENCY = 9;
    const double GATHERING_EFFICIENCY = 1.35;
    Logger logger;
    int timerDeath;
    int roundsBetweenDeaths = 3;
    int severalDeathsThreshold = 20;

    new void Awake()
    {
        base.Awake();
        logger = Logger.GetInstance();
        ressources = Ressources.GetInstance();
        timerDeath = 0;
        this.RessourceName = Data.FOOD;
    }

    public override double CalculateIncrement()
    {
        double recolt = 0;
        if (Seasons.GetCurrentSeason() == Seasons.SUMMER)
        {
            recolt = Math.Min(jobs.GetNumberOf(Data.FARMERS) * FARMING_EFFICIENCY, ressources.GetNumberOf(Data.PLANTED_FOOD));
            ressources.Add(Data.PLANTED_FOOD, -recolt * RessourceUpdater.updateInterval);
            recolt += jobs.GetNumberOf(Data.GATHERERS) * GATHERING_EFFICIENCY;
        }
        else if (Seasons.GetCurrentSeason() == Seasons.WINTER)
        {
            recolt = jobs.GetNumberOf(Data.GATHERERS)* 0.5;
        }
        else
        {
            recolt = jobs.GetNumberOf(Data.GATHERERS) * GATHERING_EFFICIENCY;
        }
        
        return Eat(recolt);
    }

    double Eat(double recolt)
    {
        recolt -= jobs.GetTotalPopulation();

        if (timerDeath > 0) timerDeath--;

        if (ressources.GetNumberOf(Data.FOOD) + recolt < 0)
        {
            StarveWorkers();
        }
        return recolt;
    }

    void StarveWorkers()
    {
        if (timerDeath == 0)
        {
            logger.PutLine("A worker starved to death!");
            jobs.KillPeople(1 + (int)(ressources.GetNumberOf(Data.FOOD)) / -severalDeathsThreshold);
            ressources.SetNumberOf(Data.FOOD, 0);
            timerDeath = roundsBetweenDeaths;
        }
    }

}
