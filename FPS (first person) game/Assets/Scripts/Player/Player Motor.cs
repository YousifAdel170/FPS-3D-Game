// This file contains all the player movement functionality 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
   
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = -3f;

    public bool crouching = false;
    public float crouchTimer = 1;
    public bool lerpCrouch = false;
    public bool sprinting = false;

    // Start is called before the first frame update
    void Start()
    {
        // assign controller at start
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // to get every single frame 
        isGrounded = controller.isGrounded;


        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);


            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }

        }
    }

    // receive the inputs for our InputManager.cs script & apply them to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;   
        moveDirection.x = input.x;  
        // grab the y component of 2d vector & apply it to z-axis of our character 
        // to translate the vertical movement into forward backward movement
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        
        // to make it not less than -2.0 se we can jump normally because without it it will be so negative that even when jump will appears at ground
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        
        controller.Move(playerVelocity * Time.deltaTime);
        // to see how much force is being applied to the player
        Debug.Log(playerVelocity.y);


    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }


    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }
}
