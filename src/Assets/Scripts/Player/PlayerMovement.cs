using System;
using System.Collections;
using System.Collections.Generic;
using Core.Input;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float smoothDuration;
        [SerializeField] private float moveInterval;
        [SerializeField] private float moveCount;

        private InputEvent moveUpEvent;
        private InputEvent moveDownEvent;

        private Tween moveUpTween;
        private Tween moveDownTween;

        private int currentMoveCount;

        private void Start()
        {
            DOTween.defaultAutoPlay = AutoPlay.None;

            moveUpEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.MoveUp);
            moveDownEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.MoveDown);

            moveUpEvent.Started += ctx =>
            {
                if (currentMoveCount >= moveCount)
                    return;

                moveUpTween?.Complete();

                moveUpTween = transform.DOMoveY(transform.position.y + moveInterval, smoothDuration);
                moveUpTween.Play();
                currentMoveCount++;
            };

            moveDownEvent.Started += ctx =>
            {
                if (currentMoveCount <= 0)
                    return;

                moveDownTween?.Complete();

                moveDownTween = transform.DOMoveY(transform.position.y - moveInterval, smoothDuration).Play();
                moveDownTween.Play();
                currentMoveCount--;
            };
        }
    }
}