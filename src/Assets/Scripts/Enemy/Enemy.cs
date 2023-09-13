using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpace;
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

            Destroy(gameObject);
            OnDestroyed?.Invoke();
        }
    }
}