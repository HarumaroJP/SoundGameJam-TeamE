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

        public bool IsMoving { get; private set; }

        private void Start()
        {
            DOTween.defaultAutoPlay = AutoPlay.None;

            moveUpEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.MoveUp);
            moveDownEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.MoveDown);

            moveUpEvent.Started += ctx =>
            {
                if (IsMoving || currentMoveCount >= moveCount)
                    return;

                IsMoving = true;

                moveUpTween = transform.DOMoveY(transform.position.y + moveInterval, smoothDuration);
                moveUpTween.Play().OnComplete(() => IsMoving = false);
                currentMoveCount++;
            };

            moveDownEvent.Started += ctx =>
            {
                if (IsMoving || currentMoveCount <= 0)
                    return;

                IsMoving = true;

                moveDownTween = transform.DOMoveY(transform.position.y - moveInterval, smoothDuration).Play();
                moveDownTween.Play().OnComplete(() => IsMoving = false);
                currentMoveCount--;
            };
        }
    }
}