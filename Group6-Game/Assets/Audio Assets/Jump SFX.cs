using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public AudioSource jumpSound;
    public float raycastDistance = 0.1f;
    public Transform raycastOrigin;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // removed the period after GetKeyDown
        {
            Ray ray = new Ray(raycastOrigin.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.tag == "Untagged" || hit.collider.tag == "Platform")
                {
                    jumpSound.Play();
                }
            }
        }
    }
}
