using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private string _ownertag;
    public int projectilespeed;
    private bool _FacingRight;
    int Direction;
    public int Damage;
    public bool bouncable;
    public int bounces;
    public bool connected;
    private Transform player;
	// Use this for initialization
	void Start () {
        Direction = (_FacingRight) ? 1 : -1;
        if(connected) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            GetComponent<SpringJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }
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
        if(col.gameObject.CompareTag("Projectile")) {
            if(!bouncable) {
                Destroy (gameObject);
            } else {
                bounces--;
                if (bounces>=0) {
                    rigidbody2D.velocity = new Vector2(Random.Range(-20,20),Random.Range (-40,40));
                } else {
                    Destroy (gameObject);
                }
            }
        }
        if(col.gameObject.CompareTag("Ground")) {
                if(!bouncable) {
                    Destroy (gameObject);
                } else {
                    bounces--;
                    if (bounces>=0) {
                        rigidbody2D.velocity = new Vector2(Random.Range(-20,20),Random.Range (-40,40));
                    } else {
                        Destroy (gameObject);
                    }
                }
            }
    }

    public void SetFacingRight (bool FacingRight) {
        _FacingRight = FacingRight;
    }

    public void SetOwnerTag (string ownertag) {
        _ownertag = ownertag;
    }

}
