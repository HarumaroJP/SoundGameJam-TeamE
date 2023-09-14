using System;
using Cysharp.Threading.Tasks;
using Player;
using UnityEngine;

namespace GameMain
{
    public class BeatPatternChecker : MonoBehaviour
    {
        [SerializeField] private PlayerShooter playerShooter;
        [SerializeField] private SpriteRenderer playerRenderer;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite missSprite;
        private readonly BeatReferee beatReferee = new BeatReferee();

        private void Start()
        {
            beatReferee.Start();

            beatReferee.OnJudged += (type, count) =>
            {
                if (type == JudgeType.Miss)
                    return;

                if (type == JudgeType.Tap)
                {
                    playerShooter.Shoot(0);
                }
                else if (type == JudgeType.Press && count == 1)
                {
                    playerShooter.Shoot(1);
                }
                else if (type == JudgeType.Press && count == 2)
                {
                    playerShooter.Shoot(2);
                }
                else
                {
                    PlayMissEffect().Forget();
                }
            };
        }

        private void Update()
        {
            beatReferee.Update();
        }

        private async UniTaskVoid PlayMissEffect()
        {
            playerRenderer.sprite = missSprite;

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

            playerRenderer.sprite = normalSprite;
        }
    }
}