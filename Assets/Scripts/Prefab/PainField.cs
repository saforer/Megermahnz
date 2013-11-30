using UnityEngine;
using System.Collections;

public class PainField : MonoBehaviour {
    string ownertag;
    public int Damage;
	// Use this for initialization
	void Start () {
        ownertag = gameObject.tag;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D (Collision2D col) {
        if (col.collider.tag=="Player") {
            if (ownertag == "Enemy") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(Damage);
            }
        } 
        if (col.collider.tag=="Enemy") {
            if (ownertag == "Player") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(Damage);
            }
        }
    }

}
