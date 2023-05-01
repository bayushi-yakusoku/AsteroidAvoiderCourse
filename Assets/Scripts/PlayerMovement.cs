using System.Collections;
using System.Collections.Generic;
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

    private void FixedUpdate()
    {
        rigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);

    }
}
