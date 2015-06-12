using UnityEngine;
using System.Collections;

public class FoodUpdater : IUpdatingStrategy{



    new void Awake()
    {
        base.Awake();
        this.RessourceName = Ressources.FOOD;
    }

    public override double CalculateIncrement()
    {
        return jobs.GetNumberOf("Farmers")*1.5;
    }

}
