using System;
using UnityEngine;

public class MovementBall : MonoBehaviour
{
    private float rollForce = 4f;
    private bool allowForwardOnly = true;
    private Transform cameraTransform;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float c = Input.GetAxis("Vertical");
        if (allowForwardOnly && c <= 0f)
        {
            return;
        }

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();
        Vector3 moveDir = camForward * c;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Vector3 torqueDirection = Vector3.Cross(Vector3.up, moveDir);
        rb.AddTorque(torqueDirection * rollForce);
    } 

}