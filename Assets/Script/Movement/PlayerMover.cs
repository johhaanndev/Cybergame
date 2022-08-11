using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class PlayerMover : MonoBehaviour, IAction
    {
        [SerializeField] float speed = 6f;

        private Vector3 direction = Vector3.zero;
        private Vector3 rbVelocity = Vector3.zero;

        private Rigidbody rb;
        private Health health;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            UpdateAnimator(rbVelocity);
        }

        public void StartMoveAction(float hor, float ver, float aimHor, float aimVer)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            Movement(hor, ver);
            Rotation(hor, ver, aimHor, aimVer);
        }

        public void Movement(float hor, float ver)
        {
            rbVelocity = new Vector3(hor * speed, 0, ver * speed);
            if (hor != 0 || ver != 0)
            {
                direction = new Vector3(hor, 0, ver);
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }

        public void Rotation(float hor, float ver, float aimHor, float aimVer)
        {
            var look = (Mathf.Abs(aimHor) > 0.1f || Mathf.Abs(aimVer) > 0.1f) ?
                new Vector3(aimHor, 0, aimVer) :
                new Vector3(hor, 0, ver);
            
            Debug.Log($"Aiming to: {look.x.ToString("F2")}, {look.y.ToString("F2")}");
            transform.rotation = Quaternion.LookRotation(look);
        }

        public void Cancel() { }

        private void UpdateAnimator(Vector3 velocity)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}