using UnityEngine;
using System.Collections;

public abstract class IUpdatingStrategy : MonoBehaviour
{
    protected Jobs jobs;
    string ressourceName;

    public string RessourceName
    {
        get { return ressourceName; }
        set { ressourceName = value; }
    }

    protected void Awake()
    {
        jobs = Jobs.GetInstance();
    }

    public abstract double CalculateIncrement();

}
