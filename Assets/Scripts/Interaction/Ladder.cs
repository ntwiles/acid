
using System;
using System.Collections;

using UnityEngine;

using IslandPuzzle.Movement;
using IslandPuzzle.Controls;

namespace IslandPuzzle.Interaction
{
    class Ladder : MonoBehaviour, IActivateable
    {
        // Events
        public event Action<bool> OnActivated = delegate { };

        // Settings
        // TODO: Shouldn't this be on the player, so it can be the same for every ladder?
        [SerializeField] private float climbSpeed = 3.5f;
        [SerializeField] private float exitPointProximityThreshold = 0.02f;

        // Dependencies
        private Vector3 dismountPositionTop, dismountPositionBottom;
        private Vector3 mountPositionTop, mountPositionBottom;

        void Awake()
        {
            dismountPositionTop = transform.Find("DismountTop").transform.position;
            dismountPositionBottom = transform.Find("DismountBottom").transform.position;
            mountPositionTop = transform.Find("MountTop").transform.position;
            mountPositionBottom = transform.Find("MountBottom").transform.position;
        }

        public void Activate()
        {
            OnActivated(true);
            StartCoroutine(climbLadder());
        }

        private IEnumerator climbLadder()
        {
            // Grab dependencies
            GameObject player = GameObject.FindWithTag("Player");
            PlayerMover playerMover = player.GetComponent<PlayerMover>();

            // Take control of the player.
            playerMover.IsGravityOn = false;
            player.GetComponent<PlayerController>().DisableControl();

            // Check climbing up or down.
            bool isClimbingUp = checkClimbingUp(playerMover);
            Vector3 mountPosition = isClimbingUp ? mountPositionBottom : mountPositionTop;
            Vector3 dismountPosition = isClimbingUp ? dismountPositionTop : dismountPositionBottom;

            // Move to mount position.
            yield return moveToPosition(playerMover, mountPosition);

            // Climb up or down.
            if (isClimbingUp)
                yield return moveToPosition(playerMover, mountPositionTop);
            else yield return moveToPosition(playerMover, mountPositionBottom);

            // Move to the dismount position.
            yield return moveToPosition(playerMover, dismountPosition);

            playerMover.IsGravityOn = true;
            player.GetComponent<PlayerController>().EnableControl();

            yield return null;
        }

        private bool checkClimbingUp(PlayerMover playerMover)
        {
            float distToBottom = Vector3.Distance(playerMover.transform.position, mountPositionBottom);
            float distToTop = Vector3.Distance(playerMover.transform.position, mountPositionTop);

            return distToBottom < distToTop;
        }

        private IEnumerator moveToPosition(PlayerMover playerMover, Vector3 targetPosition)
        {
            float distanceToTarget = Vector3.Distance(playerMover.transform.position, targetPosition);

            while (distanceToTarget > exitPointProximityThreshold)
            {
                Vector3 direction = (targetPosition - playerMover.transform.position).normalized;

                float movementAmount = Mathf.Min(distanceToTarget, climbSpeed * Time.deltaTime);

                var movement = direction * movementAmount;
                playerMover.Move(movement, false);

                distanceToTarget = Vector3.Distance(playerMover.transform.position, targetPosition);

                yield return null;
            }
        }
    }
}
