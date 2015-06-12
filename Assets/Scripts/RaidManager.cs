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
        jobsData.ChangeJob(Data.RAIDERS, Data.SOLDIERS, 1);
    }

    public void AddWorker()
    {
        jobsData.ChangeJob(Data.SOLDIERS, Data.RAIDERS, 1);
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
        if (jobsData.GetNumberOf(Data.RAIDERS) > 0)
        {
            button.SendMessage("Desactivate");
            pView.RPC("RaidLaunched", PhotonTargets.Others, jobsData.GetNumberOf(Data.RAIDERS));
        }
        else
        {
            logger.PutLine("Vous ne pouvez pas envoyer 0 raiders");
        }
    }

    [RPC]
    public void RaidReturned(byte[] result)
    {
        RaidResult raidResult = RaidResult.Deserialize(result);
        ressources.RefundCost(raidResult.Booty);
        jobsData.SetNumberOf(Data.RAIDERS, raidResult.Survivors);
        button.SendMessage("Reactivate");
        if (raidResult.Survivors > 0)
            logger.PutLine("Our raiders returned ! They " + raidResult.ToString());
        else
            logger.PutLine("All of our raiders died in the battle");
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

        public override string ToString()
        {
            String log = "";
            
            if (Booty.Length > 0)
            {
                log += "stole " + Cost.toString(Booty);
            }
            if (Killed > 0)
            {
                log += "killed " + Killed + " people";
            }
            if (Burned > 0)
            {
                log += "burned " + Burned + " houses";
            }
            log += " and " + Survivors + " of them survived";
            return log;
        }

    }
}
