using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private GameData currentGameData;
    
    private void Awake()
    {
        currentGameData = GameState.LoadGameSave();

        GameState.SaveGame(0, 3, currentGameData.highScore);
    }

    private void Start()
    {
        scoreText.text = "SCORE: " + currentGameData.currentScore;
    }
}
