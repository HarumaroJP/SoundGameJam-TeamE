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
        [SerializeField] private double judgeOffsetBefore;
        [SerializeField] private double judgeOffsetAfter;
        [SerializeField] private double fixOffset;
        [SerializeField] private float scale;
        [SerializeField] private float scaleDuration;

        private double beatTime;
        private double currentTime;

        public bool IsJudging { get; private set; }
        public long BeatCount { get; private set; }
        public float ScaleRate { get; private set; } = 1f;

        public event Action OnBeat;

        private void Awake()
        {
            beatTime = 60.0 / bpm;
            Debug.Log(beatTime);
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (!IsJudging && currentTime >= beatTime - judgeOffsetBefore + fixOffset)
            {
                Judge().Forget();
            }

            if (currentTime >= beatTime + fixOffset)
            {
                DOTween.To(() => ScaleRate, x => ScaleRate = x, scale, scaleDuration).SetLoops(2, LoopType.Yoyo).Play();
                currentTime -= beatTime;
                BeatCount++;
                OnBeat?.Invoke();
            }
        }

        async UniTaskVoid Judge()
        {
            IsJudging = true;

            await UniTask.Delay(TimeSpan.FromSeconds(judgeOffsetBefore + judgeOffsetAfter));

            IsJudging = false;
        }
    }
}