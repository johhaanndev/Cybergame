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

            //InteractWithCombat()

            if (InteractWithMovement())
                return;

        }

        //private bool InteractWithCombat()
        //{
        //    RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        //    foreach (RaycastHit hit in hits)
        //    {
        //        var target = hit.transform.GetComponent<CombatTarget>();
        //        if (target == null)
        //            continue;

        //        if (!GetComponent<Fighter>().CanAttack(target.gameObject))
        //            continue;

        //        if (Input.GetMouseButton(1))
        //        {
        //            GetComponent<Fighter>().Attack(target.gameObject);
        //        }
        //        return true;
        //    }
        //    return false;
        //}

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

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}