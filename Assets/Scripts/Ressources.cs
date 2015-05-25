using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ressources
{

    static Ressources instance;
    ObservableDictionary<string, double> quantities;

    public static string FOOD = "Food";
    public static string WOOD = "Wood";
    public static string STONE = "Stone";


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

}
