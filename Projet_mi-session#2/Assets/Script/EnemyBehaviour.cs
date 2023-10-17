using UnityEngine;

namespace Script
{
    public class EnemyBehaviour : MonoBehaviour
    {
        #region Public Variables

        public Transform rayCast;
        public LayerMAsk RayCastMask;
        public float rayCastLenght;
        public float attackDistance;
        public float moveSpeed;
        public float timer;
        #endregion
        
        #region Private Variables

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
