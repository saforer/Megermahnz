using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Rigidbody2D Ammunition;
	public float Speed;
	public Transform GunObject;
    int Direction = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(bool FacingRight) {
        if (FacingRight==true) {
            Direction = 1;
        } else {
            Direction = -1;
        }
		Rigidbody2D bulletInstance = Instantiate(Ammunition, GunObject.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(Speed, 0)*Direction;
	}
}
