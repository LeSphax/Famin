using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CombatUpdater : MonoBehaviour, Observer
{
    public static string IDLE = "Bugged";
    public static string FIGHTING = "Enemy Soldiers";
    public static string COMING = "Enemies incoming";
    public static int TIMER_START = 10;

    Jobs jobs;
    Jobs enemiesData;
    Ressources ressources;
    Buildings buildings;
    Logger logger;

    public Text textEnnemies;
    public Text textTimer;
    public GameObject display;

    int timer;
    public static string labelEnnemies;
    PhotonView pView;

    Army attackersArmy;
    Army defendersArmy;

    void Awake()
    {
        jobs = Jobs.GetInstance();
        ressources = Ressources.GetInstance();
        enemiesData = Jobs.GetEnemies();
        buildings = Buildings.GetInstance();
        logger = Logger.GetInstance();
        pView = GameObject.FindWithTag("Raiders").GetComponent<PhotonView>();
    }

    void Start()
    {
        timer = 0;
        labelEnnemies = IDLE;
        enemiesData.AddObserver(this, Data.SOLDIERS);
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
                defendersArmy = new Army(jobs, jobs.GetNumberOf(Data.SOLDIERS), jobs.GetNumberOf(Data.RECRUITS));
                InvokeRepeating("Fight", 0, 1);
            }
        }
    }

    void Fight()
    {
        Debug.Log("Fight" + Time.time);
        int resultFight = Army.Fight(defendersArmy, attackersArmy);
        if (resultFight == 1)
        {
            RaidManager.RaidResult raidResult = new RaidManager.RaidResult();
            FightEnded(raidResult);
        }
        else if (resultFight == -1)
        {
            RaidManager.RaidResult raidResult = new RaidManager.RaidResult();
            int attackersLeft = enemiesData.GetNumberOf(Data.SOLDIERS);
            raidResult.Survivors = attackersLeft;
            raidResult.Booty = Loot(ref attackersLeft);
            raidResult.Killed = defendersArmy.DeadSoldiers + Slaugther(ref attackersLeft);
            enemiesData.SetNumberOf(Data.SOLDIERS, 0);
            FightEnded(raidResult);
        }
    }

    void FightEnded(RaidManager.RaidResult result)
    {
        logger.PutLine(result.EnemysRaidResult());
        pView.RPC("RaidReturned", PhotonTargets.Others, result.Serialize());
        labelEnnemies = IDLE;
        display.SetActive(false);
        CancelInvoke("Fight");
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
    public void RaidLaunched(int numberAttackers)
    {
        display.SetActive(true);
        labelEnnemies = COMING;
        timer = TIMER_START;
        enemiesData.AddNumberOf(Data.SOLDIERS, numberAttackers);
        //
        attackersArmy = new Army(enemiesData, enemiesData.GetNumberOf(Data.SOLDIERS));
        //
        logger.PutLine(numberAttackers + " ennemies are coming to loot us !");
    }

    public void UpdateObserver(object value)
    {
        textEnnemies.text = ""+Convert.ToInt32(value);
    }

    public void UpdateTimer()
    {
        timer -= 1;
        textTimer.text = "" + timer;
    }


}

