using System;
using System.Collections;
using System.Collections.Generic;
using Core.Input;
using UnityEngine;

namespace Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GameObject[] bullets;
        [SerializeField] private AudioClip[] seClips;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private PlayerMovement playerMovement;

        private InputEvent shootEvent;

        public void Shoot(int index)
        {
            if (playerMovement.IsMoving)
                return;

            audioSource.PlayOneShot(seClips[index]);

            Bullet bullet = Instantiate(bullets[index], transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Shoot();
        }
    }
}