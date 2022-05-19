using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [Header ("Paremeters")]
    public int speed = 5;
    private bool fancingRight = false;
    public int JumpPower = 350;
    private float moveX;
    private bool isGrounded;


    [Header ("Component")]
    private Animator anim;

    private void Start()
    { // Pobierz z obiektu referencje dla cia³a sztywnego i animatora | Grab references for rigidbody and animator from object 
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerMove();
        PlayerRaycast();
    }

    void PlayerMove()
    {
        //CONRTOLS
        moveX = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //ANIMATIONS
        anim.SetBool("run", moveX != 0);
        anim.SetBool("grounded", isGrounded);
        

        //PLAYER DIRECTION
        if (moveX < 0.0f && fancingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && fancingRight == true)
        {
            FlipPlayer();
        }
        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPower);
        anim.SetTrigger("jump");
        isGrounded = false;
    }

    void FlipPlayer()
    {
        fancingRight = !fancingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    void PlayerRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit.distance < 0.9f && hit.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            hit.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            hit.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
            //Destroy(hit.collider.gameObject);
        } else if(hit.collider.tag == "Enemy")
        {
            anim.SetTrigger("died");
        }

        if (hit.distance < 0.9f && hit.collider.tag != "Enemy")
            isGrounded = true;
    }

    //private void Die()
    //{
    //    SceneManager.LoadScene("Level-1");
    //}
}
