using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputSystem : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D body;
    private float Speed = 3f;
    private float PlayerMove;
    private Animator anim;

    public void onMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        PlayerMove = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(PlayerMove * Speed, body.velocity.y);
    }

    public void onJumpInput(InputAction.CallbackContext context)
    {
       
    }

    private void OnEnable()
    {
        
    }
}
