using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    public Vector2 gunPosition = new Vector2(0.628f,0.0f);
    public int selectedWeapon = 0;
    public GameObject normalBullet;
    public GameObject sandCannon;
    public GameObject plantBullet;
    public GameObject magnetTracer;
    public GameObject blackHoleBullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A)){
            selectedWeapon--;
            if (selectedWeapon<0) selectedWeapon=0;

        }
        if (Input.GetKeyDown(KeyCode.S)){
            selectedWeapon++;
            if (selectedWeapon>4) selectedWeapon=4;
        }
    
	}

    public void Shoot (bool facingRight, Rigidbody2D player) {
        switch (selectedWeapon) {
            case 0:
                FireNormalBullet(facingRight);
                break;
            case 1:
                FireSandCannon(facingRight);
                break;
            case 2:
                FirePlantBullet(facingRight, player);
                break;
            case 3:
                FireMagnetTracer(facingRight);
                break;
            case 4:
                FireBlackHoleBullet(facingRight);
                break;
        }
    }

    void FireNormalBullet(bool facingRight) {
        Vector2 ShootPosition = transform.localPosition;
        if (facingRight) {
            ShootPosition += gunPosition;
        } else {
            ShootPosition -= gunPosition;
        }
        
       GameObject BulletInstance = SpawnBullet(normalBullet,ShootPosition, facingRight);

    }

    void FireSandCannon(bool facingRight){
        for (int i=0; i<=19; i++) {
            Vector2 ShootPosition = transform.localPosition;
            if (facingRight) {
                ShootPosition += gunPosition;
            } else {
                ShootPosition -= gunPosition;
            }
            
            GameObject BulletInstance = SpawnBullet(sandCannon,ShootPosition, facingRight);
        }
    }

    void FirePlantBullet(bool facingRight, Rigidbody2D player){
        for (int i=0; i<=22; i++) {
            Vector2 ShootPosition = transform.localPosition;
            if (facingRight) {
                ShootPosition += gunPosition * Random.Range (-0.2f,0.2f);
            } else {
                ShootPosition -= gunPosition * Random.Range (-0.2f,0.2f);
            }

            GameObject BulletInstance = SpawnBullet(plantBullet,ShootPosition, facingRight);
            BulletInstance.GetComponent<PlantBullet>().playerObject = player;
        }
    }

    void FireMagnetTracer(bool facingRight){
        Vector2 ShootPosition = transform.localPosition;
        if (facingRight) {
            ShootPosition += gunPosition;
        } else {
            ShootPosition -= gunPosition;
        }
        
        GameObject BulletInstance = SpawnBullet(magnetTracer,ShootPosition, facingRight);
    }

    void FireBlackHoleBullet(bool facingRight){
        Vector2 ShootPosition = transform.localPosition;
        if (facingRight) {
            ShootPosition += gunPosition;
        } else {
            ShootPosition -= gunPosition;
        }

        GameObject BulletInstance = SpawnBullet(blackHoleBullet,ShootPosition,facingRight);
    }

    GameObject SpawnBullet(GameObject bullet, Vector2 shootpos, bool facing) {
        GameObject BulletInstance = Instantiate(bullet,shootpos,Quaternion.identity) as GameObject;
        BulletInstance.GetComponent<CommonProjectile>().facingRight = facing;
        return BulletInstance;
    }

}
