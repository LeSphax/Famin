using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

}

