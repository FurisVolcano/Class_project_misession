using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
  
    public int healthAmount = 200;
    int currentHealth;
    void Start()
    {
        currentHealth = healthAmount;
    }

    public void GetsDamaged(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
       Debug.Log("Enemy dead!"); 
    }

   
}
