using UnityEngine;

namespace Script
{
    public class EnemyHitBox : MonoBehaviour
    {
        public Animator animator;
        public int healthAmount = 200;
        int currentHealth;
        void Start()
        {
            currentHealth = healthAmount;
        }

        public void GetsDamaged(int damage)
        {
            currentHealth -= damage;
        
            animator.SetTrigger("Hit");

            if (currentHealth <= 0)
            {
                Death();
            }
        }

        void Death()
        {
            Debug.Log("Enemy dead!"); 
            animator.SetBool("Death", true);

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }

   
    }
}
