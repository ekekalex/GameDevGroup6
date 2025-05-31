using System;
using UnityEngine;
public class CameraControl : MonoBehaviour
{
    //Target character
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -6);

    //Orbit rotation
    public float mouseSensitivity = 1.5f;
    public float pitch = 20f;
    public float yaw = 0f;
    public float minPitch = -20f;
    public float maxPitch = 60f;
    //Zoom
    public float currentZoom = 6f;
    private float zoomSpeed = 1f;
    public float minZoom = 3f;
    public float maxZoom = 10f;
    public float followSpeed = 3f;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
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

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);//cam rotate base on mouse input
        Vector3 desiredPosition = target.position + rotation * offset.normalized *currentZoom; //cal new cam position from offset and rotation
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.1f);;
        transform.LookAt(target.position + Vector3.up * 1.3f);
    }

    private void HandleRotationInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
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