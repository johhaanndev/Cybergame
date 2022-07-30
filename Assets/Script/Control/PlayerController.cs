using Game.Core;
using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            //if (InteractWithCombat())
            //    return;

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
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            if (Mathf.Abs(hor) >= 0.1f || Mathf.Abs(ver) >= 0.1f)
            {
                GetComponent<PlayerMover>().StartMoveAction(hor, ver);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}