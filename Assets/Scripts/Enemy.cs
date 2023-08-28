using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CustomBehaviour
{
    [SerializeField] private float _hp;
    bool isDead;
    protected void TakeDamage(float damageAmount)
    {
        _hp -= damageAmount;
        if (_hp <= 0&& !isDead) Death();
    }

    protected void Death()
    {
        Destroy(gameObject);
    }
} 
