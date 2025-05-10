//update script include the jump/double jump function
//automatically it should set to the spacebar for jump action
using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private float walkSpeed = 4f; //changes depends on the character 
    private float runSpeed = 8f; //changes depends on the character
    private float jumpHeight = 2.5f;
    private int maxJumps = 3; //max height jump (usually double the standard)

    private float gravity = -9.81f; //standard gravity/physics settings
    private CharacterController characterController;
    private Vector3 velocity; 
    private int jumpCount;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovement() //movement based on the player's input (arrows key and WASD)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        float currentSpeed = IsRunning() ? runSpeed : walkSpeed;
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
    private void HandleJump()
    {
        if (characterController.isGrounded)
        {
            jumpCount = 0;
            if (velocity.y < 0f) // grounded state
            velocity.y = -2f;
        }
        else
        {
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps) //when spacebar is pressed, character can jump or double jump
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //-2f so that it stay to gravity level 
                jumpCount++;
            }
        }
    }
    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; 
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }
    private bool IsRunning() //if true then the character would run if the key is pressed 
                             // if not then false, character isnt moving
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}