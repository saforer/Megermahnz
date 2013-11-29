using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public string owner;
    public int projectilespeed;
    public bool FacingRight;
    public int Direction;
	// Use this for initialization
	void Start () {
        if (FacingRight){
            Direction=1;
        }else{
            Direction=-1;
        }
        rigidbody2D.AddForce (transform.right*projectilespeed*Direction);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnCollisionEnter2D (Collision2D col) {
        if (col.collider.tag=="Player") {
            if (owner == "Enemy") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(1);
            }
                Destroy (gameObject);
        } 
        if (col.collider.tag=="Enemy") {
            if (owner == "Player") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(1);
            }
            Destroy (gameObject);
        }
        if(col.collider.tag == "Ground") {
            Destroy (gameObject);
        }
    }
}
