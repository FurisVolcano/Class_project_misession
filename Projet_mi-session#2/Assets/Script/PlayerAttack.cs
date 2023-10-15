using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform meleePoint;
    public float meleeRange = 0.5f;
    public LayerMask enemyLayers;
    public int meleeDamage;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {

            enemy.GetComponent<EnemyHitBox>().GetsDamaged(meleeDamage);
            if (Input.GetButtonDown("Fire1"))
            {
                meleeDamage = 20;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                meleeDamage = 40;
            }
            if (Input.GetButtonDown("Fire3"))
            {
                meleeDamage = 60;
            }
        }
       
    }

    void OnDrawGizmosSelected()
    {
        if(meleePoint == null)
            return;
        
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}
