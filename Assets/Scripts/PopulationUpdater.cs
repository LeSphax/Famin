using UnityEngine;
using System.Collections;

public class PopulationUpdater : MonoBehaviour
{

    Ressources ressources;
    Jobs jobs;
    Logger logger;
    int timerDeath;
    int roundsBetweenDeaths;
    int severalDeathsThreshold = 20;

    void Awake()
    {
        logger = Logger.GetInstance();
        jobs = Jobs.GetInstance();
        ressources = Ressources.GetInstance();
        timerDeath = 0;
        Time.fixedDeltaTime = 1;
    }

    void FixedUpdate()
    {
        ressources.Add(Data.FOOD, -jobs.GetTotalPopulation());
        if (timerDeath > 0) timerDeath--;
        if (ressources.GetNumberOf(Data.FOOD) < 0 && timerDeath == 0)
        {
            logger.PutLine("A worker starved to death!");
            jobs.KillPeople(1 + (int)(ressources.GetNumberOf(Data.FOOD)) / -severalDeathsThreshold);
            ressources.SetNumberOf(Data.FOOD, 0);
            timerDeath = roundsBetweenDeaths;
        }
        /*if (Seasons.GetCurrentSeason() == Seasons.WINTER)
        {
            ressources.Add(Data.WOOD, -jobs.GetTotalPopulation());
            if (ressources.GetNumberOf(Data.WOOD) < 0)
            {
                logger.PutLine("A worker freezed to death!");
                jobs.KillPeople(1 + (int)(ressources.GetNumberOf(Data.WOOD)) / -20);
                ressources.SetNumberOf(Data.WOOD, 0);
            }
        }*/
    }
}
