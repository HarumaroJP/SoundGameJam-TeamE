using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class ObjectDestroyer : MonoBehaviour
    {
        [SerializeField] private float removeDelay;

        private void Start()
        {
            Destroy(gameObject, removeDelay);
        }
    }
}