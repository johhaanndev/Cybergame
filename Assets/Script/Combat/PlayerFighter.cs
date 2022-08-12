using Game.Core;
using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerFighter : MonoBehaviour, IAction
    {
        [SerializeField] float meleeRange = 2.0f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 5.0f;

        [SerializeField] LayerMask enemyMask;

        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            //timeSinceLastAttack += Time.deltaTime;

        }

        public void MeleeAttack()
        {
            if (Physics.CheckSphere(transform.position, meleeRange, enemyMask))
            {
                Debug.Log("Enemy Detected");
            }
        }

        /// <summary>
        /// Animation Event when attacking
        /// </summary>
        private void Hit()
        {
            if (target == null)
                return;

            target.TakeDamage(weaponDamage);
        }

        public void Cancel() { }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, meleeRange);
        }
    }
}