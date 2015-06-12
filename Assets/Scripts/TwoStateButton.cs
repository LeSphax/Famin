using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TwoStateButton : MonoBehaviour {

    public Text textButton;

    public string IDLE;
    public string PRESSED;

    private string state;
    private Button buttonScript;

    void Awake()
    {
        state = IDLE;
        textButton.text = state;
        buttonScript = this.GetComponent<Button>();
    }


	void Desactivate() {
            state = PRESSED;
            textButton.text = state;
            buttonScript.interactable = false;
	}

    void Reactivate()
    {
        state = IDLE;
        textButton.text = state;
        buttonScript.interactable = true;
    }
}
