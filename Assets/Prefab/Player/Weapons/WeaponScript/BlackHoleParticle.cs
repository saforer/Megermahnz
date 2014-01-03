using UnityEngine;
using System.Collections;

public class BlackHoleParticle : MonoBehaviour {
    public float countdown = 5.0f;
    [HideInInspector]
    public bool exploded = false;
    [HideInInspector]
    public bool facingRight;
    public float rightVelocity;
    public float upVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown<=0.0f && !exploded) {
            exploded = true;
            rigidbody2D.gravityScale=1;
            Vector2 CurrentVelocity = new Vector2(rightVelocity,upVelocity);
            if (!facingRight) {
                CurrentVelocity.x *= -1;
            }
            rigidbody2D.velocity = CurrentVelocity;
        }
	}

    void KillBox () {
        Destroy(gameObject);
    }
}
