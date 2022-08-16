using Game.Core;
using Game.Movement;
using System;
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
        [SerializeField] float meleeForce = 5f;

        // **************** PROTOTYPE ****************
        [Header("Prototype objects")]
        [SerializeField] TrailRenderer trailRenderer;
        [SerializeField] SphereCollider enemiesDetector;
        // ************** END PROTOTYPE **************

        private bool attackPressed = false;

        [SerializeField] LayerMask enemyMask;

        private List<GameObject> targets = new List<GameObject>();

        private float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            trailRenderer.emitting = false;
        }

        private void Update()
        {

        }

        public void AttackBehaviour()
        {
            attackPressed = true;
            trailRenderer.emitting = true;
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");

            CheckForEnemies();
        }

        private void CheckForEnemies()
        {
            var enemiesList = new List<GameObject>();

            if (Physics.CheckSphere(transform.position, meleeRange, enemyMask))
            {
                Debug.Log("Enemy found");
            }
        }

        public void Cancel()
        {
            StopAttack();
        }

        private void StopAttack()
        {
            trailRenderer.emitting = false;
            targets.Clear();
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, meleeRange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                targets.Add(other.gameObject);

                var forceDirection = new Vector3(other.transform.position.x - transform.position.x,
                                                 0,
                                                 other.transform.position.z - transform.position.z);

                other.GetComponent<Rigidbody>().AddForce(forceDirection * meleeForce, ForceMode.Impulse);
                Debug.Log($"targets: {targets.Count}");
            }
        }

        public void EnableSphere()
        {
            enemiesDetector.enabled = true;
        }

        public void DisableSphere()
        {
            enemiesDetector.enabled = false;
        }
    }
}