﻿using UnityEngine;
using System.Collections;

public class PainField : MonoBehaviour {
    public int owner;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D (Collision2D col) {
        if (owner==1) {
            if (col.collider.tag=="Player") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(1);
            }
        } else if (owner==2) {
            if (col.collider.tag=="Enemy") {
                col.collider.gameObject.GetComponent<Hitpoints>().Hurt(1);
            }
        }
        if(col.collider.tag == "Ground") {
        }
    }

}
