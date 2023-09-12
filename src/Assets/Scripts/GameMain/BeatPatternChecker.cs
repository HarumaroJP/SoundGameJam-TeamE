using System;
using UnityEngine;

namespace GameMain
{
    public class BeatPatternChecker : MonoBehaviour
    {
        private readonly BeatReferee beatReferee = new BeatReferee();

        private void Start()
        {
            beatReferee.Start();

            beatReferee.OnJudged += type =>
            {
                Debug.Log(type);
            };
        }

        private void Update()
        {
            transform.localScale = Vector3.one * BeatObserver.Instance.ScaleRate;
        }
    }
}