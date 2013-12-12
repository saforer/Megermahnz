using UnityEngine;
using System.Collections;

public class OneWayScript : MonoBehaviour {
    private Transform Player;
    private bool PlayerAbove;
    private bool OnLadderOneWay;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log (Player);
    }
	
	// Update is called once per frame
	void Update () {

       PlayerAbove = (Player.position.y > GetComponent<Transform>().localPosition.y);
       OnLadderOneWay = Player.GetComponent<LadderClimb>().OnLadder;

	   if(PlayerAbove && !OnLadderOneWay) {
            GetComponent<BoxCollider2D>().enabled = true;
        } else {
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}
}
