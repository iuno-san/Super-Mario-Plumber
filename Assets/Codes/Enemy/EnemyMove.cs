using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
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
        Flip();
        
    }
    void die()
    {
        RaycastHit2D hit = gameObject.GetComponent<RaycastHit2D>();
        if (hit.collider.tag == "Player")
        {
            Destroy(hit.collider.gameObject);
        }

    }

    void Flip()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(
                    transform.position.x - speed * Time.deltaTime,
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
                    transform.position.x + speed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );
            }
            else
                movingLeft = true;

        }
    }
}