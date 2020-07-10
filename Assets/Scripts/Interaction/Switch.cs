
using System;

using UnityEngine;

namespace IslandPuzzle.Interaction
{
    public class Switch : MonoBehaviour, IActivateable
    {
        // Dependencies
        [SerializeField] Transform lever;

        // Settings 
        [SerializeField] GameObject enablingGameObject;
        [SerializeField] private bool flipOnlyOnce = false;

        // Components 
        private AudioSource buttonClick;

        // Events
        public event Action<bool> OnActivated;

        // Status
        [SerializeField] private bool isEnabled;
        [SerializeField] private bool setting = false;
        private bool hasFlipped = false;

        void Awake()
        {
            if (enablingGameObject == null) return;

            var activator = enablingGameObject.GetComponent<IActivateable>();
            var triggerer = enablingGameObject.GetComponent<ITriggerable>();

            if (activator != null)
                activator.OnActivated += onEnablerTriggered;
            else if (triggerer != null)
                triggerer.OnTriggered += onEnablerTriggered;

            buttonClick = GetComponent<AudioSource>();
        }

        public void Activate()
        {
            buttonClick.Play();

            // Has this flipped? If so, we need to check if it's allowed
            // to flip again.
            bool canFlip = !hasFlipped || !flipOnlyOnce;

            if (isEnabled && canFlip)
            {
                hasFlipped = true;
                setting = !setting;
                OnActivated(setting);
            }

        }

        public void onEnablerTriggered(bool isOn)
        {
            isEnabled = isOn;
        }
    }
}
