using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSprite : MonoBehaviour
{
    [SerializeField] private AudioSource EndSpriteSoundEffect;
    [SerializeField] Animator anim;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetTrigger("fall");
            EndSpriteSoundEffect.Play();
            
        }
    }
}


