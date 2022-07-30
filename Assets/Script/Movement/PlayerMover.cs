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
            if (!health.IsDead())
                Movement();
            //UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            Movement();
        }

        public void Movement()
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            if (Mathf.Abs(hor) > 0.1f)
            {
                GetComponent<Rigidbody>().velocity += transform.right * hor * speed;
            }
            if (Mathf.Abs(ver) > 0.1f)
            {
                GetComponent<Rigidbody>().velocity += transform.forward * ver * speed;
            }

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