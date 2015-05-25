using UnityEngine;
using System.Collections;

public class WoodUpdater : IUpdatingStrategy
{

    new void Awake()
    {
        base.Awake();
        this.RessourceName = Ressources.WOOD;
    }

    public override double CalculateIncrement()
    {
        return jobs.GetNumberOf("Woodcutters")*0.5;
    }

}
