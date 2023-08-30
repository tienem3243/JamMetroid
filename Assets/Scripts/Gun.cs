using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Equipment
{
    public Transform bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shotDelay = 0.5f;

    private bool canShoot = true;
    private float timeSinceLastShot = 0f;
 
    void Update()
    {
        if (!IsEquip) return;
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetButton("Fire1") && canShoot)
        {
            PlayerController.Instance.Anim.SetBool("IsShooting", true);
            if (timeSinceLastShot >= shotDelay)
            {
                Shoot();
                timeSinceLastShot = 0f;
               
               
            }
            PlayerController.Instance.Stand();
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {
           ;
            PlayerController.Instance.Anim.SetBool("IsShooting", false);
        }


    }
    void Shoot()
    {
        // Instantiate the bullet prefab at the bullet spawn point's position and rotation
        var dir = PlayerController.Instance.Anim;
        Instantiate(bulletPrefab, bulletSpawnPoint.position, dir.transform.rotation);


    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
