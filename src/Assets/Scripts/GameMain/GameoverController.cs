using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ResultController Result;
    [SerializeField] private GameOverPlayerChanger gameOverPlayerChanger;
    [SerializeField] private ScoreCounter scoreCounter;

    [NonSerialized] public bool IsGameOver;

    private void Start()
    {
        IsGameOver = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsGameOver)
            return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            IsGameOver = true;
            Result.Result(scoreCounter.score_sum);
            gameOverPlayerChanger.GameOver();
        }
    }
}