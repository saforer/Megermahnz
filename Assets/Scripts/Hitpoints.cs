﻿using UnityEngine;
using System.Collections;

public class Hitpoints : MonoBehaviour {
	public int HP;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (HP<=0) {
            Death ();
        }
	}

	public void Hurt(int Damage) {
		//Take Damage
		HP=HP-Damage;
		//Diecheck

	}
	

	public void Death() {
        Destroy (gameObject);
        SendMessage ("DyingEffect");
	}
}
