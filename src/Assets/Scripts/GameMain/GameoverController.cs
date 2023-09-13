using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ResultController Result;
    [SerializeField] private GameOverPlayerChanger gameOverPlayerChanger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Result.result(0);
            gameOverPlayerChanger.GameOver();
        }
    }
}