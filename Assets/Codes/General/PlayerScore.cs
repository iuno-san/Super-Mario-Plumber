using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private AudioSource coinsSoundEffect;
    [SerializeField] private AudioSource MushroomColectSound;
    [SerializeField] private AudioSource FlowerEffectSound;
    [SerializeField] private float timeLeft = 300;
    [SerializeField] public int score = 0;
    [SerializeField] public int highScore = 0;
    [SerializeField] public int lives = 3;
    public bool isGameOver = false;

    public Text TimeLeftUIText;
    public Text ScoreUIText;
    public Text LivesUIText;
    public Text HighScoreUIText;
    

    private void Awake()
    {
        GameData savedData = GameState.LoadGameSave();

        score = savedData.currentScore;
        lives = savedData.currentLives;
        highScore = savedData.highScore;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        //pobierz komponeny tekstu ustaw licznik puntk�w i czasu
        TimeLeftUIText.text = ("TIME " + (int)timeLeft);
        ScoreUIText.text = ("SCORE " + score);
        LivesUIText.text = ("LIVES " + lives);
        HighScoreUIText.text = ("HighScore " + highScore);

        if ((timeLeft < 0.1f || lives < 1) && !isGameOver)
            GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            GameState.SaveGame(score, lives, highScore);
        } 
        
        if (collision.gameObject.name == ("EndSprite"))
        {
            CountScore();
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            AddCoinScore();
            Destroy (collision.gameObject);
            coinsSoundEffect.Play();
        }

        if (collision.CompareTag("MinerSprite"))
        {
            --lives;
        }

        if (collision.CompareTag("Mushroom"))
        {
            ++lives;
            MushroomColectSound.Play();
        }

        if (collision.CompareTag("star"))
        {
            score += 500;
            FlowerEffectSound.Play();
        }

        if (collision.CompareTag("Flower"))
        {
            score += 200;
            FlowerEffectSound.Play();
        }

        if (collision.CompareTag("Enemy"))
        {
            score += 50;
        }
    }

    public void AddCoinScore()
    {
        score += 50;
    }

    void CountScore()
    {
        score = score + (int)(timeLeft * 5);
    }

    void GameOver()
    {
        if (isGameOver)
            return; // Allow to call only once.
        isGameOver = true;

        // Zapisz wynik do pliku:
        highScore = highScore > score ? highScore : score;

        GameState.SaveGame(score, lives, highScore);
        
        Debug.Log("Zapisano grę z wynikiem "+score+";"+highScore);
        
        score = 0;

        lives = 3;
        
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        
    }
    
}
