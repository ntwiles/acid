using System;
using System.Collections.Generic;

using UnityEngine;

namespace IslandPuzzle.Interaction
{
    class ActivatedLight : MonoBehaviour
    {
        // TODO: Find a better way to to this. Currently, we use a GameObject because you can not
        // ask for an IActivateable in the inspector.

        // Settings
        [SerializeField] Material materialOn = null;
        [SerializeField] GameObject activatingGameObject = null;
        private IActivateable activator;

        [SerializeField] private Light light = null;
        [SerializeField] private MeshRenderer lightBulb;

        void Awake()
        {
            activator = activatingGameObject.GetComponent<IActivateable>();
            if (activator == null) return;

            activator.OnActivated += onActivatorFired;
        }

        public void onActivatorFired(bool isOn)
        {
            light.enabled = true;
            lightBulb.material = materialOn;
        }

    }
}
