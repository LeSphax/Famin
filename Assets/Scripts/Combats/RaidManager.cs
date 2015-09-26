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

    [RPC]
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

        public RaidResult()
        {
            survivors = 0;
            killed = 0;
            burned = 0;
            Booty = null;
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

        int burned;

        public int Burned
        {
            get { return burned; }
            set { burned = value; }
        }

        Cost[] booty;

        public Cost[] Booty
        {
            get { return booty; }
            set { booty = value; }
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
            if (Survivors == 0)
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
                    log += " killed " + Killed + " of our people";
                }
                if (Burned > 0)
                {
                    log += " burned " + Burned + " houses";
                }
                log += ".";
                return log;
            }
        }

        public string OurRaidResult()
        {
            String log = "";
            if (Survivors == 0)
            {
                return "Our " + Data.LOOTERS + " didn't come back";
            }
            else
            {
                log += Survivors + " of our " + Data.LOOTERS + " have returned ! They ";
                if (Booty.Length > 0)
                {
                    log += "stole " + Cost.toString(Booty);
                }
                if (Killed > 0)
                {
                    log += " killed " + Killed + " people";
                }
                if (Burned > 0)
                {
                    log += " burned " + Burned + " houses";
                }
                log += ".";
                return log;
            }
        }

    }
}
