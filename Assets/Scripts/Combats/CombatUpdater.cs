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
        enemiesData = Jobs.GetEnemies();
        logger = Logger.GetInstance();
        pView = GameObject.FindWithTag("Raiders").GetComponent<PhotonView>();
    }

    void Start()
    {
        timer = TIMER_START;
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
                timer = TIMER_START;
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
            RaidManager.RaidResult raidResult = new RaidManager.RaidResult(false, defendersArmy.DeadSoldiers);
            FightEnded(raidResult);
        }
        else if (resultFight == -1)
        {
            RaidManager.RaidResult raidResult = new RaidManager.RaidResult(true, defendersArmy.DeadSoldiers);
            FightEnded(raidResult);
        }
    }

    void FightEnded(RaidManager.RaidResult result)
    {
        enemiesData.SetNumberOf(Data.SOLDIERS, 0);
        logger.PutLine(result.EnemysRaidResult());
        pView.RPC("RaidReturned", PhotonTargets.Others, result.Serialize());
        labelEnnemies = IDLE;
        display.SetActive(false);
        CancelInvoke("Fight");
    }

    [PunRPC]
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
        textEnnemies.text = "" + Convert.ToInt32(value);
    }

    public void UpdateTimer()
    {
        timer -= 1;
        textTimer.text = "" + timer;
    }


}

