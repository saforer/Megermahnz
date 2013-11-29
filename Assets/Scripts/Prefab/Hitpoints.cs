using UnityEngine;
using System.Collections;

public class Hitpoints : MonoBehaviour {
	public int HP;
    public float Hitstun=0;
    float Hitcounter = 0;
    bool stun;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (stun) {
            Hitcounter += Time.deltaTime;
            if (Hitcounter > Hitstun) {
                stun = false;
            }
        }


        if (HP<=0) {
            Death ();
        }
	}

	public void Hurt(int Damage) {
        if (!stun) {
            Hitcounter = 0;
            stun = true;
		    //Take Damage
		    HP=HP-Damage;
        }
	}
	

	public void Death() {
        Destroy (gameObject);
        SendMessage ("DyingEffect");
	}
}
