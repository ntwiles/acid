using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IslandPuzzle.Movement;
using IslandPuzzle.Interaction;
using IslandPuzzle.UI;

namespace IslandPuzzle.Controls
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        // Dependencies
        [SerializeField] new private Transform camera;
        [SerializeField] private AimCursor cursor;

        // Components
        private PlayerMover mover;

        // Settings 
        [SerializeField] private float walkSpeed = 3;
        [SerializeField] private float interactDistance = 3;
        [SerializeField] private float ledgeDetectionRayLength = 2;

        // Status 
        private bool controlEnabled = true;

        void Start()
        {
            mover = GetComponent<PlayerMover>();
        }

        void Update()
        {
            if (controlEnabled)
            {
                handleMovement();
                handleInteractables();
                handleMenuControls();
            }
        }

        public void DisableControl()
        {
            mover.Disable();
            controlEnabled = false;
        }

        public void EnableControl()
        {
            mover.Enable();
            controlEnabled = true;
        }

        private void handleMovement()
        {
            float inputHorz = Input.GetAxis("Horizontal");
            float inputVert = Input.GetAxis("Vertical");

            Vector3 movementHorz = transform.right * inputHorz * walkSpeed * Time.deltaTime;
            Vector3 movementVert = transform.forward * inputVert * walkSpeed * Time.deltaTime;

            Vector3 movement = movementHorz + movementVert;

            mover.Move(movement);
        }
        

        private void handleInteractables()
        {
            int layerMask = ~((1 << 8) | (1 << 9));

            if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, interactDistance, layerMask))
            {
                var activateable = hit.transform.GetComponent<IActivateable>();

                if (activateable == null)
                {
                    cursor.SetCursorStyle(CursorStyle.Default);
                    return;
                }

                cursor.SetCursorStyle(CursorStyle.Activateable);

                if (Input.GetMouseButtonDown(0))
                {
                    activateable.Activate();
                }
            }
            else
            {
                cursor.SetCursorStyle(CursorStyle.Default);
            }
        }

        private void handleMenuControls()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
