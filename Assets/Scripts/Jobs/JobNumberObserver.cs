using UnityEngine;
using UnityEngine.UI;
using System;

public class JobNumberObserver : MonoBehaviour, Observer
{

    public Text jobNameObject;
    public Text jobNumberObject;
    public string jobName;

    void Awake()
    {
        if (jobNameObject != null)
            jobName = jobNameObject.text;
    }

    public virtual void UpdateObserver(object value)
    {
        jobNumberObject.text = "" + value;
    }

    void Start()
    {
        Jobs.GetInstance().AddObserver(this, jobName);
        if (jobNameObject != null)
        jobNameObject.text = jobName;
        Jobs.GetInstance().AddNumberOf(jobName, Int32.Parse(jobNumberObject.text));
    }


}
