using UnityEngine;
using System.Collections;

public class LadderClimb : MonoBehaviour {
    public int speed;
    public bool OverLadder;
    public bool OnLadder;
    public float centerx;

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter2D (Collider2D col) {
        centerx = collider.GetComponent<BoxCollider2D>().center.x;
        Debug.Log ("Collision");
        OverLadder = true;
    }

    void OnTriggerExit2D (Collider2D col) {
        OverLadder = false;
    }

	// Update is called once per frame
	void Update () {
	
	}

    void FallOffLadder () {
        OnLadder=false;
        rigidbody2D.gravityScale=1;
    }

    void Climb (int Direction) {
        if (OverLadder) {
            OnLadder=true;
            transform.position = new Vector3(centerx,transform.position.y,0);
            rigidbody2D.gravityScale=0;
            transform.position += transform.up*speed*Time.deltaTime*Direction;
        }
    }
}
