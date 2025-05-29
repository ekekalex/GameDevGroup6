using System;
using UnityEngine;

public class MovementBall : MonoBehaviour
{
    private float rollForce = 1f;
    private float maxSpeed = 2f;

    private bool allowTurning = true;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float turnInput = allowTurning ? Input.GetAxis("Horizontal") : 0f;
        if (forwardInput < 0f)
        {
            forwardInput = 0f;
        }
        Vector3 moveDirection = new Vector3(turnInput, 0f, forwardInput);
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            Vector3 torque = Vector3.Cross(Vector3.up, moveDirection.normalized) * rollForce;
            rb.AddTorque(torque, ForceMode.Force);
        }
    } 

}