using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private string _ownertag;
    public int projectilespeed;
    private bool _FacingRight;
    int Direction;
    public int Damage;
    public bool Bouncable;
    public int Bounces;
    public bool Connected;
    private Transform Player;
    public bool IsTurret;
    public Transform TurretAmmo;
    public float TimeToDestroy;
	// Use this for initialization
	void Start () {
        Direction = (_FacingRight) ? 1 : -1;
        if(Connected) {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            GetComponent<SpringJoint2D>().connectedBody = Player.GetComponent<Rigidbody2D>();
        }
        rigidbody2D.velocity = transform.right*projectilespeed*Direction;
	}
	
	// Update is called once per frame
	void Update () {
        TimeToDestroy-=Time.deltaTime;
        if(TimeToDestroy<=0){
            Destroy (gameObject);
        }
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
        if(col.gameObject.CompareTag("Ground")) {
            if(!Bouncable&&!IsTurret) {
                Destroy (gameObject);
            } 
            if (Bouncable) {
                Bounces--;
                if (Bounces>=0) {
                   // rigidbody2D.velocity = new Vector2(Random.Range(-20,20),Random.Range (-40,40));
                } else {
                    Destroy (gameObject);
                }
            }
            if (IsTurret) {
                ActivateTurret(gameObject.transform.localPosition);
            }
        }
    }

    public void SetFacingRight (bool FacingRight) {
        _FacingRight = FacingRight;
    }

    public void SetOwnerTag (string ownertag) {
        _ownertag = ownertag;
    }

    private void ActivateTurret(Vector3 Shotposition) {
        Rigidbody2D turretInstance = Instantiate(TurretAmmo, Shotposition, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
    }

}
