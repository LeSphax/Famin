using UnityEngine;
using System.Collections.Generic;

public class Army
{

    List<Fighter> troups;
    Jobs jobsData;
    int deadSoldiers;

    public int DeadSoldiers
    {
        get { return deadSoldiers; }
        set { deadSoldiers = value; }
    }

    public Army(Jobs jobsData, int soldiers, int recruits = 0)
    {
        this.jobsData = jobsData;
        deadSoldiers = 0;
        troups = new List<Fighter>();
        for (int i = 0; i < soldiers; i++)
        {
            troups.Add(new Soldier());
        }
        for (int i = 0; i < recruits; i++)
        {
            troups.Add(new Recruit());
        }
        Shuffle();
    }


    void Shuffle()
    {
        int n = troups.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Fighter value = troups[k];
            troups[k] = troups[n];
            troups[n] = value;
        }
    }

    /**
     * The first soldiers of the army take the amountDamages points of damages.
     * The army is defeated when more than half of the soldiers are dead.
     * Return false if the army is defeated, otherwise true.
     * */
    public bool TakeDamage(float amountDamages)
    {
        float amountLeft = amountDamages;
        Debug.Log("Out   " + "Damages" + amountLeft + "deadSoldiers" + deadSoldiers);
        while (deadSoldiers < troups.Count && (amountLeft = troups[deadSoldiers].TakeDamages(amountLeft)) > 0)
        {
            Debug.Log("Damages" + amountLeft + "deadSoldiers" + deadSoldiers);
            jobsData.AddNumberOf(troups[deadSoldiers].NameJob, -1);
            deadSoldiers++;
        }
        return deadSoldiers < troups.Count / 2;
    }

    /**
     * Return the amount of damages the army inflicts.
     **/
    public float InflictDamages()
    {
        float amountDamages = 0;
        for (int i = deadSoldiers; i < troups.Count; i++)
        {
            amountDamages += troups[i].InflictDamages();
        }
        Debug.Log(amountDamages);
        return amountDamages;
    }

    /**
     * Each army inflict his damages on the other, the attackers hit first
     * Return -1 if the defenders lost
     * Return 1 if the attackers lost
     * Return 0 if the battle isn't over yet
     **/
    public static int Fight(Army defenders, Army attackers)
    {
        if (!defenders.TakeDamage(attackers.InflictDamages()))
        {
            return -1;
        }

        if (!attackers.TakeDamage(defenders.InflictDamages()))
        {
            return 1;
        }
        return 0;
    }

}
