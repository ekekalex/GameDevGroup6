//update script include the jump/double jump function
//automatically it should set to the spacebar for jump action
using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f; //changes depends on the character 
    public float runSpeed = 8f; //changes depends on the character
    public float jumpHeight = 2f;
    public int maxJumps = 10; 
    public float gravity = -10f; //standard gravity/physics settings
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
        if (characterController.isGrounded && velocity.y <0f)
        {
            jumpCount = 0;
            velocity.y = -2f;
        }
        // assigning key accordingly to the correct jump type
        if (Input.GetKeyDown(KeyCode.V) && jumpCount == 2) //triple jump when "V" key is press
        {
            PerformJump(jumpHeight * 1.5f);
            jumpCount++;
            return;
        }
        //check for normal or cricket jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            float jumpPower;
            if (jumpCount == 0)
            {
                jumpPower = jumpHeight; //normal jump
            }
            else if (jumpCount == 1)
            {
                jumpPower = jumpHeight * 0.4f; //cricket hop
            }
            else
            {
                return;
            }
            PerformJump(jumpPower);
            jumpCount++;
        }
    }
    private void PerformJump(float jumpPower)
    {
        velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
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