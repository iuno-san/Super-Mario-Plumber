using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "AbbysSprite")
        {
            transform.position = respawnPoint;
            anim.SetTrigger("idle");
        }
        else if (col.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
    }
}
