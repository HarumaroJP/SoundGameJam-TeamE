using System.Collections;
using System.Collections.Generic;
using Core.Input;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rig2D;
        [SerializeField] private Sprite errorSprite;
        [SerializeField] private float power;
        public int Type;

        public void Shoot()
        {
            rig2D.AddForce(transform.right * power, ForceMode2D.Impulse);
        }

        public void Error()
        {
            spriteRenderer.sprite = errorSprite;
        }
    }
}