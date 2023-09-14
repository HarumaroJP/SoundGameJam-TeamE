using System;
using Core.Input;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameMain
{
    public class BeatObserver : SingletonMonoBehaviour<BeatObserver>
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private int startBpm;
        [SerializeField] private int originalBpm;
        [SerializeField] private int bpm;
        [SerializeField] private int maxBpm;
        [SerializeField] private int increaseBpm;
        [SerializeField] private int changeBeatCount;

        [SerializeField] private double fixOffset;
        [SerializeField] private float scale;
        [SerializeField] private float scaleDuration;

        private double beatTime;
        private double accumulateTime;
        private double startTime;

        public long BeatCount { get; private set; }
        public float ScaleRate { get; private set; } = 1f;

        public double BpmUnit => 60.0 / bpm;

        public event Action OnBeat;

        private void Awake()
        {
            startTime = AudioSettings.dspTime;
            bpm = startBpm;
            audioSource.pitch = (float)bpm / originalBpm;
        }

        private void FixedUpdate()
        {
            double currentTime = AudioSettings.dspTime;

            if (currentTime >= GetNextTime() - fixOffset)
            {
                DOTween.To(() => ScaleRate, x => ScaleRate = x, scale, scaleDuration).SetLoops(2, LoopType.Yoyo).Play();
                accumulateTime += BpmUnit;
                BeatCount++;
                OnBeat?.Invoke();

                if (bpm < maxBpm && BeatCount % changeBeatCount == 0)
                {
                    bpm += increaseBpm;
                    bpm = Mathf.Clamp(bpm, 0, maxBpm);
                    audioSource.pitch = (float)bpm / originalBpm;
                }
            }
        }

        public double GetNextTime()
        {
            return startTime + accumulateTime + BpmUnit + fixOffset;
        }
    }
}