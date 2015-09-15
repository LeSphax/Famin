using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RessourceChangeManager : MonoBehaviour {

    private Text UIText;

	// Use this for initialization
	void Start () {
        UIText = GetComponent<Text>();
	}

    public void SetRessourceChange(double value)
    {
        UIText.text = Math.Round(value,2) + "/s";
        if (value < 0)
        {
            UIText.color = Color.red;
        }
        else if (value == 0)
        {
            UIText.color = Color.black;
        }
        else
        {
            UIText.color = Color.green;
        }
    }
}
