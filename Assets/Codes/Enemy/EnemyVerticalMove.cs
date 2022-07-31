using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalMove : MonoBehaviour
{
    public bool debug = false;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingDown;
    private float downEdge;
    private float upEdge;

    private void Awake()
    {
        downEdge = transform.position.y - movementDistance;
        upEdge = transform.position.y + movementDistance;
    }
    private void Update()
    {
        Patrol();
        //die();
        
    }

    private void Patrol()
    {
        if (movingDown)
        {
            if (transform.position.y > downEdge)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y - (speed * Time.deltaTime),
                    transform.position.z
                );
            }
            else
                movingDown = false;
        
        }
        else
        {
            if (transform.position.y < upEdge)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + (speed * Time.deltaTime),
                    transform.position.z
                );
                
            }
            else
                movingDown = true;
        }
    }
}