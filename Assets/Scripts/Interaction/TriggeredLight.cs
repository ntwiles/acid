using System;
using System.Collections.Generic;

using UnityEngine;

namespace IslandPuzzle.Interaction
{
    class TriggeredLight : MonoBehaviour
    {
        // TODO: Find a better way to to this. Currently, we use a GameObject because you can not
        // ask for an IActivateable in the inspector.

        // Settings
        [SerializeField] Material materialOn = null;
        [SerializeField] GameObject triggeringGameObject = null;
        private ITriggerable triggerer;

        [SerializeField] private Light light = null;
        [SerializeField] private MeshRenderer lightBulb;

        void Awake()
        {
            triggerer = triggeringGameObject.GetComponent<ITriggerable>();
            if (triggerer == null) return;

            triggerer.OnTriggered += onTriggererFired;
        }

        public void onTriggererFired(bool isOn)
        {
            light.enabled = true;
            lightBulb.material = materialOn;
        }
    }
}
