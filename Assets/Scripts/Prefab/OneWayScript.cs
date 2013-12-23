using UnityEngine;
using System.Collections;

public class OneWayScript : MonoBehaviour {
    [HideInInspector]
    private Transform player;
    [HideInInspector]
    private bool PlayerAbove;
    [HideInInspector]
    private bool playerIsOnLadder;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
       PlayerAbove = (player.position.y > transform.localPosition.y);
       playerIsOnLadder = player.GetComponent<Player>().playerOnLadder;
       if (PlayerAbove) {
            //Player Above Oneway
            GetComponent<BoxCollider2D>().enabled = true;
            if (playerIsOnLadder) {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        } else {
            //Player Below Oneway
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}
}
