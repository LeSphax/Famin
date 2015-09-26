using UnityEngine;
using System.Collections;

public class Recruit : Fighter {

    public Recruit()
    {
        base.strength = 0.5f;
        base.life = 5;
        base.NameJob = Data.RECRUITS;
    }
}
