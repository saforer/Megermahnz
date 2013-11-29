using UnityEngine;
using System.Collections;

public class PainField : MonoBehaviour {
    public string owner;
    public int Damage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D (Collision2D col) {
        if (col.collider.tag=="Player") {
            if (owner == "Enemy") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(Damage);
            }
        } 
        if (col.collider.tag=="Enemy") {
            if (owner == "Player") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(Damage);
            }
        }
    }

}
