using System;

using UnityEngine;

using IslandPuzzle.Interaction;

namespace IslandPuzzle.IslandScene
{
    public class SymbolLight : MonoBehaviour, ITriggerable
    {
        // Dependencies
        [SerializeField] Button toggleButton;

        // Components
        new MeshRenderer renderer;

        // Events
        public event Action<bool> OnTriggered;

        void Start()
        {
            toggleButton.OnActivated += onButtonPressed;

            renderer = GetComponent<MeshRenderer>();
        }

        private void onButtonPressed(bool isOn)
        {
            renderer.enabled = !renderer.enabled;
        }
    }
}


