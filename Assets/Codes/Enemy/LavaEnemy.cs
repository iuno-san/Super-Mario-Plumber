using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LavaEnemy : MonoBehaviour
{
    public bool debug = false;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingUp;
    private float downEdge;
    private float upEdge;

    private void Awake()
    {
        upEdge = transform.position.y - movementDistance;
        downEdge = transform.position.y + movementDistance;
    }
    private void Update()
    {
        Patrol();
        //die();
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null && !col.collider.CompareTag("Player") && col.collider.CompareTag("Ground"))
        {
            movingUp = !movingUp;
        }
    }

    private void Patrol()
    {
        if (movingUp)
        {
            if (transform.position.y > upEdge)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y - (speed * Time.deltaTime),
                    transform.position.z
                );
            }
            else
                movingUp = false;
        
        }
        else
        {
            if (transform.position.y < downEdge)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + (speed * Time.deltaTime),
                    transform.position.z
                );
                
            }
            else
                movingUp = true;
        }

        if (movingUp)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}