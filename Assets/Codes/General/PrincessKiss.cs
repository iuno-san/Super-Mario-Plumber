using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincessKiss : PlayerScore
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(6);
        
        highScore = highScore > score ? highScore : score;
        
        GameState.SaveGame(score, lives, highScore);
    }
}
