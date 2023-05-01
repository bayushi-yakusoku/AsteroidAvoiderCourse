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

    private new Rigidbody rigidbody;
    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchScreenManager();

        ScreenWrapAround();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);

    }

    private void TouchScreenManager()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector3 screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            // Due to issue while using Unity Editor UI:
            if (float.IsInfinity(screenPosition.x) || float.IsInfinity(screenPosition.y))
            {
                Debug.Log("Infinity detected!");
                return;
            }

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPosition.z = transform.position.z;

            //transform.LookAt(worldPosition, Vector3.back);
            movementDirection = transform.position - worldPosition;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void ScreenWrapAround()
    {
        Vector3 newPosition = transform.position;

        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0 || viewportPosition.x > 1)
            newPosition.x *= -1;

        if (viewportPosition.y < 0 || viewportPosition.y > 1)
            newPosition.y *= -1;

        transform.position = newPosition;
    }

}
