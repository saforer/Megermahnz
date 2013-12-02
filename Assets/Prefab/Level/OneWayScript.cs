using UnityEngine;
using System.Collections;

public class OneWayScript : MonoBehaviour {
    private Transform Player;
    private bool PlayerAbove;
    private bool OnLadder;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

       PlayerAbove = (Player.position.y > GetComponent<Transform>().localPosition.y);
       OnLadder = Player.GetComponent<LadderClimb>().OnLadder;

	   if(PlayerAbove) {
            if (!OnLadder) {
                Debug.Log ("Playerabove, not on ladder");
            GetComponent<BoxCollider2D>().enabled = true;
            } else {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        } else {
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}
}
