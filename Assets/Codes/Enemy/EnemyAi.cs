using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D body;
    private bool facingLeft;

    private void Update()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null && !col.collider.CompareTag("Player") && col.collider.CompareTag("Ground"))
        {
            facingLeft = !facingLeft;
        }

        if (facingLeft)
        {
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
    
    

    private void OnBecameVisible()
    {
        enabled = true;
    }
}
