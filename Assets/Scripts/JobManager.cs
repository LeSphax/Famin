using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobManager : MonoBehaviour
{

    ObservableJobs data;
    public Text textObject;
    string jobName;

    void Awake()
    {
        jobName = textObject.text;
    }

    void Start()
    {
        data = ObservableJobs.GetInstance();
    }

    public void DeleteWorker()
    {
        if (data.GetNumberOf(jobName)> 0)
            data.Add(jobName, -1);
    }
    public void AddWorker()
    {

        data.Add(jobName, 1);
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
