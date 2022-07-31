using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool debug = false;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
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
            movingLeft = !movingLeft;
        }
    }


    private void Patrol()
    {
        if(debug)
            Debug.Log(movingLeft+", "+leftEdge+", "+rightEdge+", "+transform.position.x);

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(
                    transform.position.x - (speed * Time.deltaTime),
                    transform.position.y,
                    transform.position.z
                );
            }
            else
                movingLeft = false;
        
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(
                    transform.position.x + (speed * Time.deltaTime),
                    transform.position.y,
                    transform.position.z
                );
                
            }
            else
                movingLeft = true;
        }
        
        if (movingLeft)
        {
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

}