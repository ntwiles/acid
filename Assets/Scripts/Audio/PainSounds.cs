using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IslandPuzzle.Core;
using System;

namespace IslandPuzzle.Audio
{
    [RequireComponent(typeof(Health), typeof(AudioSource))]
    public class PainSounds : MonoBehaviour
    {
        // Components 
        private AudioSource painSoundSource;

        List<AudioClip> painClips;

        // Settings
        [SerializeField] private float timeToPlayAfterLastDamage = .5f;
        [SerializeField] private float timeBetweenGrunts = 4f;

        // Status
        private float timeSinceLastDamage = 0;
        private bool isPlaying = false;
        

        void Awake()
        {
            painSoundSource = GetComponent<AudioSource>();

            loadPainClips();
        }

        void Start()
        {
            var health = GetComponent<Health>();
            health.OnDamaged += onDamaged;
        }

        void Update()
        {
            timeSinceLastDamage += Time.deltaTime;
        }

        private void loadPainClips()
        {
            painClips = new List<AudioClip>();

            for (int i = 12; i < 37; i++)
            {
                painClips.Add(Resources.Load<AudioClip>("Sounds/Pain/Male Pain "+i));
            }
        }

        private void onDamaged()
        {
            timeSinceLastDamage = 0;

            if (!isPlaying) StartCoroutine(playPainSounds());
        }

        private IEnumerator playPainSounds()
        {
            isPlaying = true;

            while (timeSinceLastDamage < timeToPlayAfterLastDamage)
            {
                int audioClipIndex = UnityEngine.Random.Range(0, 24);
                painSoundSource.clip = painClips[audioClipIndex];
                painSoundSource.Play();

                yield return new WaitForSeconds(timeBetweenGrunts);
            }

            isPlaying = false;
        }
    }
}
