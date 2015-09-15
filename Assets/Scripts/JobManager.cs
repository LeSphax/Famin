using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobManager : MonoBehaviour
{
    Jobs jobsData;
    Ressources ressourcesData;
    public Text textObject;
    public string jobName;

    void Awake()
    {
        if (textObject != null)
            jobName = textObject.text;
    }

    void Start()
    {
        jobsData = Jobs.GetInstance();
        ressourcesData = Ressources.GetInstance();
    }

    public void DeleteWorker()
    {
        int number = 1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            number = 10;
        for (int i = 0; i < number; i++)
            jobsData.ChangeJob(jobName, Data.IDLE, 1);
    }

    public void AddWorker()
    {
        int number = 1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            number = 10;
        for (int i = 0; i < number; i++)
        {
            if (jobName == Data.IDLE)
            {
                if (ressourcesData.PayCosts(Data.GetCost(Data.PERSON)))
                    jobsData.Add(jobName, 1);
            }
            else if (jobsData.GetNumberOf(Data.IDLE) > 0)
            {
                jobsData.ChangeJob(Data.IDLE, jobName, 1);
            }
        }
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
