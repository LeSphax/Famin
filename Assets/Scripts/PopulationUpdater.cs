using UnityEngine;
using System.Collections;

public class PopulationUpdater : MonoBehaviour {

    Ressources ressources;
    Jobs jobs;
    Logger logger;

    void Awake()
    {
        logger = Logger.GetInstance();
        jobs = Jobs.GetInstance();
        ressources = Ressources.GetInstance();
        Time.fixedDeltaTime = 1;
    }

    void FixedUpdate()
    {
        ressources.Add(Ressources.FOOD, -jobs.GetTotalPopulation());
        if (ressources.GetNumberOf(Ressources.FOOD) < 0)
        {
            logger.PutLine("A worker starved to death!");
            jobs.KillPeople(1+(int)(ressources.GetNumberOf(Ressources.FOOD))/-20);
            ressources.SetNumberOf(Ressources.FOOD, 0);
        }
    }
}
