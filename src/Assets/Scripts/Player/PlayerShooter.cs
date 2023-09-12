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

        private InputEvent shootEvent;

        private void Start()
        {
            shootEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.MoveUp);

            shootEvent.Started += ctx =>
            {
                
            };
        }
    }
}