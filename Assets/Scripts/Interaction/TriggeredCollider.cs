using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandPuzzle.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class TriggeredCollider : MonoBehaviour
    {
        // Dependencies
        [SerializeField] GameObject triggeringGameObject = null;
        private ITriggerable triggerer;

        // Components
        private Collider collider;

        // Settings 
        [SerializeField] private bool fireOnce = false;

        // State
        private bool hasFired = false;

        void Awake()
        {
            collider = GetComponent<Collider>();
        }

        void Start()
        {
            triggerer = triggeringGameObject.GetComponent<ITriggerable>();
            if (triggerer == null) return;

            triggerer.OnTriggered += onTriggererFired;
        }

        private void onTriggererFired(bool obj)
        {
            if (!fireOnce || !hasFired)
            {
                collider.enabled = !collider.enabled;
                hasFired = true;
            }
        }

        void Update()
        {

        }
    }

}