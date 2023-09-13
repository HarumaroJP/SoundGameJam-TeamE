﻿using System;
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

        public event Action<JudgeType, long> OnJudged;
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
                OnJudged?.Invoke(JudgeType.Tap, 0);
            }
            else
            {
                OnJudged?.Invoke(JudgeType.Miss, 0);
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
                OnJudged?.Invoke(JudgeType.Miss, 0);
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
                    OnJudged?.Invoke(JudgeType.Press, beatObserver.BeatCount - beatCount);
                }
            }
            else
            {
                OnJudged?.Invoke(JudgeType.Miss, 0);
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