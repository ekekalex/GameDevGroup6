using System;
using UnityEngine;
public class CameraControl : MonoBehaviour
{
    //Target character
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -6f);

    //Orbit rotation
    public float mouseSensitivity = 2f;
    public float pitch = 20f;
    public float yaw = 0f;
    public float minPitch = -20f;
    public float maxPitch = 60f;
    //Zoom
    public float currentZoom = 6f;
    private float zoomSpeed = 1f;
    public float minZoom = 3f;
    public float maxZoom = 10f;
    private Vector3 currentVelocity;
    private Vector3 finalPosition;
    private Quaternion finalRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yaw = target.eulerAngles.y;
        finalPosition = transform.position;
        finalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        HandleRotationInput();
        HandleZoom();

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);//cam rotate base on mouse input
        Vector3 desiredPosition = target.position + rotation * offset.normalized * currentZoom; //cal new cam position from offset and rotation
        finalPosition = Vector3.SmoothDamp(finalPosition, desiredPosition, ref currentVelocity, 0.1f);
        finalRotation = Quaternion.LookRotation(target.position + Vector3.up * 1.3f - finalPosition);
        transform.position = finalPosition;
        transform.rotation = finalRotation;
    }

    private void HandleRotationInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            yaw += mouseX * mouseSensitivity;
            pitch -= mouseY * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    public Quaternion GetYawRotation()
    {
        return Quaternion.Euler(0f, yaw, 0f);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}