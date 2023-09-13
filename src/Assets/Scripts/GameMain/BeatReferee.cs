using System;
using Core.Input;
using UnityEngine;

namespace GameMain
{
    public class BeatReferee
    {
        private double judgeOffsetBefore = 0.25;
        private double judgeOffsetAfter = 0.25;

        private InputEvent tapEvent;
        private InputEvent pressEvent;
        private long beatCount;
        private bool isPressing;

        public event Action<JudgeType> OnJudged;
        private BeatObserver beatObserver;

        public void Start()
        {
            beatObserver = BeatObserver.Instance;
            tapEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.ShootTap);
            pressEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.ShootPress);

            tapEvent.Started += _ => OnTap();
            pressEvent.Started += _ => OnPress();
            pressEvent.Canceled += _ => OnRelease();
        }

        private void OnTap()
        {
            if (GetJudgement())
            {
                OnJudged?.Invoke(JudgeType.Tap);
            }
            else
            {
                OnJudged?.Invoke(JudgeType.Miss);
            }
        }

        private void OnPress()
        {
            if (GetJudgement())
            {
                beatCount = beatObserver.BeatCount;
                isPressing = true;
            }
            else
            {
                OnJudged?.Invoke(JudgeType.Miss);
            }
        }

        private void OnRelease()
        {
            if (!isPressing)
                return;

            if (GetJudgement())
            {
                if (beatCount != beatObserver.BeatCount)
                {
                    OnJudged?.Invoke(JudgeType.Press);
                }
            }
            else
            {
                OnJudged?.Invoke(JudgeType.Miss);
            }

            isPressing = false;
        }

        private bool GetJudgement()
        {
            double dspTime = AudioSettings.dspTime;
            double nextTime = beatObserver.GetNextTime();
            Debug.Log(nextTime - dspTime);
            return dspTime >= nextTime - judgeOffsetBefore && dspTime <= nextTime + judgeOffsetAfter;
        }
    }
}