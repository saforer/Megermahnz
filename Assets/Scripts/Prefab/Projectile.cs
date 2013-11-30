using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private string _ownertag;
    public int projectilespeed;
    private bool _FacingRight;
    int Direction;
    public int Damage;
	// Use this for initialization
	void Start () {
        Direction = (_FacingRight) ? 1 : -1;
        rigidbody2D.velocity = transform.right*projectilespeed*Direction;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            if (_ownertag == "Enemy") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(Damage);
                Destroy (gameObject);
            }
        } 
        if (col.gameObject.CompareTag("Enemy")) {
            if (_ownertag == "Player") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(Damage);
                Destroy (gameObject);
            }
        }
        if(col.collider.tag == "Ground") {
            Destroy (gameObject);
        }
    }

    public void SetFacingRight (bool FacingRight) {
        _FacingRight = FacingRight;
    }

    public void SetOwnerTag (string ownertag) {
        _ownertag = ownertag;
    }

}
