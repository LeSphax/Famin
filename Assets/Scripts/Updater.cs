using UnityEngine;
using System.Collections;

public abstract class Updater : MonoBehaviour {

	protected void Awake () {
        Time.fixedDeltaTime = 1;
	}
}
