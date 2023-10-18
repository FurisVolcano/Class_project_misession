using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script
{
    public class EnemyBehaviour : MonoBehaviour
    {
        #region Public Variables

        public Transform rayCast;
        public LayerMask RayCastMask;
        public float rayCastLenght;
        public float attackDistance;
        public float moveSpeed;
        public float timer;//timer for cooldown between attacks

        #endregion

        #region Private Variables
        private RaycastHit2D hit;
        private GameObject target;
        private Animator animator;
        private float distance;//store the distance between enemy and player
        private bool attackmode;
        private bool inRange;
        private bool cooling; //check if ennemy is cooling after attack;
        private float intTimer;
        #endregion

        void Awake()
        {
            intTimer = timer;//store the initial value of timer
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (inRange)
            {
                hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLenght, RayCastMask);
                RayCastDebugger();
            }

            if (hit.collider != null)
            {
                EnemyLogic();
            }
            else if (hit.collider == null)
            {
                inRange = false;
            }

            if (inRange == false)
            {
                animator.SetBool("Skeleton_Walk", false);
                StopAttack();
            }
        }

        void EnemyLogic()
        {
            distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance > attackDistance)
            {
                Move();
                StopAttack();
            }
            else if (attackDistance >= distance && cooling == false)
            {
                Attack();
            }

            if (cooling)
            {
                Cooldown();
                animator.SetBool("Skeleton_Attack", false);
            }
        }

        void Move()
            {
                animator.SetBool("SkeletonWalk", true);
                
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Skel_attack"))
                {
                    Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
                    transform.position =
                        Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
            }
        
        
        private void OnTriggerEnter2D(Collider2D trig)
        {
            if (trig.gameObject.tag == "Player")
            {
                target = trig.gameObject;
                inRange = true;
            }
        }

        void Attack()
        {
            timer = intTimer; //Reset Timer when player enter Attack Range
            attackmode = true;//check if enemy can still attack
            
            animator.SetBool("Skeleton_Walk", false);
            animator.SetBool("Skeleton_Attack", true);
        }

        private void Cooldown()
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && cooling && attackmode)
            {
                cooling = false;
                timer = intTimer;
            }
        }

        void StopAttack()
        {
            cooling = false;
            attackmode = false;
            animator.SetBool("Skeleton_Attack", false);
        }

        void RayCastDebugger()
            {
                if (distance > attackDistance)
                {
                    Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght, Color.red);
                }
                else if (attackDistance > distance)
                {
                    Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght);
                }
            }

        public void TriggerCooling()
        {
            cooling = true;
        }
        }
    }



