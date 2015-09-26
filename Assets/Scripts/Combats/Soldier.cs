using UnityEngine;
using System.Collections;

public class Soldier : Fighter {

    public Soldier()
    {
        base.strength = 1;
        base.life = 10;
        base.NameJob = Data.SOLDIERS;
    }
}
