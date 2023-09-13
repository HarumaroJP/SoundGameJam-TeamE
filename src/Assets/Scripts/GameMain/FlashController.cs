using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain {
    public class FlashController : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 125);
        }
        void Update()
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255,(0.5f * BeatObserver.Instance.ScaleRate));
        }
    }
}

