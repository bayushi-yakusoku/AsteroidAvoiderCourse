using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;

    private new Rigidbody rigidbody;
    private Vector3 movementDirection;

    private int roll = 0;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchScreenManager();

        Rotate();

        ScreenWrapAround();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);

    }

    private void TouchScreenManager()
    {
        if (Touchscreen.current is null) return;

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector3 screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            // Due to issue while using Unity Editor UI:
            if (float.IsInfinity(screenPosition.x) || float.IsInfinity(screenPosition.y))
            {
                Debug.Log("Infinity detected!");
                return;
            }

            Debug.Log($"screenPosition: {screenPosition}");

            // Set Z distance from the camera for the world point.
            // Used by method ScreenToWorldPoint:
            screenPosition.z = transform.position.z - cam.transform.position.z;

            Vector3 worldPosition = cam.ScreenToWorldPoint(screenPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
            roll = 0;
        }
    }

    private void ScreenWrapAround()
    {
        Vector3 newPosition = transform.position;

        Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0)
            newPosition.x = -1f * (newPosition.x + 0.1f);
        else if (viewportPosition.x > 1)
            newPosition.x = -1f * (newPosition.x - 0.1f);

        if (viewportPosition.y < 0)
            newPosition.y = -1f * (newPosition.y + 0.1f);
        else if (viewportPosition.y > 1)
            newPosition.y = -1f * (newPosition.y - 0.1f);

        transform.position = newPosition;
    }

    private void Rotate()
    {
        if (movementDirection == Vector3.zero) return;

        Quaternion newRotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(movementDirection, Vector3.back),
                Time.deltaTime * rotationSpeed
            );

        if (newRotation.z > transform.rotation.z)
        {
            roll = -1;
        }
        else if (newRotation.z < transform.rotation.z)
        {
            roll = 1;
        }
        else
        {
            roll = 0;
        }

        transform.rotation = newRotation;
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        Vector2 screenPos = new()
        {
            x = currentEvent.mousePosition.x,
            y = cam.pixelHeight - currentEvent.mousePosition.y
        };

        Vector3 point = cam.ScreenToWorldPoint(
            new Vector3(screenPos.x, 
                        screenPos.y, 
                        transform.position.z - cam.transform.position.z
                        )
            );

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        
        GUILayout.Label("Screen pixels  : " + cam.pixelWidth + "*" + cam.pixelHeight);
        GUILayout.Label("Screen position: " + screenPos);
        GUILayout.Label("World position : " + point.ToString("F3"));

        switch(roll)
        {
            case -1:
                GUILayout.Label("Roll           : Left");
                break;
            case 0:
                GUILayout.Label("Roll           : ");
                break;
            case 1:
                GUILayout.Label("Roll           : Right");
                break;
        }

        GUILayout.EndArea();
    }
}
