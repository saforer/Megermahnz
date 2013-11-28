using UnityEngine;
using System.Collections;

public class SealAI : MonoBehaviour {
    public int ShootingTimer = 0;   
    public int ShootingCount = 500;
    public Animator anim;
    bool FacingRight = false;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        ShootingTimer--;
        if (ShootingTimer <= 0) {
            ShootingTimer=ShootingCount;
            anim.SetBool ("SealShooting",true);
            SendMessage ("Shoot",FacingRight);
        }

        if (ShootingTimer <= 300) {
            anim.SetBool ("SealShooting",false);
        }
	}

    void DyingEffect () {
        for (int i=0; i<10; i++) {
        SendMessage ("Shoot",FacingRight);
        SendMessage ("Shoot",!FacingRight);
        }
    }
}
