using UnityEngine;
using System.Collections;

public class RecruitsNumberObserver : JobNumberObserver
{

    public GameObject trainingSlider;

    public override void UpdateObserver(object o)
    {
        base.UpdateObserver(o);
        int numberRecruits = (int)o;
        trainingSlider.SendMessage("NumberRecruitsHasChanged", numberRecruits);
    }
}
