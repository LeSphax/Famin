using UnityEngine;
using System.Collections;

public class PlantedFoodUpdater : IUpdatingStrategy{



    new void Awake()
    {
        base.Awake();
        this.RessourceName = Data.PLANTED_FOOD;
    }

    public override double CalculateIncrement()
    {
        if (Seasons.GetCurrentSeason() == Seasons.FALL  || Seasons.GetCurrentSeason() == Seasons.SPRING)
        {
            return jobs.GetNumberOf("Farmers") * 3;
        }
        return 0;
    }

}
