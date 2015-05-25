using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobNumberObserver : MonoBehaviour, Observer{

    public Text jobNameObject;
    public Text jobNumberObject;
    string jobName;

    void Awake()
    {
        jobName = jobNameObject.text;
    }

    public void UpdateObserver(object value)
    {
        jobNumberObject.text = ""+value;
    }

    void Start()
    {
        Jobs.GetInstance().AddObserver(this,jobName);
        jobNameObject.text = jobName + " : ";
    }


}
