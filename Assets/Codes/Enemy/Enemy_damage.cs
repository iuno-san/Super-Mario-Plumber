using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_damage : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("SpikeHead"))
        {
            StartCoroutine(die());
        }
        
        IEnumerator die()
        {
            anim.SetTrigger("died");
            yield return new WaitForSeconds(2);
        }
        
    }

}
