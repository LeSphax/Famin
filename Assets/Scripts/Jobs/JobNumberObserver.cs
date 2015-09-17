using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        jobNameObject.text = jobName + " : ";
    }


}
