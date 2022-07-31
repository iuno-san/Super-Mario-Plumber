using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
   [SerializeField] public int score = 0;
   [SerializeField] public int highScore;
   private float displayScore;
   private float transitionSpeed = 100;
   public GameObject HighScoreUI;

   private void Start()
   {
      HighScoreUI.GetComponent<Text>().text = ("HighScore" + highScore);
   }

   private void Update()
   {
      if (score > highScore)
      {
         highScore = score;
      } 
      displayScore = Mathf.MoveTowards(displayScore, score, transitionSpeed * Time.deltaTime);
   }
}
