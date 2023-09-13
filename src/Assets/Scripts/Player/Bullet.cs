using System.Collections;
using System.Collections.Generic;
using Core.Input;
using GameMain;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private SpriteAnimation spriteAnimation;
        [SerializeField] private Rigidbody2D rig2D;
        [SerializeField] private float power;
        public int Type;

        public void Shoot()
        {
            rig2D.AddForce(transform.right * power, ForceMode2D.Impulse);
        }

        public void Error()
        {
            spriteAnimation.SpriteAnimeStart();
        }
    }
}