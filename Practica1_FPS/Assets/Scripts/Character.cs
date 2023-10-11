using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float mouseXSensitivity = 120f;
    public float mouseYSensitivity = 120f;
    public float velocity = 0.01f;
    Camera camera;
    Rigidbody rb;

    private float rotation;

    void Start(){
        camera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        float xr = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float yr = Input.GetAxis("Mouse Y") * mouseXSensitivity * Time.deltaTime;
        
        rotation -= yr;
        rotation = Mathf.Clamp(rotation, -90, 90);

        transform.Rotate(0, xr, 0);
        camera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);

        Move();
    }

    void Move(){
        float lateralMov = Input.GetAxis("Horizontal");
        float forwardMov = Input.GetAxis("Vertical");

        // Create a vector for movement based on input
        Vector3 inputMovement = new Vector3(lateralMov, 0, forwardMov);

        // Transform the input movement vector from local space to world space
        Vector3 worldMovement = transform.TransformDirection(inputMovement);

        // Set a movement speed factor to control the speed of movement
        float speed = 5f; // Adjust this value to control the movement speed

        // Apply speed to the movement vector
        Vector3 finalMovement = worldMovement * speed * Time.deltaTime;

        // Move the rigidbody using rb.MovePosition
        rb.MovePosition(rb.position + finalMovement);
    }

}