using System;
using Core.Input;
using UnityEngine;

namespace GameMain
{
    public class BeatReferee
    {
        private double judgeOffsetBefore = 0.25;
        private double judgeOffsetAfter = 0.25;

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
            double dspTime = AudioSettings.dspTime;
            double nextTime = beatObserver.GetNextTime();
            bool isJudging = dspTime >= nextTime - judgeOffsetBefore && dspTime <= nextTime + judgeOffsetAfter;

            if (isJudging)
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
            double dspTime = AudioSettings.dspTime;
            double nextTime = beatObserver.GetNextTime();
            bool isJudging = dspTime >= nextTime - judgeOffsetBefore && dspTime <= nextTime + judgeOffsetAfter;

            Debug.Log(nextTime - dspTime);

            if (isJudging)
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