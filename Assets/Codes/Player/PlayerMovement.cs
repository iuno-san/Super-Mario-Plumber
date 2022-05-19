using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Paremeters")]
    [SerializeField] private float Speed;
   
    [Header("Components")]
    private Rigidbody2D body;
    private Animator anim;
    private bool isGrounded;

    private void Start()
    { //REFERENCES
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    //PLAYER MOVE
    private void Update()
    {
        PlayerRaycast();
        CheckCeilingRays();

        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * Speed, body.velocity.y);

        //FLIP PLAYER
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            Jump();
        }
        //Animations
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded);

    }
    //JUMP
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, Speed);
        isGrounded = false;
        anim.SetTrigger("jump");
    }

    void PlayerRaycast()
    {
        // KILLING THE ENEMY
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 1f);

        if (hit.collider && hit.collider.tag == "Enemy")
        {
            Rigidbody2D colliderRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();

            body.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            colliderRb.AddForce(Vector2.right * 200);
            colliderRb.gravityScale = 8;
            colliderRb.freezeRotation = false;
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            hit.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
            //Destroy(hit.collider.gameObject);
        }

        if (hit.collider && hit.collider.tag != "Enemy")
        {
            isGrounded = true;
        }

        // Spr�buj zabi� Mario, je�li zderza si� z przeciwnikiem:
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f);
        // hit.collider == null
        // hit.collider = new Collider() { tag = "enemy"; }
        // hit.collider.tag

        //Debug.Log(hitRight.collider?.tag + " - tag zderzenia");

        if (hitRight.collider?.tag == "Enemy" || hitLeft.collider?.tag == "Enemy")
        {
            anim.SetTrigger("died");
        }
    }
    void CheckCeilingRays ()
    {
        return;


        Vector2 originLeft = new Vector2(gameObject.transform.position.x - 0.5f + 0.2f, gameObject.transform.position.y + 1f);
        Vector2 originTop = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1f);
        Vector2 originRight = new Vector2(gameObject.transform.position.x - 0.5f - 0.2f, gameObject.transform.position.y + 1f);

        RaycastHit2D ceilLeft = Physics2D.Raycast(originLeft, Vector2.up, body.velocity.y * Time.deltaTime, LayerMask.GetMask("CoinBlock"));
        RaycastHit2D ceilTop = Physics2D.Raycast(originTop, Vector2.up, body.velocity.y * Time.deltaTime, LayerMask.GetMask("CoinBlock"));
        RaycastHit2D ceilRight = Physics2D.Raycast(originRight, Vector2.up, body.velocity.y * Time.deltaTime, LayerMask.GetMask("CoinBlock"));

        RaycastHit2D hitRay;

        Debug.DrawRay(originTop, Vector2.up * 5f, Color.magenta);

        if (ceilLeft)
        {
            hitRay = ceilLeft;
        }
        else if (ceilTop)
        {
            hitRay = ceilTop;
        }
        else if (ceilRight)
        {
            hitRay = ceilRight;
        } 
        else
        {
            return;
        }

        Debug.Log("Mam hita!");
        Debug.Log(hitRay.collider.tag);

        if (hitRay.collider.tag == "Questionblock")
        {
            hitRay.collider.GetComponent<QuestionBlock>().QuestionBlockBounce();
        }
    }
}
