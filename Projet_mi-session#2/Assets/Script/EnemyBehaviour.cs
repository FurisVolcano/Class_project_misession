using System;
using TMPro;
using UnityEditor.SearchService;
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
        private Transform target;
        private Animator animator;
        private float distance;//store the distance between enemy and player
        private bool attackmode;
        private bool inRange;
        private bool cooling; //check if ennemy is cooling after attack;
        private float intTimer;
        private static readonly int SkeletonAttack = Animator.StringToHash("Skeleton_Attack");
        private static readonly int SkeletonWalk = Animator.StringToHash("Skeleton_Walk");
        private static readonly int Walk = Animator.StringToHash("SkeletonWalk");
        public Transform leftLimit;
        public Transform rightLimit;

        #endregion

        void Awake()
        {
            SelectTarget();
            intTimer = timer;//store the initial value of timer
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!attackmode)
            {
                Move();
            }

            if (!InsideOfLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
            {
                SelectTarget();
            }
            if (inRange)
            {
                hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLenght, RayCastMask);
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
            
                StopAttack();
            }
        }

        void EnemyLogic()
        {
            distance = Vector2.Distance(transform.position, target.position);

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
                animator.SetBool("Skeleton_Walk", true);
                
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
                {
                    Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
                    transform.position =
                        Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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
        private void OnTriggerEnter2D(Collider2D trig)
        {
            if (trig.gameObject.CompareTag("Player"))
            {
                target = trig.transform;
                inRange = true;
                Flip();
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
                    Debug.DrawRay(rayCast.position, transform.right * rayCastLenght, Color.red);
                }
                else if (attackDistance > distance)
                {
                    Debug.DrawRay(rayCast.position, transform.right * rayCastLenght);
                }
            }

        public void TriggerCooling()
        {
            cooling = true;
        }

        private bool InsideOfLimits()
        {
            return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
        }

        private void SelectTarget()
        {
            float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
            float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

            if (distanceToLeft > distanceToRight)
            {
                target = leftLimit;
            }
            else
            {
                target = rightLimit;
            }

            Flip();
        }

        private void Flip()
        {
            Vector3 rotation = transform.eulerAngles;
            if (transform.position.x > target.position.x)
            {
                rotation.y = 180f;
            }
            else
            {
                rotation.y = 0f;
            }

            transform.eulerAngles = rotation;
        }
        
    }
    
}


