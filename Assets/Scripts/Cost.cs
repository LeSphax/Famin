using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Cost
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private int number;

    public int Number
    {
        get { return number; }
        set { number = value; }
    }

    public Cost(string name, int number)
    {
        this.name = name;
        this.number = number;
    }

    public static String toString(Cost[] costs)
    {
        String result = "";
        for (int i = 0; i < costs.Length-1; i++)
        {
            result += costs[i].ToString() +", ";
        }
        result += costs[costs.Length-1].ToString();
        return result;
    }

    public override string ToString()
    {
        return number + " " + name;
    }

}

