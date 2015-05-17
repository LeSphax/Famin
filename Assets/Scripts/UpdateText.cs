using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateText : MonoBehaviour, Observer{

    ObservableJobs data;
    public Text jobNameObject;
    public Text jobNumberObject;
    string jobName;

    void Awake()
    {
        jobName = jobNameObject.text;
        jobNameObject.text = jobName + " : ";
    }

    public void update(object value)
    {
        jobNumberObject.text = ""+value;
    }

    void Start()
    {
        ObservableJobs.GetInstance().AddObserver(this,jobName);
    }


}
