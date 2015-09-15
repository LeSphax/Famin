using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobManager : MonoBehaviour
{
    Jobs jobsData;
    Ressources ressourcesData;
    Buildings buildingsData;
    Logger logger;
    public Text textObject;
    string jobName;

    void Awake()
    {
        jobName = textObject.text;
        logger = Logger.GetInstance();
    }

    void Start()
    {
        jobsData = Jobs.GetInstance();
        ressourcesData = Ressources.GetInstance();
        buildingsData = Buildings.GetInstance();
    }

    public void DeleteWorker()
    {
        jobsData.ChangeJob(jobName, Data.IDLE, 1);
    }

    public void AddWorker()
    {
        if (jobsData.GetNumberOf(Data.IDLE) > 0)
        {
            jobsData.ChangeJob(Data.IDLE, jobName, 1);
        }
        //else if (buildingsData.GetPopLimit() > jobsData.GetTotalPopulation())
        else if (ressourcesData.PayCosts(Data.GetCost(jobName)))
        {
            if (ressourcesData.PayCosts(Data.GetCost(Data.PERSON)))
            {
                jobsData.Add(jobName, 1);
            }
            else
            {
                ressourcesData.RefundCost(Data.GetCost(jobName));
            }

        }
        /*else
        {
            logger.PutLine("Build more houses");
        }*/
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
}
