using UnityEngine;
using System.Collections;

public class MagnetTracer : MonoBehaviour {
    public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D (Collision2D col) {
        if (col.collider.CompareTag("Ground")) {
            GameObject BulletInstance = Instantiate(bullet,transform.localPosition,Quaternion.identity) as GameObject;
            BulletInstance.GetComponent<MagnetShot>().setPos = gameObject.transform.localPosition;
            Destroy(gameObject);
        }
    }
}
