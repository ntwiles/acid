using System;
using System.Collections.Generic;

using UnityEngine;

namespace IslandPuzzle.Core
{
    class SeaLevel : MonoBehaviour
    {
        // Dependencies
        private Transform player;
        [SerializeField] private AudioSource underwaterSound;

        // Settings
        [SerializeField] private float deepWaterLevel;

        // Status
        public bool IsUnderwater = false;
        public bool IsInDeepWater = false;

        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        void Update()
        {
            checkIsUnderwater();

            if (IsUnderwater && !underwaterSound.isPlaying) underwaterSound.Play();
            else if (!IsUnderwater && underwaterSound.isPlaying) underwaterSound.Stop();
        }

        void checkIsUnderwater()
        {
            if (player != null)
            {
                IsUnderwater = player.position.y < transform.position.y;

                IsInDeepWater = player.position.y < deepWaterLevel;
            }
        }
    }
}
