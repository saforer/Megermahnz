using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Rigidbody2D Ammunition;
	public Transform GunObject;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot() {
 
		Rigidbody2D bulletInstance = Instantiate(Ammunition, GunObject.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
        bulletInstance.GetComponent<Projectile>().SetFacingRight(GetComponent<Movement>().facingRight);
        bulletInstance.GetComponent<Projectile>().SetOwnerTag(gameObject.tag);

    }
}
