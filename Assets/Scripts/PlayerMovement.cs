using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

            Debug.Log($"Screen position: {screenPosition}");

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPosition.z = transform.position.z;

            Debug.Log($"World position: {worldPosition}");

            transform.LookAt(worldPosition, Vector3.back);
        }
    }
}
