using UnityEngine;
using System.Collections;

public class StoneUpdater : IUpdatingStrategy
{

    new void Awake()
    {
        base.Awake();
        this.RessourceName = Data.STONE;
    }

    public override double CalculateIncrement()
    {
        return jobs.GetNumberOf("Miners")/4;
    }

}
