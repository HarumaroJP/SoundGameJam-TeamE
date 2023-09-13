using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameoverController gameoverController;

    public int score_sum = 0;

    public void AddScore(int score)
    {
        if (gameoverController.IsGameOver)
            return;

        score_sum += score;
        scoreText.text = score_sum.ToString("d5");
    }
}