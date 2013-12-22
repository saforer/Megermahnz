using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    [HideInInspector]
    public bool fireRight = true;
    public bool isAttached;
    private GameObject playerObject;
    public int bounces = 2;
    // Use this for initialization
    void Start () {
        if (isAttached) {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            GetComponent<SpringJoint2D>().connectedBody = playerObject.GetComponent<Rigidbody2D>();
        }

        Vector2 CurrentVelocity = new Vector2(20.0f,0);
        if (!fireRight) {
            CurrentVelocity *= -1;
        }
        if (!isAttached) {
            rigidbody2D.velocity = CurrentVelocity;
        }
    }
    
    // Update is called once per frame
    void Update () {


        if (isAttached) {
           

            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                rigidbody2D.AddForce(new Vector2(-2000,0));
                bounces--;
            }
            if (Input.GetKeyDown (KeyCode.RightArrow)) {
                rigidbody2D.AddForce(new Vector2(2000,0));
                bounces--;
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                rigidbody2D.AddForce(new Vector2(0,-2000));
                bounces--;
            }

            if (bounces<=0) {
                Destroy (gameObject);
            }
        }
    }
}
