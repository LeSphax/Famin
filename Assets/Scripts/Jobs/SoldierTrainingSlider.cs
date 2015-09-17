using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoldierTrainingSlider : MonoBehaviour
{

    bool training;
    float percentage;
    public float trainingDuration = 30;
    Slider slider;
    int numberRecruits;

    Jobs jobs;

    void Awake()
    {
        jobs = Jobs.GetInstance();
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        percentage = 0;
        training = false;
        numberRecruits = 0;
    }
    void Update()
    {
        if (training && !(CombatUpdater.labelEnnemies == CombatUpdater.FIGHTING))
        {
            percentage += Time.deltaTime / trainingDuration;
            slider.value = percentage;
            if (percentage > 1)
            {
                percentage = 0;
                slider.value = percentage;
                training = false;
                jobs.ChangeJob(Data.RECRUITS, Data.SOLDIERS, numberRecruits);
            }
        }

    }

    public void NumberRecruitsHasChanged(int newNumberRecruits)
    {
        if (numberRecruits == 0 && newNumberRecruits != 0)
        {
            training = true;
        }
        else if (newNumberRecruits > numberRecruits)
        {
            Debug.Log(numberRecruits);
            Debug.Log(newNumberRecruits);
            Debug.Log(numberRecruits / newNumberRecruits);
            percentage *= (float) (numberRecruits) / newNumberRecruits;
            Debug.Log(percentage);
            training = true;
        }
        else if (newNumberRecruits == 0)
        {
            training = false;
        }
        else
        {
            training = true;
        }
        numberRecruits = newNumberRecruits;
    }
}
