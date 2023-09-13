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
        if (other.CompareTag("Enemy"))
        {
            //当たったときの処理を書く
            //スコアを加算する
        }
    }
}