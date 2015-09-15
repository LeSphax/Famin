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
        quantities[ressourceName] = value;
    }

    public void Add(string ressourceName, double value)
    {
        quantities[ressourceName] += value;
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

    public void RecoltPlants(double quantitie)
    {
        if (quantities[Data.PLANTED_FOOD] > 0)
        {
            double recoltedFood = Math.Min(quantitie, quantities[Data.PLANTED_FOOD]);
            quantities[Data.PLANTED_FOOD] -= recoltedFood;
            quantities[Data.FOOD] += recoltedFood;
        }
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
        List<string> keys = new List<string>(quantities.GetKeys());
        List<Cost> list = new List<Cost>();
        int i = 0;
        foreach (string key in keys)
        {
            if (attackers == 0)
            {
                return list.ToArray();
            }
            int number = Math.Min(Convert.ToInt32(quantities[key]), attackers * 10);
            list.Add(new Cost(key, number));
            quantities[key] -= number;
            attackers -= number / 10;
            i++;
        }
        return list.ToArray();
    }

}
