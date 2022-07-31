using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D[] boxCollider2D;
    private float score;
    [SerializeField] private AudioSource enemyDeadSound;
    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponents<BoxCollider2D>();
    }

    public void Kill()
    {
        ComplexDeath();
    }

    private void ComplexDeath()
    {
        anim.SetTrigger("dead");
        enemyDeadSound.Play();
        

        foreach (var boxCollider2D in boxCollider2D)
        {
            boxCollider2D.enabled = false;
        }
        
        Wait(() =>
        {
            Destroy(gameObject);
        }, 5f);
    }
    
    public void Wait(Action action, float delay)
    {
        StartCoroutine(WaitCoroutine(action, delay));
    }

    IEnumerator WaitCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);

        action();
    }
    
}