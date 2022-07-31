using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKills : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource browserFallSound;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        anim.SetTrigger("bossDie");
        browserFallSound.Play();
        
    }
}