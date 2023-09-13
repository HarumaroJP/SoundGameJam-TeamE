using System;
using System.Collections;
using System.Collections.Generic;
using GameMain;
using UnityEngine;

public class GameOverPlayerChanger : MonoBehaviour
{
    [SerializeField] private Sprite gameOverSprite;
    [SerializeField] private SpriteAnimation spriteAnimation;

    public void GameOver()
    {
        spriteAnimation.SpriteAnimeStart();
    }
}