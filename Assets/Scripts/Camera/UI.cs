using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {
    float countUp = 0.0f;
    public bool stop;
	// Use this for initialization
	void Start () {
        UpdateGUICount(0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!stop) {
        countUp += Time.deltaTime;
        }

        UpdateGUICount(countUp);
	}

    void UpdateGUICount(float count) {
        guiText.text = "Time: " +  count;
    }

    public void StopCount() {
        stop = true;
    }
}
