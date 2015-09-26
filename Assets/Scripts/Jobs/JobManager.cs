using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobManager : MonoBehaviour
{
    Jobs jobsData;
    public Text textObject;
    public string jobName;
    public string jobRequired = Data.IDLE;

    void Awake()
    {
        if (textObject != null)
            jobName = textObject.text;
    }

    void Start()
    {
        jobsData = Jobs.GetInstance();
    }

    public void DeleteWorker()
    {
        int number = 1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            number = jobsData.GetNumberOf(jobName);
        jobsData.ChangeJob(jobName, jobRequired, number);
    }

    public void AddWorker()
    {
        bool canPay = false;
        if (jobsData.GetNumberOf(jobRequired) > 0)
        {
            jobsData.ChangeJob(jobRequired, jobName, 1);
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            canPay = true;
        }
        while (canPay)
        {

            if (jobsData.GetNumberOf(jobRequired) > 0)
            {
                jobsData.ChangeJob(jobRequired, jobName, 1);
            }
            else
            {
                canPay = false;
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
