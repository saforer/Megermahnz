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

	   if(PlayerAbove && !OnLadder) {
            GetComponent<BoxCollider2D>().enabled = true;
        } else {
            Debug.Log (PlayerAbove);
            Debug.Log (!OnLadder);
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}
}
