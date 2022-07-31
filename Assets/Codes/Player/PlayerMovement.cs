using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Paremeters")]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpPower;

    [Header("Components")]
    private Rigidbody2D body;
    private Animator anim;
    private float moveHorizontal;
    private bool isGrounded = false;

    [Header("Sound and SFX")]
    [SerializeField] AudioSource jumpSoundEffect;
    //[SerializeField] AudioSource coinSoundEffect;

    private void Start()
    {
        CheckCeilingRays();
    }

    private void Awake()
    { //REFERENCES
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    //PLAYER MOVE
    private void Update()
    {
        PlayerRaycast();

        // if (Input.GetButtonDown("Jump"))

        moveHorizontal = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveHorizontal * Speed, body.velocity.y);
        
        //FLIP PLAYER
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();
        if(Input.GetKeyDown(KeyCode.JoystickButton0) && isGrounded)
            Jump();
        //ADJUSTABLE JUMP HEIGHT
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 3);
        
        if (Input.GetKeyUp(KeyCode.JoystickButton0) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 3);
        
        //Animations
        anim.SetBool("run", moveHorizontal != 0);
        anim.SetBool("grounded", isGrounded);

    }
    //JUMP
    public void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, JumpPower);
        isGrounded = false;
        anim.SetTrigger("jump");
        jumpSoundEffect.Play();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            isGrounded = true;
    }
    
    

    // ReSharper disable Unity.PerformanceAnalysis
    void PlayerRaycast()
    {
        // KILLING THE ENEMY
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 1f);

        if (hit.collider && hit.collider.tag == "Enemy")
        {
            Rigidbody2D colliderRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();

            body.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            colliderRb.AddForce(Vector2.down * 100);
            colliderRb.gravityScale = 8;
            colliderRb.freezeRotation = false;
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            hit.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
            //Destroy(hit.collider.gameObject);
        }

        /* if (hit.collider && hit.collider.tag != "Enemy")
        {
            isGrounded = true;
        }*/

        // Spr buj zabi  Mario, je li zderza si  z przeciwnikiem:
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f);
        // hit.collider == null
        // hit.collider = new Collider() { tag = "enemy"; }
        // hit.collider.tag

        //Debug.Log(hitRight.collider?.tag + " - tag zderzenia");
        //=========================================
        /*if (hitRight.collider?.tag == "Enemy" || hitLeft.collider?.tag == "Enemy")
        {
            anim.SetTrigger("died");
        }*/
    }
    void CheckCeilingRays ()
    {

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
            //coinSoundEffect.Play();
            hitRay.collider.GetComponent<QuestionBlock>().QuestionBlockBounce();
        }
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
            anim.SetTrigger("died");
    }*/

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            var enemy = col.gameObject.GetComponent<EnemyController>();
            enemy.Kill();
        }
    }

    // CONTROLLER INPUT - XBOX GAMEPAD AND OTHER
    public void Move(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<Vector2>().x;
    }

    public void Jumping(InputAction.CallbackContext context)
    {
        if(isGrounded)
         Jump();
    }
}
