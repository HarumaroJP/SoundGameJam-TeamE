using System;
using System.Collections;
using System.Collections.Generic;
using Core.Input;
using UnityEngine;

namespace Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private PlayerMovement playerMovement;

        private InputEvent shootEvent;

        private void Start()
        {
            shootEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.Shoot);

            shootEvent.Started += ctx =>
            {
                if (playerMovement.IsMoving)
                    return;

                Bullet bullet = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Shoot();
            };
        }

    }
}