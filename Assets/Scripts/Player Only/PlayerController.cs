using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour {
	public string axisName = "Horizontal";
    public Transform groundCheck1;
    public Transform groundCheck2;
    public Transform groundCheck3;
    bool FacingRight = true;
    bool grounded1;
    bool grounded2;
    bool grounded3;
    bool grounded;
    bool jumped;
	public Animator anim;
	public int ShootingTimer;	
	public int ShootingCount = 120;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		AnimationUpdate ();
        //FIND OUT IF THE PLAYER IS GROUNDED
        grounded1 = Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"));  
        grounded2 = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));  
        grounded3 = Physics2D.Linecast(transform.position, groundCheck3.position, 1 << LayerMask.NameToLayer("Ground")); 
        if (grounded1||grounded2||grounded3) {
            grounded = true;
            anim.SetBool ("InAir",false);
        } else {
            anim.SetBool ("InAir",true);
            grounded = false;
        }
        SendMessage("Move",Input.GetAxisRaw(axisName));
		if(Input.GetKeyDown(KeyCode.Z) && grounded) {
			SendMessage ("JumpUp");
            jumped = true;
		}
		if(Input.GetKeyUp (KeyCode.Z) && jumped) {
            jumped = false;
			SendMessage ("StopJump");
		}
		if(Input.GetKey (KeyCode.X)) {
			ShootingTimer=ShootingCount;
			anim.SetBool ("Shooting",true);
			SendMessage ("Shoot",FacingRight);
		}
        if(Input.GetKey(KeyCode.UpArrow)){
            SendMessage ("Climb",1);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            SendMessage ("Climb",-1);
        }
	}
	private void AnimationUpdate() {
        ShootingTimer--;
		anim.SetFloat("Speed",Mathf.Abs(Input.GetAxisRaw (axisName)));	
		if(ShootingTimer<=0) {
			anim.SetBool ("Shooting",false);
		}
	}
    private void DyingEffect () {
        Debug.Log ("Oops");
    }
}
