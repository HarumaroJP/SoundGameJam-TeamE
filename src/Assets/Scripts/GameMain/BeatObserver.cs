using System;
using Core.Input;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameMain
{
    public class BeatObserver : SingletonMonoBehaviour<BeatObserver>
    {
        [SerializeField] private int bpm;

        [SerializeField] private double fixOffset;
        [SerializeField] private float scale;
        [SerializeField] private float scaleDuration;

        private double beatTime;
        private double accumulateTime;
        private double startTime;

        public long BeatCount { get; private set; }
        public float ScaleRate { get; private set; } = 1f;

        public event Action OnBeat;

        private void Awake()
        {
            startTime = AudioSettings.dspTime;
        }

        private void FixedUpdate()
        {
            double currentTime = AudioSettings.dspTime;

            if (currentTime >= GetNextTime() - fixOffset)
            {
                DOTween.To(() => ScaleRate, x => ScaleRate = x, scale, scaleDuration).SetLoops(2, LoopType.Yoyo).Play();
                accumulateTime += 60.0 / bpm;
                BeatCount++;
                OnBeat?.Invoke();
            }
        }

        public double GetNextTime()
        {
            double bpmTime = 60.0 / bpm;
            return startTime + accumulateTime + bpmTime + fixOffset;
        }
    }
}