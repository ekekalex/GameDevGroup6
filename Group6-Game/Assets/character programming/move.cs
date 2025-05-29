using System;
using UnityEngine;
public class SimplePlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f; //changes depends on the character 
    public float runSpeed = 8f; //changes depends on the character
    public float gravity = -9.81f; //standard gravity/physics settings
    private CharacterController characterController;
    private Vector3 velocity; 

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        HandleMovement();
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