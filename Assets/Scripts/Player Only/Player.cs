using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    
    public float moveSpeed = 5.0f;
    public float jumpPower = 300.0f;
    [HideInInspector]    
    public Vector2 gunPosition = new Vector2(0.628f,0.0f);
    [HideInInspector]
    public bool facingRight;
    [HideInInspector]
    public bool jumped;
    [HideInInspector]
    public bool overLadder;
    [HideInInspector]
    public bool playerOnLadder;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool inAir;
    [HideInInspector]
    public float centerx;
    
    public float leftOffset = 0;
    public float feetOffset = 0;
    
    //Where is the center of the sprite
    [HideInInspector]
    public Vector2 leftGroundCenter = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 leftHalfGroundCenter = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 middleGroundCenter = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 rightHalfGroundCenter = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 rightGroundCenter = new Vector2(0.0f,0.0f);
    
    //where are we checking for ground
    [HideInInspector]
    public Vector2 leftGroundCheck = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 leftHalfGroundCheck = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 middleGroundCheck = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 rightHalfGroundCheck = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 rightGroundCheck = new Vector2(0.0f,0.0f);
    
    //Check for ground
    [HideInInspector]
    public bool leftGrounded;
    [HideInInspector]
    public bool leftHalfGrounded;
    [HideInInspector]
    public bool middleGrounded;
    [HideInInspector]
    public bool rightHalfGrounded;
    [HideInInspector]
    public bool rightGrounded;

    //Left and Right check
    Vector2 leftWalkCheck = new Vector2(0.0f,0.0f);   
    Vector2 rightWalkCheck = new Vector2(0.0f,0.0f);
    public float leftWalkOffset;
    [HideInInspector]
    public bool leftBlocked;
    [HideInInspector]
    public bool rightBlocked;


    [HideInInspector]
    float shootCount;
    float shootLength = 50.0f;


    public enum ValidDirections { Left, Right, Up, Down}

    Animator anim;
    bool playerMoving;
    bool playerMovingLadder;
    bool playerShooting;

    // Use this for initialization
    void Start () {
        facingRight = true;    
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {

        //Ask for player input
        PlayerControls();
    }

    void FixedUpdate () {
        overLadder = false;
        //DEBUG: Show Grounding Lines
        DebugDrawLines();
        //Check if grounded
        GroundCheck();
        //Check for Walls
        WalkCheck();
        //Ladder velocity bugfix
        ResetLadderLocation();
        //Animation update
        AnimationUpdate();
    }
    
    void PlayerControls() {
        playerMoving=false;
        playerMovingLadder= false;
        //Shooting
        if (Input.GetKeyDown (KeyCode.Z)) {
            GetComponent<Gun>().Shoot (facingRight, gameObject.GetComponent<Rigidbody2D>());
            shootCount = shootLength;
        }
        //Jump
        if (Input.GetKeyDown (KeyCode.X)) {
            if (playerOnLadder) {
                DetachFromLadder();
            }
            if (grounded) {
                Jump();
            }
        }
        
        if (Input.GetKeyUp (KeyCode.X)) {
            StopJump();
        }
        //Movement
        if (Input.GetKey(KeyCode.LeftArrow)){

            if (!playerOnLadder && !leftBlocked) {
                Move(ValidDirections.Left);
                playerMoving = true;
            }
            Vector2 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
            facingRight = false;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            if (!playerOnLadder && !rightBlocked) {
                Move(ValidDirections.Right);
                playerMoving = true;
            }
            Vector2 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
            facingRight = true;
        }
        //if not on ladder, attach to ladder if able
        if ( (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) && overLadder) {
            AttachToLadder();
        }
        
        
        
        //If on ladder Move
        if (Input.GetKey(KeyCode.UpArrow) && playerOnLadder){
            Move(ValidDirections.Up);
            playerMovingLadder = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && playerOnLadder){
            Move(ValidDirections.Down);
            playerMovingLadder = true;
        }
    }
    
    void Move(ValidDirections Direction) {
        switch (Direction) {
            case ValidDirections.Left:
                transform.position += transform.right*-1*moveSpeed;
                break;
            case ValidDirections.Right:
                transform.position += transform.right*moveSpeed;
                break;
            case ValidDirections.Up:
                transform.position += transform.up*moveSpeed;
                break;
            case ValidDirections.Down:
                transform.position += transform.up*-1*moveSpeed;
                break;
        }
        
    }
    
    void Jump() {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,jumpPower);
        jumped = true;
    }
    
    void StopJump() {
        //user not set up to if jumped is true do anything SO YEAH later do that.
        Vector2 CurrentVelocity = rigidbody2D.velocity;
        if (CurrentVelocity.y>0) {
            CurrentVelocity.y *= .2f;
            rigidbody2D.velocity = CurrentVelocity;
            jumped = false;
        }
    }
    
    void OnTriggerStay2D (Collider2D col) {
        //IF LADDER
        centerx = col.transform.position.x+0.5f;
        overLadder = true;
        
    }
    
    void OnTriggerExit2D (Collider2D col) {
        //IF LADDER
        overLadder = false;
        if (playerOnLadder) {
            DetachFromLadder();
        }
    }
    
    
    void AttachToLadder() {
        playerOnLadder = true;
        rigidbody2D.gravityScale=0;
        transform.position = new Vector3(centerx,transform.position.y,0);
        Vector2 CurrentVelocity = rigidbody2D.velocity;
        CurrentVelocity.y *= 0.0f;
        rigidbody2D.velocity = CurrentVelocity;
    }
    
    void DetachFromLadder() {
        playerOnLadder=false;
        rigidbody2D.gravityScale=1;
    }

    void ResetLadderLocation() {
        if (playerOnLadder) {
            transform.position = new Vector3(centerx,transform.position.y,0);
            rigidbody2D.velocity = Vector3.zero;
        }
    }
    
    
    void GroundCheck() {
        //Reset Center Locations
        leftGroundCenter = transform.localPosition;
        leftHalfGroundCenter = transform.localPosition;
        middleGroundCenter = transform.localPosition;
        rightHalfGroundCenter = transform.localPosition;
        rightGroundCenter = transform.localPosition;


        leftGroundCenter.x += leftOffset;
        leftHalfGroundCenter.x += (leftOffset*.5f);
        rightHalfGroundCenter.x -= (leftOffset*.5f);
        rightGroundCenter.x -= leftOffset;
        
        //Reset Ground Locations
        leftGroundCheck = leftGroundCenter;
        leftGroundCheck.y += feetOffset;
        leftHalfGroundCheck = leftHalfGroundCenter;
        leftHalfGroundCheck.y += feetOffset;
        middleGroundCheck = middleGroundCenter;
        middleGroundCheck.y += feetOffset;
        rightHalfGroundCheck = rightHalfGroundCenter;
        rightHalfGroundCheck.y += feetOffset;
        rightGroundCheck = rightGroundCenter;
        rightGroundCheck.y += feetOffset;
        
        leftGrounded = Physics2D.Linecast(leftGroundCenter, leftGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        leftHalfGrounded = Physics2D.Linecast(leftHalfGroundCenter, leftHalfGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        middleGrounded = Physics2D.Linecast(middleGroundCenter, middleGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        rightHalfGrounded = Physics2D.Linecast(rightHalfGroundCenter, rightHalfGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        rightGrounded = Physics2D.Linecast(rightGroundCenter, rightGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        
        if (leftGrounded || leftHalfGrounded || middleGrounded || rightHalfGrounded || rightGrounded) {
            grounded = true;
        } else {
            grounded = false;
        }
    }
    
    void DebugDrawLines() {
        Debug.DrawLine (leftGroundCenter,leftGroundCheck);
        Debug.DrawLine (leftHalfGroundCenter,leftHalfGroundCheck);
        Debug.DrawLine (middleGroundCenter,middleGroundCheck);
        Debug.DrawLine (rightHalfGroundCenter,rightHalfGroundCheck);
        Debug.DrawLine (rightGroundCenter,rightGroundCheck);
        Debug.DrawLine (leftWalkCheck,transform.localPosition);
        Debug.DrawLine (rightWalkCheck, transform.localPosition);
    }

    void AnimationUpdate() {
        if (shootCount > 0) {
            shootCount--;
            playerShooting = true;
        } else {
            playerShooting = false;
        }


        anim.SetBool ("Move",playerMoving);
        anim.SetBool ("MoveLadder",playerMovingLadder);
        anim.SetBool ("Grounded", grounded);
        anim.SetFloat ("PlayerY", rigidbody2D.velocity.y);
        anim.SetBool ("Ladder", playerOnLadder);
        anim.SetBool ("Shoot", playerShooting);
    }

    void WalkCheck () {


        leftWalkCheck = transform.localPosition;
        rightWalkCheck = transform.localPosition;
        leftWalkCheck.x += leftWalkOffset;
        rightWalkCheck.x -= leftWalkOffset;
        leftBlocked = Physics2D.Linecast(leftWalkCheck, transform.localPosition, 1 << LayerMask.NameToLayer("Ground"));  
        rightBlocked = Physics2D.Linecast(rightWalkCheck, transform.localPosition, 1 << LayerMask.NameToLayer("Ground"));  
    }
}
