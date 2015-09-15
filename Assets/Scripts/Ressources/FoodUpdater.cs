using UnityEngine;
using System.Collections;
using System;

public class FoodUpdater : IUpdatingStrategy{

    Ressources ressources;
    const int EFFICACITY = 5;

    new void Awake()
    {
        base.Awake();
        ressources = Ressources.GetInstance();
        this.RessourceName = Data.FOOD;
    }

    public override double CalculateIncrement()
    {
        if (Seasons.GetCurrentSeason() == Seasons.SUMMER)
        {
            double recolt = Math.Min(jobs.GetNumberOf(Data.FARMERS) * 2.5, ressources.GetNumberOf(RessourceName));
            ressources.Add(Data.PLANTED_FOOD, -recolt);
            return recolt;
        }
        return 0;
    }

}
