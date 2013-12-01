using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float jumpPower;
    bool jumped = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void JumpUp() {
		rigidbody2D.AddForce (transform.up*jumpPower);
        jumped = true;
	}

	public void StopJump() {
        if (jumped) {
            Vector2 CurrentVelocity = rigidbody2D.velocity;
            if(CurrentVelocity.y>0){
                CurrentVelocity.y *= .5f;
                rigidbody2D.velocity = CurrentVelocity;
                jumped = false;
            }
        }

	}
}
