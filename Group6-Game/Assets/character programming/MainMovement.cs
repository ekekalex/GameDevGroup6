//update script include the jump/double jump function
//automatically it should set to the spacebar for jump action
using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f; //changes depends on the character 
    public float runSpeed = 8f; //changes depends on the character
    public float jumpHeight = 1f;
    public int maxJumps = 10;
    public float gravity = -10f; //standard gravity/physics settings
                                 //updating features to include the Cricket Hop ability.
    public bool hasCricketPower = false;
    public float cricketHopMultiplier = 0.4f;
    public float tripleJumpMultiplier = 1.5f;
    private CharacterController characterController;
    public float speed = 6f;
    private Vector3 velocity;
    public CameraControl cameraControl;
    private int jumpCount;
    private bool isAlive = true;
    private void Start()
    {
    }
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (!isAlive) return;   //heart health tracking 
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovement() //movement based on the player's input (arrows key and WASD)
    {
        float a = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(a, 0, b).normalized;
        if (inputDir.magnitude >= 0.1f)
        {
            Quaternion camYaw = cameraControl.GetYawRotation();
            Vector3 moveDir = camYaw * inputDir;
            float currentSpeed = IsRunning() ? runSpeed : walkSpeed;
            characterController.Move(moveDir * currentSpeed * Time.deltaTime);

            Vector3 lookDirection = new Vector3(moveDir.x, 0f, moveDir.z);
            if (lookDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            }

        }
    }
    private void HandleJump()
    {
        if (characterController.isGrounded && velocity.y < 0f)//resetting
        {
            jumpCount = 0;
            velocity.y = -2f;
        }
        // assigning key accordingly to the correct jump type
        if (Input.GetKeyDown(KeyCode.V) && jumpCount < maxJumps) //triple jump when "V" key is press
        {
            PerformJump(jumpHeight * tripleJumpMultiplier);
            jumpCount++;
            return;
        }

        //testing 
        if (Input.GetKeyDown(KeyCode.C))
        {
            hasCricketPower = true;
            Debug.Log("Cricket hop is activated");
        }


        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            float jumpPower;
            if (jumpCount == 0)
            {
                jumpPower = jumpHeight; //normal jump
            }
            else
            {
                if (hasCricketPower)
                {
                    jumpPower = jumpHeight * cricketHopMultiplier;
                }
                else
                {
                    jumpPower = jumpHeight;
                }
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
    public void DisableMovement()
    {
        isAlive = false;
    }

}