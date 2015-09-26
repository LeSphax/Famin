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

    public const string FOOD = "Food";
    public const string WOOD = "Wood";
    public const string STONE = "Stone";
    public const string PLANTED_FOOD = "Plantations";

    public const string HOUSES = "Houses";
    public const string FARMERS = "Farmers";
    public const string GATHERERS = "Gatherers";
    public const string MINERS = "Miners";
    public const string WOODCUTTERS = "Woodcutters";
    public const string IDLE = "Idle";
    public const string PERSON = "Person";
    public const string SOLDIERS = "Soldiers";
    public const string ENEMY_SOLDIERS = "Attackers";
    public const string RECRUITS = "Recruits";
    public const string LOOTERS = "Looters";
    public const string SENT_RAIDERS = "Sent Raiders";

    public const string LOOTERS_TRIP = "Looters Trip";

    static Cost[] FREE_COST = { };
    static Cost[] PERSON_COST = { new Cost(FOOD, 100) };
    //static Cost[] WEAPON_COST = { new Cost(WOOD, 100) };
    static Cost[] LOOTERS_TRIP_COST = { new Cost(FOOD,30)};

    static void InitCosts()
    {
        costs = new Dictionary<string, Cost[]>();
        costs.Add(FARMERS, FREE_COST);
        costs.Add(GATHERERS, FREE_COST);
        costs.Add(MINERS, FREE_COST);
        costs.Add(WOODCUTTERS, FREE_COST);
        costs.Add(IDLE, FREE_COST);
        costs.Add(PERSON, PERSON_COST);
        costs.Add(SOLDIERS, FREE_COST);
        costs.Add(LOOTERS, FREE_COST);
        costs.Add(SENT_RAIDERS, FREE_COST);
        costs.Add(LOOTERS_TRIP, LOOTERS_TRIP_COST);
    }

    public static Cost[] GetCost(string name)
    {
        Cost[] cost;
        if (Costs.TryGetValue(name, out cost))
            return cost;
        else
        {
            Debug.LogError("The name isn't in the costs database");
            return null;
        }
    }
}
