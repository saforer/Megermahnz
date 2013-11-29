using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float speed;
	public Animator anim;
    public bool facingRight;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}



	void Move(int Direction) {
        if (Direction > 0) {
            //Moving to the right
            if (!facingRight) {
                Flip ();
            }
        } else if (Direction < 0) {
            //Moving to the left
            if (facingRight) {
                Flip ();
            }
        }

        transform.position += transform.right*speed*Time.deltaTime*Direction;
	}


    public void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
    }
}
