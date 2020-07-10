
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using IslandPuzzle.Core;
using IslandPuzzle.Interaction;


namespace IslandPuzzle.SceneManagement
{
    public class Teleporter : MonoBehaviour
    {
        // Dependencies
        [SerializeField] GameObject triggeringGameObject = null;
        private ITriggerable triggerer;

        // Settings
        [SerializeField] private float waitBeforeFading, fadeOutTime, fadeWaitTime, fadeInTime;

        // Status
        private int sceneToLoad;

        void Awake()
        {
            triggerer = triggeringGameObject.GetComponent<ITriggerable>();
            if (triggerer == null) return;
        }

        void Start()
        {
            var health = GameObject.FindWithTag("Player").GetComponent<Health>();
            var oxygen = GameObject.FindWithTag("Player").GetComponent<Oxygen>();

            health.OnKilled += onKilled;
            oxygen.OnDrowned += onDrowned;

            triggerer.OnTriggered += onGameWon;
        }

        private void onGameWon(bool obj)
        {
            GameObject player = GameObject.FindWithTag("Player");
            DontDestroyOnLoad(player);
            sceneToLoad = 1;
            StartCoroutine(transitionToScene());
        }

        private void onDrowned()
        {
            print("drowned");
            StartCoroutine(transitionToSpawn());
        }

        private void onKilled()
        {
            StartCoroutine(transitionToSpawn());
        }

        private IEnumerator transitionToSpawn()
        {
            Fader fader = FindObjectOfType<Fader>();

            // Wait a while a message is displayed to the player.
            yield return new WaitForSeconds(waitBeforeFading);

            // Fade out.
            yield return fader.FadeOut(fadeOutTime);

            // Respawn the player.
            GameObject.FindWithTag("Player").GetComponent<PlayerSpawner>().Respawn();

            // Wait on blank screen for n seconds to let camera and player stabilize.
            yield return new WaitForSeconds(fadeWaitTime);

            // Fade back in
            yield return fader.FadeIn(fadeInTime);
        }

        private IEnumerator transitionToScene()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            // Wait a while a message is displayed to the player.
            yield return new WaitForSeconds(waitBeforeFading);

            // Fade out.
            yield return fader.FadeOut(fadeOutTime);

            // Load new scene.
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // Wait on blank screen for n seconds to let camera and player stabilize.
            yield return new WaitForSeconds(fadeWaitTime);

            // Fade back in
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

    }
}
