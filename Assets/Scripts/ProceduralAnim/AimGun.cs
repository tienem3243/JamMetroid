using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGun : MonoBehaviour
{
    public Transform aimRig;
    public float radius = 10f; 
    public float unity = 2f;
    private void FixedUpdate()
    {
        Vector2 aimDesire = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimRig.position=new Vector3(aimDesire.x,aimDesire.y,aimRig.position.z);
       

    }
   

}
