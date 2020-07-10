
using System;

using UnityEngine;

namespace IslandPuzzle.Movement
{
    class PlayerMover : MonoBehaviour
    {
        private CharacterController character;

        // Settings
        [SerializeField] private float gravity = 9.8f;

        // Status 
        public bool IsGravityOn = true;

        void Start()
        {
            character = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (IsGravityOn)
            {
                handleGravity();
            }
        }

        public void Move(Vector3 movement, bool useColliders = true)
        {
            if (useColliders)
                character.Move(movement);
            else moveTransform(movement);
        }

        public void Enable()
        {
            character.enabled = true;
        }

        public void Disable()
        {
            character.enabled = false;
        }

        private void moveTransform(Vector3 movement)
        {
            transform.position += movement;
        }

        private void handleGravity()
        {
            if (!character.isGrounded)
            {
                character.Move(Vector3.down * gravity * Time.deltaTime);
            }
        }
    }
}
