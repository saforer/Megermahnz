using UnityEngine;
using System.Collections;

public class BlackHoleBullet : MonoBehaviour {
    public GameObject bullet;
    public float countdown = 5.0f;
    [HideInInspector]
    public Vector3 shootPosition;

	// Use this for initialization
	void Start () {

        for (int i=0; i<=11; i++) {
            shootPosition = gameObject.transform.localPosition + new Vector3(Random.Range(.5f,-.5f),Random.Range(.5f,-.5f),0);
            GameObject bulletInstance = Instantiate(bullet,shootPosition,Quaternion.identity) as GameObject;
            bulletInstance.GetComponent<SpringJoint2D>().connectedBody = gameObject.rigidbody2D;
            bulletInstance.GetComponent<BlackHoleParticle>().facingRight = GetComponent<CommonProjectile>().facingRight;
        }



	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown<=0) {
            Destroy (gameObject);
        }
	}

}
