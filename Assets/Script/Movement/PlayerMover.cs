using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class PlayerMover : MonoBehaviour, IAction
    {
        [SerializeField] float speed = 6f;

        private Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            //UpdateAnimator();
        }

        public void StartMoveAction(float hor, float ver)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            Movement(hor, ver);
        }

        public void Movement(float hor, float ver)
        {
            GetComponent<Rigidbody>().velocity = ((transform.right * hor) + (transform.forward * ver )) * speed;
        }

        public void Cancel() { }

        //private void UpdateAnimator()
        //{
        //    Vector3 velocity = navMeshAgent.velocity;
        //    Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        //    float speed = localVelocity.z;
        //    GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        //}
    }
}