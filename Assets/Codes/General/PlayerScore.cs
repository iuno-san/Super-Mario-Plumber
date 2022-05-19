using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private float timeLeft = 120;
    [SerializeField] public int Score = 0;


    public GameObject TimeLeftUI;
    public GameObject ScoreUI;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        //pobierz komponeny tekstu ustaw licznik puntków i czasu
        TimeLeftUI.gameObject.GetComponent<Text>().text = ("TIME " + (int)timeLeft);
        ScoreUI.gameObject.GetComponent<Text>().text = ("SCORE " + Score);

        if (timeLeft < 0.1f)
            SceneManager.LoadScene("Level-1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("EndSprite"))
        {
            CountScore();
            DataManagement.instance.SaveData();
        }

        if (collision.gameObject.name == ("Coin"))
        {
            AddCoinScore();
            Destroy (collision.gameObject);
        }
    }

    public void AddCoinScore()
    {
        Score += 50;
    }

    void CountScore()
    {
        Score = Score + (int)(timeLeft * 10);
    }

}
