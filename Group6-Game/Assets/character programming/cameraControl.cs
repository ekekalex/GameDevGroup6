using System;
using UnityEngine;
public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;
    public float mouseSensitivity = 3f;
    public float rotationDampending = 5f;
    public bool allowRotation = true;
    public float yaw = 0f;
    public float pitch = 20f;
    public float minPitch = -20f;
    public float maxPitch = 80f;

    public float scrollSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    private float currentZoom;

    private void Start()
    {
        currentZoom = offset.magnitude;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        HandleRotation();
        HandleZoom();
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0, 0, -currentZoom);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target.position + Vector3.up * offset.y);
    }

    private void HandleRotation()
    {
        if (!allowRotation)
        {
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * scrollSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}