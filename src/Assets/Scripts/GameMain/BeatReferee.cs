using System;
using Core.Input;
using UnityEngine;

namespace GameMain
{
    public class BeatReferee
    {
        private InputEvent shootEvent;
        private long beatCount;
        private bool isTouching;

        public event Action<JudgeType> OnJudged;
        private BeatObserver beatObserver;

        public void Start()
        {
            beatObserver = BeatObserver.Instance;
            shootEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.Shoot);

            shootEvent.Started += _ => OnPress();
            shootEvent.Canceled += _ => OnRelease();
        }

        private void OnPress()
        {
            if (beatObserver.IsJudging)
            {
                beatCount = beatObserver.BeatCount;
            }
            else
            {
                OnJudged?.Invoke(JudgeType.Miss);
            }
        }

        private void OnRelease()
        {
            if (beatObserver.IsJudging)
            {
                if (beatCount == beatObserver.BeatCount)
                {
                    OnJudged?.Invoke(JudgeType.Tap);
                }
                else
                {
                    OnJudged?.Invoke(JudgeType.Press);
                }
            }
            else
            {
                OnJudged?.Invoke(JudgeType.Miss);
            }
        }
    }
}