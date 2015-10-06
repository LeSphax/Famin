using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class RaidManager : MonoBehaviour
{
    Jobs jobsData;
    Ressources ressources;
    Logger logger;
    PhotonView pView;

    public GameObject button;

    void Awake()
    {
        logger = Logger.GetInstance();
        jobsData = Jobs.GetInstance();
        ressources = Ressources.GetInstance();
        pView = GameObject.FindWithTag("Attackers").GetComponent<PhotonView>();
    }

    public void DeleteWorker()
    {
        jobsData.ChangeJob(Data.LOOTERS, Data.SOLDIERS, 1);
    }

    public void AddWorker()
    {
        jobsData.ChangeJob(Data.SOLDIERS, Data.LOOTERS, 1);
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddWorker();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DeleteWorker();
        }
    }

    public void Send()
    {
        int nbLooters = jobsData.GetNumberOf(Data.LOOTERS);
        if (nbLooters > 0)
        {
            if (ressources.PayCosts(Data.GetCost(Data.LOOTERS_TRIP)))
            {
                button.SendMessage("Desactivate");
                pView.RPC("RaidLaunched", PhotonTargets.Others, jobsData.GetNumberOf(Data.LOOTERS));
                jobsData.SetNumberOf(Data.LOOTERS, 0);
            }
        }
        else
        {
            logger.PutLine("Vous ne pouvez pas envoyer 0" + Data.LOOTERS);
        }
    }

    [PunRPC]
    public void RaidReturned(byte[] result)
    {
        RaidResult raidResult = RaidResult.Deserialize(result);
        ressources.RefundCost(raidResult.Booty);
        jobsData.SetNumberOf(Data.LOOTERS, raidResult.Survivors);
        button.SendMessage("Reactivate");
        logger.PutLine(raidResult.OurRaidResult());
    }


    [Serializable]
    public class RaidResult
    {

        bool victorious;

        public bool Victorious
        {
            get { return victorious; }
            set { victorious = value; }
        }

        int survivors;

        public int Survivors
        {
            get { return survivors; }
            set { survivors = value; }
        }

        int killed;

        public int Killed
        {
            get { return killed; }
            set { killed = value; }
        }

        Cost[] booty;

        public Cost[] Booty
        {
            get { return booty; }
            set { booty = value; }
        }



        public RaidResult(bool victorious, int killedDuringBattle)
        {
            Jobs enemiesData = Jobs.GetEnemies();
            int attackersLeft = enemiesData.GetNumberOf(Data.SOLDIERS);
            Survivors = attackersLeft;
            Victorious = victorious;
            if (victorious)
            {
                Booty = Loot(ref attackersLeft);
                Killed = killedDuringBattle + Slaugther(ref attackersLeft);
            }
        }

        Cost[] Loot(ref int attackers)
        {
            Cost[] costs = Ressources.GetInstance().StealRessources(ref attackers);
            return costs;
        }

        int Slaugther(ref int attackers)
        {
            int killed = Jobs.GetInstance().KillPeople(attackers);
            attackers -= killed;
            return killed;
        }

        public byte[] Serialize()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, this);
            return stream.ToArray();
        }

        public static RaidResult Deserialize(byte[] bytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            object result = bf.Deserialize(stream);
            if (result.GetType() == typeof(RaidResult))
            {
                return (RaidResult)result;
            }
            else
            {
                Debug.LogError("The deserialized object is not of type :" + typeof(RaidResult));
                return null;
            }
        }

        public string EnemysRaidResult()
        {
            String log = "";
            if (!victorious)
            {
                return "We defeated the enemy " + Data.LOOTERS + " ! " + Killed + " of our people have died";
            }
            else
            {
                log += "The enemys " + Data.LOOTERS + " defeated our army ! They ";
                if (Booty.Length > 0)
                {
                    log += "stole " + Cost.toString(Booty);
                }
                if (Killed > 0)
                {
                    log += "and killed " + Killed + " of our people";
                }
                log += ".";
                return log;
            }
        }

        public string OurRaidResult()
        {
            String log = "";
            if (!victorious)
            {
                return "Our " + Data.LOOTERS + " lost this battle, they managed to kill " + Killed + " enemies but only " + Survivors + " of them survived";
            }
            else
            {
                log += Survivors + " of our " + Data.LOOTERS + " were victorious ! They ";
                if (Booty.Length > 0)
                {
                    log += "stole " + Cost.toString(Booty);
                }
                if (Killed > 0)
                {
                    log += "and killed " + Killed + " people";
                }
                log += ".";
                return log;
            }
        }

    }
}
