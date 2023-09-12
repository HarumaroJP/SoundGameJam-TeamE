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
        [SerializeField] private double judgeOffset;
        [SerializeField] private float scale;
        [SerializeField] private float scaleDuration;

        private double beatTime;
        private double currentTime;

        public bool IsJudging { get; private set; }
        public long BeatCount { get; private set; }
        public float ScaleRate { get; private set; } = 1f;

        private void Awake()
        {
            beatTime = 60.0 / bpm;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (!IsJudging && currentTime >= beatTime - judgeOffset)
            {
                Judge().Forget();
            }

            if (currentTime >= beatTime)
            {
                DOTween.To(() => ScaleRate, x => ScaleRate = x, scale, scaleDuration).SetLoops(2, LoopType.Yoyo).Play();
                currentTime -= beatTime;
                BeatCount++;
            }
        }

        async UniTaskVoid Judge()
        {
            IsJudging = true;

            await UniTask.Delay(TimeSpan.FromSeconds(judgeOffset * 2.0));

            IsJudging = false;
        }
    }
}