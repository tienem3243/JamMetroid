using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CustomBehaviour
{
    [SerializeField]private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = -transform.right *speed; 
    }

   
}
