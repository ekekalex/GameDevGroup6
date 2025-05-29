using System;
using UnityEngine;
public class CameraControl : MonoBehaviour
{
    //Target character
    public Transform target;
    public Vector3 followOffset = new Vector3(0, 3, -6);
    public float followSpeed = 5f;
    //Orbit rotation
    public float mouseSensitivity = 3f;
    public float minPitch = -20f;
    public float maxPitch = 60f;
    //Zoom
    private float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    //Auto realign
    public bool autoAlign = true;
    public float alignDelay = 0.5f;
    public float alignSpeed = 4f;
    public float yaw = 0f;
    public float pitch = 20f;
    public float currentZoom;
    public float lastInputTime;
    private void Start()
    {
        currentZoom = followOffset.magnitude;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        HandleRotationInput();
        HandleZoom();
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * Vector3.back * currentZoom;
        Vector3 targetPosition = target.position + direction;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);;
        transform.LookAt(target.position + Vector3.up * 1.3f);
    }

    private void HandleRotationInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            lastInputTime = Time.time;
        }
        if (Cursor.lockState == CursorLockMode.Locked)
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
        return Quaternion.Euler(0, yaw, 0);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}