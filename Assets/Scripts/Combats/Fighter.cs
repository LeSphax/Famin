using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter
{

    static Dictionary<string, Fighter> fighters;

    static Dictionary<string, Fighter> Fighters
    {
        get
        {
            if (fighters == null)
            {
                InitFighters();
            } return fighters;
        }
        set { fighters = value; }
    }

    protected float life;
    protected float strength;
    private string nameJob;

    public string NameJob
    {
        get { return nameJob; }
        set { nameJob = value; }
    }


    static void InitFighters()
    {
        fighters = new Dictionary<string, Fighter>();
        fighters.Add(Data.SOLDIERS, new Soldier());
        fighters.Add(Data.RECRUITS, new Recruit());
    }

    public float TakeDamages(float amountDamages)
    {
        Debug.Log("vie" + life);
        float damagesOverflow = amountDamages - life;
        life -= amountDamages;
        return damagesOverflow;
    }

    public float InflictDamages()
    {
        return strength;
    }

}
