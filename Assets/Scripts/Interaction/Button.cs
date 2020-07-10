using System;

using UnityEngine;

namespace IslandPuzzle.Interaction
{
    public class Button : MonoBehaviour, IActivateable
    {
        // Dependencies
        [SerializeField] private Material offMaterial;
        private Material onMaterial;

        // Components 
        private AudioSource buttonClick;

        // Settings 
        [SerializeField] GameObject enablingGameObject;
        private IActivateable enabler;

        // Status
        [SerializeField] bool isEnabled = true;

        // Events
        public event Action<bool> OnActivated = delegate { };

        void Awake()
        {
            if (enablingGameObject == null) return;

            enabler = enablingGameObject.GetComponent<IActivateable>();
            if (enabler == null) return;

            enabler.OnActivated += onEnablerTriggered;

            onMaterial = GetComponent<MeshRenderer>().material;
        }

        void Start()
        {
            // We do this just to change the material to the disabled one.
            if (!isEnabled) Disable();

            buttonClick = GetComponent<AudioSource>();
        }

        public void Activate()
        {
            buttonClick.Play();

            if (isEnabled)
            {
                OnActivated(true);
            }
        }

        public void Enable()
        {
            isEnabled = true;
            GetComponent<MeshRenderer>().material = onMaterial;
        }

        public void Disable()
        {
            isEnabled = false;
            GetComponent<MeshRenderer>().material = offMaterial;
        }

        public void onEnablerTriggered(bool isOn)
        {
            if (isOn) Enable();
            else Disable();
        }
    }
}