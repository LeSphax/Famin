using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CombatUpdater : MonoBehaviour, Observer
{
    public static string IDLE = "Bugged";
    public static string FIGHTING = "Attackers";
    public static string COMING = "Attackers on their way";
    public static int TIMER_START = 10;

    Jobs jobs;
    Ennemies ennemies;
    Ressources ressources;
    Buildings buildings;
    Logger logger;

    public Text textEnnemies;
    public Text textTimer;
    public GameObject display;

    int timer;
    string labelEnnemies;
    PhotonView pView;

    void Awake()
    {
        jobs = Jobs.GetInstance();
        ennemies = Ennemies.GetInstance();
        ressources = Ressources.GetInstance();
        buildings = Buildings.GetInstance();
        logger = Logger.GetInstance();
        Time.fixedDeltaTime = 1;
        pView = GameObject.FindWithTag("Raiders").GetComponent<PhotonView>();
    }

    void Start()
    {
        timer = 0;
        labelEnnemies = IDLE;
        ennemies.AddObserver(this, Data.SOLDIERS);
    }

    void FixedUpdate()
    {
        if (labelEnnemies == IDLE) { }
        else if (labelEnnemies == COMING)
        {
            UpdateTimer();
            if (timer == 0)
            {
                labelEnnemies = FIGHTING;
            }
        }
        else if (labelEnnemies == FIGHTING)
        {
            int attackers = ennemies.GetNumberOf(Data.SOLDIERS);
            if (attackers > 0)
            {
                int defenders = jobs.GetNumberOf(Data.SOLDIERS);
                if (defenders > 0)
                {
                    ennemies.Add(Data.SOLDIERS, -Math.Min(attackers, Math.Max(1, Convert.ToInt32(defenders * 0.1))));
                    jobs.Add(Data.SOLDIERS, -Math.Min(defenders, Math.Max(1, Convert.ToInt32(attackers * 0.1))));
                    if (ennemies.GetNumberOf(Data.SOLDIERS) == 0)
                    {
                        RaidManager.RaidResult result = new RaidManager.RaidResult();
                        FightEnded(result);
                    }
                }
                else
                {
                    RaidManager.RaidResult result = new RaidManager.RaidResult();
                    result.Survivors = attackers;
                    result.Booty = Loot(ref attackers);
                    result.Killed = Slaugther(ref attackers);
                    result.Burned = Burn(attackers);
                    ennemies.SetNumberOf(Data.SOLDIERS, 0);
                    FightEnded(result);
                }
            }
        }
    }

    void FightEnded(RaidManager.RaidResult result)
    {
        if (result.Survivors== 0){
            logger.PutLine("We successfully defeated the raiders !");
        }
        else {
            logger.PutLine("The raiders defeated our army !");
            logger.PutLine("They " + result.ToString());
        }
        pView.RPC("RaidReturned", PhotonTargets.Others, result.Serialize());
        labelEnnemies = IDLE;
        display.SetActive(false);
    }

    Cost[] Loot(ref int attackers)
    {
        Cost[] costs = ressources.StealRessources(ref attackers);
        return costs;
    }

    int Slaugther(ref int attackers)
    {
        int killed = jobs.KillPeople(attackers);
        attackers -= killed;
        return killed;
    }

    int Burn(int attackers)
    {
        return buildings.Burn(attackers);
    }

    [RPC]
    public void RaidLaunched(int attackers)
    {
        display.SetActive(true);
        labelEnnemies = COMING;
        timer = TIMER_START;
        ennemies.Add(Data.SOLDIERS, attackers);
        logger.PutLine(attackers + " ennemies are coming to raid us !");
    }

    public void UpdateObserver(object value)
    {
        textEnnemies.text = labelEnnemies + " :" + Convert.ToInt32(value);
    }

    public void UpdateTimer()
    {
        timer -= 1;
        textTimer.text = "" + timer;
    }


}

