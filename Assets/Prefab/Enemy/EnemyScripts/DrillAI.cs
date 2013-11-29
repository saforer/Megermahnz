using UnityEngine;
using System.Collections;

public class DrillAI : MonoBehaviour {
    int Direction = -1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        SendMessage("Move",Direction);
	}

    void DyingEffect () {

    }
}
