using System;
using UnityEngine;

public class MovementBall : MonoBehaviour
{
    private float rollForce = 4f;
    private bool allowForwardOnly = true;

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
        Vector3 move = new Vector3(0, 0, 1) * c;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddTorque(move * -1);
    } 

}