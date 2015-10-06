using UnityEngine;
using System.Collections;

public class PlantedFoodUpdater : IUpdatingStrategy{

    const double EFFICIENCY = 6;

    new void Awake()
    {
        base.Awake();
        this.RessourceName = Data.PLANTED_FOOD;
    }

    public override double CalculateIncrement()
    {
        if (Seasons.GetCurrentSeason() == Seasons.FALL )
        {
            return jobs.GetNumberOf("Farmers") * EFFICIENCY;
        }
        return 0;
    }

}
