using UnityEngine;
using System.Collections;

public class PlantBullet : MonoBehaviour {
    public Rigidbody2D playerObject;
    public int bounces = 2;

	// Use this for initialization
	void Start () {
        GetComponent<SpringJoint2D>().connectedBody = playerObject.GetComponent<Rigidbody2D>();       
	}
	
    void Awake () {

    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown (KeyCode.X)) {
            Destroy(gameObject);
        }
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
