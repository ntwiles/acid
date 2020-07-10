using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace IslandPuzzle.Controls
{
    public class FirstPersonCamera : MonoBehaviour
    {
        // Settings
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private Transform playerBody = null;

        // State
        private float xRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float moveX = mouseX * mouseSensitivity * Time.deltaTime;
            float moveY = mouseY * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

}