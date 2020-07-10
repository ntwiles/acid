using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IslandPuzzle.Core;
using IslandPuzzle.Interaction;
using System;

namespace IslandPuzzle.Weather
{
    public class Rain : MonoBehaviour
    {
        // Settings
        [SerializeField] private float damagePerSecond;
        [SerializeField] private float replenishRate = 20;
        [SerializeField] private float silenceSpeed = .2f;

        // Dependencies 
        [SerializeField] SeaLevel seaLevel;
        private Health player;
        [SerializeField] GameObject triggeringGameObject = null;
        private ITriggerable triggerer;

        // Components
        private AudioSource rainSounds;

        // Status 
        private bool particlesEnabled = true;

        void Awake()
        {
            rainSounds = GetComponent<AudioSource>();

            player = GameObject.FindWithTag("Player").GetComponent<Health>();

            triggerer = triggeringGameObject.GetComponent<ITriggerable>();
            triggerer.OnTriggered += onGameWon;
        }

        void Update()
        {
            checkUnderwater();

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            {
                if (hit.transform.tag == "Player")
                {
                    player.Damage(damagePerSecond * Time.deltaTime);
                }
                else
                {
                    player.Heal(replenishRate * Time.deltaTime);
                }
            }
        }

        private void checkUnderwater()
        {
            if (seaLevel.IsUnderwater && particlesEnabled) 
            { 
                disableParticles();
                turnSoundsLow();
                particlesEnabled = false;
            }
            if (!seaLevel.IsUnderwater && !particlesEnabled)
            {
                enableParticles();
                turnSoundsHigh();
                particlesEnabled = true;
            }
        }

        private void turnSoundsHigh()
        {
            rainSounds.volume = 1;
        }

        private void turnSoundsLow()
        {
            rainSounds.volume = .2f;
        }

        private void enableParticles()
        {
            foreach (ParticleSystem system in transform.GetComponentsInChildren<ParticleSystem>())
            {
                system.enableEmission = true;
            }
        }

        private void disableParticles()
        {
            foreach (ParticleSystem system in transform.GetComponentsInChildren<ParticleSystem>())
            {
                system.enableEmission = false;
            }
        }

        private void onGameWon(bool obj)
        {
            damagePerSecond = 0;

            StartCoroutine(silenceRainSounds());

        }

        private IEnumerator silenceRainSounds()
        {
            while (rainSounds.volume > 0)
            {
                rainSounds.volume -= silenceSpeed * Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }

    }

}