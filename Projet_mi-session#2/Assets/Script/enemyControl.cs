using System;
using UnityEngine;

namespace Script
{
    public class EnemyControl : MonoBehaviour
    {
        public float speed;
        public float distance;
        
        private bool right = true;
        public Transform groundDetection;

        void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
            if (groundInfo.collider == false)
            {
                if (right == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    right = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    right = true;
                }
            }
        }

    }
}
