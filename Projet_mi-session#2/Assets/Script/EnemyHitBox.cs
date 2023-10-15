using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public int healthAmount = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = healthAmount;
    }

    public void GetsDamaged(int damage)
    {
        currentHealth -= healthAmount;

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
