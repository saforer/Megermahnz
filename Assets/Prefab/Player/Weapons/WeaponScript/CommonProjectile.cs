using UnityEngine;
using System.Collections;

public class CommonProjectile : MonoBehaviour {
    [HideInInspector]
    public bool facingRight;
    public float rightVelocity;
    public float upVelocity;

    void Start () {
        Vector2 CurrentVelocity = new Vector2(rightVelocity,upVelocity);
        if (!facingRight) {
            CurrentVelocity.x *= -1;
        }
        rigidbody2D.velocity = CurrentVelocity;
    }
    
    void KillBox () {
        Destroy(gameObject);
    }


}
