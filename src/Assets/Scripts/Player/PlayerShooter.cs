using System;
using System.Collections;
using System.Collections.Generic;
using Core.Input;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GameObject[] bullets;
        [SerializeField] private AudioClip[] seClips;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private PlayerMovement playerMovement;

        [SerializeField] private float easeDistance;
        [SerializeField] private float easeDuration;

        private InputEvent shootEvent;

        public void Shoot(int index)
        {
            if (playerMovement.IsMoving)
                return;

            audioSource.PlayOneShot(seClips[index]);

            transform.DOLocalMoveX(easeDistance, easeDuration).SetEase(Ease.OutQuad, 2.5f).SetLoops(2, LoopType.Yoyo).Play();

            Bullet bullet = Instantiate(bullets[index], transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Shoot();
        }
    }
}