using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer tailRenderer;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private float moveSpace;
    [SerializeField] private float deathDuration;
    [SerializeField] private int Player_score;
    [SerializeField] private int type;

    public int Length;
    public event Action OnDestroyed;

    public void Move()
    {
        transform.position += -transform.right * moveSpace;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        ScoreCounter counter = GameObject.Find("Score").GetComponent<ScoreCounter>();

        if (other.CompareTag("Bullet"))
        {
            //当たったときの処理を書く
            //スコアを加算する

            Bullet bullet = other.GetComponent<Bullet>();

            if (type != bullet.Type)
            {
                bullet.Error();
                return;
            }

            counter.AddScore(Player_score);

            Destroy(other.gameObject);

            
            bodyCollider.enabled = false;
            spriteRenderer.sprite = deathSprite;
            spriteRenderer.DOFade(0f, deathDuration).Play();
            tailRenderer.DOFade(0f, deathDuration).Play();
            Destroy(gameObject, deathDuration);

            OnDestroyed?.Invoke();
        }
    }
}