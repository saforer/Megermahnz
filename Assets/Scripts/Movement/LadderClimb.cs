using UnityEngine;
using System.Collections;

public class LadderClimb : MonoBehaviour {
    public int speed;
    public bool OverLadder;
    public bool OnLadder;
    public float centerx;
    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
    void OnTriggerEnter2D (Collider2D col) {
        centerx = col.transform.position.x+0.5f;
        OverLadder = true;
    }

    void OnTriggerExit2D (Collider2D col) {
        OverLadder = false;
    }

	// Update is called once per frame
	void Update () {
        if(!OverLadder) {
            OnLadder=false;
        }
	    if(!OnLadder) {
            rigidbody2D.gravityScale=1;
        }
	}

    void FallOffLadder () {
        OnLadder=false;
        rigidbody2D.gravityScale=1;
    }

    void Climb (int Direction) {
        if (OverLadder) {
            OnLadder=true;
            anim.SetBool ("OnLadderAnim",true);
            transform.position = new Vector3(centerx,transform.position.y,0);
            rigidbody2D.velocity=new Vector3(0f,0f,0f);
            rigidbody2D.gravityScale=0;
            transform.position += transform.up*speed*Time.deltaTime*Direction;
        }
    }
}
