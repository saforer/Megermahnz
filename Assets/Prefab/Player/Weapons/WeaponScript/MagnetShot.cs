using UnityEngine;
using System.Collections;

public class MagnetShot : MonoBehaviour {
    public GameObject playerObject;
    public float timeToArmed = .125f;
    public Vector2 setPos;
    GameObject[] magnetTracerArray;

	// Use this for initialization
	void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerObject.AddComponent<SpringJoint2D>();
        playerObject.GetComponent<SpringJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rigidbody2D.transform.position = setPos;

       if (timeToArmed > 0) {
            timeToArmed -= Time.deltaTime;
        } else {
            ArmedCheck ();
        }

        if (Input.GetKeyDown (KeyCode.X)) {
            DestroySelf();
        }

    }

    void DestroySelf() {
        Destroy (playerObject.GetComponent<SpringJoint2D>());
        Destroy (gameObject);
    }

    void ArmedCheck() {
        magnetTracerArray = GameObject.FindGameObjectsWithTag("Tracer"); 
        if (magnetTracerArray.Length != 0)
            DestroySelf ();
    }
}
