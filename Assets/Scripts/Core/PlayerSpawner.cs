using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IslandPuzzle.Controls;

namespace IslandPuzzle.Core
{
    public class PlayerSpawner : MonoBehaviour
    {
        private Vector3 spawnPosition;
        private Quaternion spawnRotation;

        void Awake()
        {
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;
        }

        public void Respawn()
        {
            GetComponent<PlayerController>().DisableControl();

            transform.position = spawnPosition;
            transform.rotation = spawnRotation;

            GetComponent<Health>().Restore();
            GetComponent<Oxygen>().Restore();

            GetComponent<PlayerController>().EnableControl();
        }
    }

}