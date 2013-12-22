using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    
    //DEBUG: Show Lines
    public bool showLines = false;
    
    public float moveSpeed = 5.0f;
    public float jumpPower = 300.0f;
    public Vector2 gunPosition = new Vector2(0.628f,0.0f);
    public GameObject bullet;
    [HideInInspector]
    public bool facingRight;
    [HideInInspector]
    public bool jumped;
    [HideInInspector]
    public bool overLadder;
    [HideInInspector]
    public bool playerOnLadder;
    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool inAir;
    [HideInInspector]
    public float centerx;
    
    
    public float leftOffset = 0;
    [HideInInspector]
    public float middleOffset = 0;
    public float rightOffset = 0;
    public float feetOffset = 0;
    
    //Where is the center of the sprite
    public Vector2 leftGroundCenter = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 middleGroundCenter = new Vector2(0.0f,0.0f);
    public Vector2 rightGroundCenter = new Vector2(0.0f,0.0f);
    
    //where are we checking for ground
    public Vector2 leftGroundCheck = new Vector2(0.0f,0.0f);
    [HideInInspector]
    public Vector2 middleGroundCheck = new Vector2(0.0f,0.0f);
    public Vector2 rightGroundCheck = new Vector2(0.0f,0.0f);
    
    //Check for ground
    [HideInInspector]
    public bool leftGrounded;
    [HideInInspector]
    public bool middleGrounded;
    [HideInInspector]
    public bool rightGrounded;

    public enum ValidDirections { Left, Right, Up, Down}
    
    // Use this for initialization
    void Start () {
        facingRight = true;    
    }
    
    // Update is called once per frame
    void Update () {
        //DEBUG: Show Grounding Lines
        DebugDrawLines();
        //Check if grounded
        GroundCheck();
        //Ask for player input
        PlayerControls();
        //Ladder velocity bugfix
        ResetLadderLocation();
    }
    
    void PlayerControls() {
        //Shooting
        if (Input.GetKey (KeyCode.Z)) {
            Shoot();
        }
        //Jump
        if (Input.GetKeyDown (KeyCode.X)) {
            if (playerOnLadder) {
                DetachFromLadder();
            }
            Jump();
        }
        
        if (Input.GetKeyUp (KeyCode.X)) {
            StopJump();
        }
        //Movement
        if (Input.GetKey(KeyCode.LeftArrow)){
            if (!playerOnLadder) {
                Move(ValidDirections.Left);
            }
            facingRight = false;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            if (!playerOnLadder) {
                Move(ValidDirections.Right);
            }
            facingRight = true;
        }
        //if not on ladder, attach to ladder if able
        if ( (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) && overLadder) {
            AttachToLadder();
        }
        
        
        //if not able to attach to ladder and pressing down AND pressing jump, slide
        
        //If on ladder Move
        if (Input.GetKey(KeyCode.UpArrow) && playerOnLadder){
            Move(ValidDirections.Up);
        }
        if (Input.GetKey(KeyCode.DownArrow) && playerOnLadder){
            Move(ValidDirections.Down);
        }
    }
    
    void Move(ValidDirections Direction) {
        switch (Direction) {
            case ValidDirections.Left:
                transform.position += transform.right*-1*moveSpeed*Time.deltaTime;
                break;
            case ValidDirections.Right:
                transform.position += transform.right*moveSpeed*Time.deltaTime;
                break;
            case ValidDirections.Up:
                transform.position += transform.up*moveSpeed*Time.deltaTime;
                break;
            case ValidDirections.Down:
                transform.position += transform.up*-1*moveSpeed*Time.deltaTime;
                break;
        }
        
    }
    
    void Shoot() {
        Vector2 ShootPosition = transform.localPosition;
        if (facingRight) {
            ShootPosition += gunPosition;
        } else {
            ShootPosition -= gunPosition;
        }
        GameObject BulletInstance = Instantiate(bullet,ShootPosition,Quaternion.identity) as GameObject;
        BulletInstance.GetComponent<ProjectileScript>().fireRight = facingRight;
    }
    
    void Jump() {
        rigidbody2D.AddForce(transform.up*jumpPower);
        jumped = true;
    }
    
    void StopJump() {
        Vector2 CurrentVelocity = rigidbody2D.velocity;
        if (CurrentVelocity.y>0) {
            CurrentVelocity.y *= .5f;
            rigidbody2D.velocity = CurrentVelocity;
            jumped = false;
        }
    }
    
    void OnTriggerEnter2D (Collider2D col) {
        centerx = col.transform.position.x+0.5f;
        overLadder = true;
        
    }
    
    void OnTriggerExit2D (Collider2D col) {
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
        middleGroundCenter = transform.localPosition;
        rightGroundCenter = transform.localPosition;
        
        leftGroundCenter.x += leftOffset;
        middleGroundCenter.x += middleOffset;
        rightGroundCenter.x += rightOffset;
        
        //Reset Ground Locations
        leftGroundCheck = leftGroundCenter;
        leftGroundCheck.y += feetOffset;
        middleGroundCheck = middleGroundCenter;
        middleGroundCheck.y += feetOffset;
        rightGroundCheck = rightGroundCenter;
        rightGroundCheck.y += feetOffset;
        
        
        
        leftGrounded = Physics2D.Linecast(leftGroundCenter, leftGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        middleGrounded = Physics2D.Linecast(middleGroundCenter, middleGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        rightGrounded = Physics2D.Linecast(rightGroundCenter, rightGroundCheck, 1 << LayerMask.NameToLayer("Ground"));  
        
        if (leftGrounded || middleGrounded || rightGrounded) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }
    
    void DebugDrawLines() {
        Debug.DrawLine (leftGroundCenter,leftGroundCheck);
        Debug.DrawLine (middleGroundCenter,middleGroundCheck);
        Debug.DrawLine (rightGroundCenter,rightGroundCheck);
    }
}
