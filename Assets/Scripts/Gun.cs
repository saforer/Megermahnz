using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Rigidbody2D Ammunition;
	public Transform GunObject;
    bool tempFacing;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot() {
		Rigidbody2D bulletInstance = Instantiate(Ammunition, GunObject.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
        bulletInstance.GetComponent<Projectile>().FacingRight = GetComponent<Movement>().facingRight;
	}
}
