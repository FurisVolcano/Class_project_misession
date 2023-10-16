using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform meleePoint; 
    public LayerMask enemyLayers;
    
    public float meleeRange = 0.5f;
    private int meleeDamage = 20;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            meleeDamage = 20;
            Attack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            meleeDamage = 40;
            Attack();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            meleeDamage = 60;
            Attack();
        }
    }

    void Attack()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHitBox>().GetsDamaged(meleeDamage);
        }
       
    }

    void OnDrawGizmosSelected()
    {
        if(meleePoint == null)
            return;
        
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}
