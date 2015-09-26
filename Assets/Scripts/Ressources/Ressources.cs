using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Ressources
{

    static Ressources instance;
    ObservableDictionary<string, double> quantities;


    public static Ressources GetInstance()
    {
        if (instance == null)
        {
            instance = new Ressources();
        }
        return instance;
    }

    private Ressources()
    {
        quantities = new ObservableDictionary<string, double>();
    }

    public void AddObserver(Observer o, string ressourceName)
    {
        quantities.AddObserver(o, ressourceName);
        double x;
        if (!quantities.TryGetValue(ressourceName, out x))
        {
            quantities.Add(ressourceName, 0);
        }
    }

    public double GetNumberOf(string ressourceName)
    {
        return quantities[ressourceName];
    }

    public void SetNumberOf(string ressourceName, double value)
    {
        if (value < 0)
        {
            Debug.Log("Can't set a ressource's value to a negative number");
        }
        quantities[ressourceName] = value;
    }

    public void Add(string ressourceName, double value)
    {
        quantities[ressourceName] += value;
        if (quantities[ressourceName] < 0)
            quantities[ressourceName] = 0;
    }

    public bool PayCosts(Cost[] costs)
    {
        foreach (Cost cost in costs)
        {
            if (quantities[cost.Name] < cost.Number)
            {
                return false;
            }
        }
        foreach (Cost cost in costs)
        {
            quantities[cost.Name] -= cost.Number;
        }
        return true;
    }

    public bool PayCosts(Cost[] costs, int times)
    {
        foreach (Cost cost in costs)
        {
            if (quantities[cost.Name] < cost.Number * times)
            {
                return false;
            }
        }
        foreach (Cost cost in costs)
        {
            quantities[cost.Name] -= cost.Number * times;
        }
        return true;
    }

    public void RefundCost(Cost[] costs)
    {
        foreach (Cost cost in costs)
        {
            quantities[cost.Name] += cost.Number;
        }
    }

    public Cost[] StealRessources(ref int attackers)
    {
        int ressourcesToSteal = 100 * attackers;
        List<Cost> list = new List<Cost>();
        int ressourcesStolen = Math.Min(Convert.ToInt32(quantities[Data.FOOD]), ressourcesToSteal);
        attackers -= ressourcesStolen / 100;
        list.Add(new Cost(Data.FOOD, ressourcesStolen));
        quantities[Data.FOOD] -= ressourcesStolen;
        return list.ToArray();

        /* List<string> keys = new List<string>(quantities.GetKeys());
         List<Cost> list = new List<Cost>();
         foreach (string key in keys)
         {
             if (attackers == 0)
             {
                 return list.ToArray();
             }
             int number = Math.Min(Convert.ToInt32(quantities[key]), attackers * 100);
             list.Add(new Cost(key, number));
             quantities[key] -= number;
             attackers -= number / 100;
         }
         return list.ToArray();*/
    }

}
