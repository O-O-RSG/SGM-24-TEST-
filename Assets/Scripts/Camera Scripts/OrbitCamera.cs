using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    public float rotSpeed = 1.5f;
    private float rotY;
    private float rotX;
    private Vector3 offset;
    public float smoothFactor = 0.5f;
    public LayerMask collisionMask;
    public float collisionBuffer = 0.2f;

    void Start()
    {
        rotY = transform.eulerAngles.y;
        rotX = transform.eulerAngles.x;
        offset = target.position - transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * rotSpeed;
        rotX -= mouseY * rotSpeed;

        rotX = Mathf.Clamp(rotX, -90f, 60f);

        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);
        Vector3 desiredPosition = target.position - (rotation * offset);

        // Check if the camera would collide with any objects
        if (Physics.Linecast(target.position, desiredPosition, out RaycastHit hit, collisionMask))
        {
            // If a collision is detected, place the camera at the collision point
            desiredPosition = hit.point + (hit.normal * collisionBuffer);
        }

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothFactor);
        transform.LookAt(target);
    }
}