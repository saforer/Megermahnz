using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {
    private int frameCount = 0;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        frameCount++;
	}

    void OnTriggerStay2D (Collider2D col) {
            if (frameCount % 20 == 0) {
                col.gameObject.SendMessage("KillBox");
            }
        }
    }
