using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data
{
    static Dictionary<string, Cost[]> costs;

    static Dictionary<string, Cost[]> Costs
    {
        get
        {
            if (costs == null)
            {
                InitCosts();
            } return costs;
        }
        set { costs = value; }
    }

    public const string HOUSES = "Houses";
    public const string FARMERS = "Farmers";
    public const string MINERS = "Miners";
    public const string WOODCUTTERS = "Woodcutters";
    public const string UNEMPLOYED = "Unemployed";

    static Cost[] COST_HOUSE = { new Cost(Ressources.WOOD, 20), new Cost(Ressources.STONE, 10) };
    static Cost[] COST_PERSON = { new Cost(Ressources.FOOD, 20) };

    static void InitCosts()
    {
        costs = new Dictionary<string, Cost[]>();
        costs.Add(HOUSES, COST_HOUSE);
        costs.Add(FARMERS, COST_PERSON);
        costs.Add(MINERS, COST_PERSON);
        costs.Add(WOODCUTTERS, COST_PERSON);
        costs.Add(UNEMPLOYED, COST_PERSON);
    }

    public static Cost[] GetCost(string name)
    {
        Cost[] cost;
        if (Costs.TryGetValue(name,out cost))
            return cost;
        else
        {
            Debug.LogError("The name isn't in the costs database");
            return null;
        }
    }
}
