using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 1;

    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("Player died");
            //SceneManager.LoadScene(5);  ładowanie sceny game over
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("AbbysSprite"))
        {
            Debug.Log("-hp");
            
        }
    }
}
