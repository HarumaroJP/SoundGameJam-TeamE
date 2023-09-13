using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpace;
    [SerializeField] private int Player_score;

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

            Debug.Log("Bullet");

            counter.AddScore(Player_score);

            Destroy(other.gameObject);

            Destroy(gameObject);

        }
    }    
}