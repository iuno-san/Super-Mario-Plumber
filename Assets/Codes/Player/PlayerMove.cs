using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //HEADER AND COMPONENTS
    [Header("Horizontal Movement")]
    public float moveSpeed;
    public Vector2 direction;

    [Header("Components")]
    public Rigidbody2D body;
    public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;

    [Header("Vertical Movement")]
    public float jumpPower = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Physics")]
    public float maxSpeed;
    public float linerDrag;
    public float gravity;
    public float fallMultiplier;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;

    //UPDATE
    private void Update()
    {
        FlipPlayer();

        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer); ;

        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            jumpTimer = Time.time + jumpDelay;
        }

        animator.SetBool("onGround", onGround);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    //VOID PARAMETERS AND JUMP METHOD
    private void FixedUpdate()
    {
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }
        moveCharacter(direction.x);
        modifyPhysics();
    }
    //MOVE PLAYER
    void moveCharacter(float horizontal)
    {
        body.AddForce(Vector2.right * horizontal * moveSpeed);

        animator.SetFloat("horizontal", Mathf.Abs(body.velocity.x));

        if (Mathf.Abs(body.velocity.x) > maxSpeed)
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);
    }

    //FLIP
    void FlipPlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    //JUMP
    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }


    //FIXED MOVE PLAYER PHYSICS
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && body.velocity.x < 0) || (direction.x < 0 && body.velocity.x > 0);

        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                body.drag = linerDrag;
            }
            else
            {
                body.drag = 0f;
            }
            body.gravityScale = 0;
        }

        else
        {
            body.gravityScale = gravity;
            body.drag = linerDrag * 0.15f;

            if (body.velocity.y < 0)
            {
                body.gravityScale = gravity * fallMultiplier;
            }
            else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.W))
            {
                body.gravityScale = gravity * (fallMultiplier / 2);
            }
        }

    }

    //DRAWS RED LINE THAT SHOWS THE DISTANCE BETWEEN PLAYER AND GROUND 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 orginalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, orginalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t+= Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(orginalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while(t <= 1.0)
        {
            t+= Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, orginalSize, t);
            yield return null;
        }    
    }
}
