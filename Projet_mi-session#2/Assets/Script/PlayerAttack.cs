using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform meleePoint; 
    public LayerMask enemyLayers;
    
    public float meleeRange = 0.5f;
    private int meleeDamage = 20;
    private float nextMeleeTime = 0f;
    public float meleeRate = 2f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextMeleeTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.Play("Attack#1_Animation");
                meleeDamage = 20;
                Attack();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                animator.Play("Attack#2_Animation");
                meleeDamage = 40;
                Attack();
                nextMeleeTime = Time.time + 5f / meleeRate;
            }

            if (Input.GetButtonDown("Fire3"))
            {
                animator.Play("Wolf_Dash");
                meleeDamage = 60;
                Attack();
                nextMeleeTime = Time.time + 10f / meleeRate;
            }
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
