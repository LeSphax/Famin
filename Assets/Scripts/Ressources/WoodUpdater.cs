using UnityEngine;
using System.Collections;

public class WoodUpdater : IUpdatingStrategy
{

    new void Awake()
    {
        base.Awake();
        this.RessourceName = Data.WOOD;
    }

    public override double CalculateIncrement()
    {
        return jobs.GetNumberOf("Woodcutters");
    }

}
