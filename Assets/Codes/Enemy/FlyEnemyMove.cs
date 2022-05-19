using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyMove : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    private bool movingUp;
    private float upEdge;
    private float downEdge;

    private void Awake()
    {
        upEdge = transform.position.y + moveDistance;
        downEdge = transform.position.y + moveDistance;
    }
    private void Update()
    {
        Fly();
    }
    private void Fly()
    {
        if(movingUp)
        {
            if (transform.position.y > upEdge)
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y - speed * Time.deltaTime,
                    transform.position.z) ;
        else
        movingUp = false;
        }
        else
        {
            if(transform.position.y < downEdge)
            {

                transform.position = new Vector3 (
                    transform.position.x,
                    transform.position.y + speed * Time.deltaTime,
                    transform.position.z);
            }
        else 
            movingUp = true;
        }
    }

    void die()
    {
        RaycastHit2D hit = gameObject.GetComponent<RaycastHit2D>();
        if (hit.collider.tag == "Player")
        {
            Destroy(hit.collider.gameObject);
        }

    }
}