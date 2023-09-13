using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpace;

    public void Move()
    {
        transform.position += -transform.right * moveSpace;
    }

    public void OnTriggerEnter(Collider other)
    {
        ScoreCounter counter = GameObject.Find("Score").GetComponent<ScoreCounter>();

        if (other.CompareTag("Enemy"))
        {
            //当たったときの処理を書く
            //スコアを加算する

            if (other.gameObject.name == "A")
            {
                counter.AddScore(100);
            }

            if (other.gameObject.name == "B")
            {
                counter.AddScore(200);
            }

            if (other.gameObject.name == "C")
            {
                counter.AddScore(300);
            }

        }

    }

        
}