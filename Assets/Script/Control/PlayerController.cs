using Game.Combat;
using Game.Core;
using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Control
{

    public class PlayerController : MonoBehaviour
    {
        private Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
                return;

            InteractWithCombat();

            if (InteractWithMovement())
                return;

        }

        private void InteractWithCombat()
        {
            if (Gamepad.current.buttonWest.wasPressedThisFrame)
            {
                GetComponent<PlayerFighter>().AttackBehaviour();
            }
        }

        private bool InteractWithMovement()
        {
            float hor = Gamepad.current.leftStick.x.ReadValue();
            float ver = Gamepad.current.leftStick.y.ReadValue();
            float aimHor = Gamepad.current.rightStick.x.ReadValue();
            float aimVer = Gamepad.current.rightStick.y.ReadValue();
            if (Mathf.Abs(hor) >= 0.1f || Mathf.Abs(ver) >= 0.1f)
            {
                GetComponent<PlayerMover>().StartMoveAction(hor, ver, aimHor, aimVer);
                return true;
            }
            GetComponent<PlayerMover>().StartMoveAction(0, 0, aimHor, aimVer);
            return false;
        }        
    }

}