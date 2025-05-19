using System;
using UnityEngine;

public class MovementBall : MonoBehaviour
{
    private float rollForce = 10f;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float c = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, c);
        rb.AddForce(move * rollForce);
    } 

}