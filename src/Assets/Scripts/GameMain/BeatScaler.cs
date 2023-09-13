using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
    public class BeatScaler : MonoBehaviour
    {
        private Vector3 startScale;

        void Start()
        {
            startScale = transform.localScale;
        }

        void Update()
        {
            transform.localScale = startScale * BeatObserver.Instance.ScaleRate;
        }
    }
}