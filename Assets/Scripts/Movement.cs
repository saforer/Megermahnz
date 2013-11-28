using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float speed;
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void Move(int Direction) {
		if (Direction>0){
			Vector3 theScale = transform.localScale;
			theScale.x = 1;
			transform.localScale = theScale;
		}else if (Direction<0) {
			Vector3 theScale = transform.localScale;
			theScale.x = -1;
			transform.localScale = theScale;
		}
		transform.position += transform.right*speed*Time.deltaTime*Direction;
	}
}
