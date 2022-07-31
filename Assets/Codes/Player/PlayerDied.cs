using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDied : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource playerDeadSound;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy"))
        {
            StartCoroutine(die());
        }

        if (col.collider.CompareTag("Lava"))
        {
            StartCoroutine(die());
        }

        IEnumerator die()
        {
            anim.SetTrigger("died");
            playerDeadSound.Play();
            yield return new WaitForSeconds(3);
        }
        
    }
}
