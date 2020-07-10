using System;

using UnityEngine;

namespace IslandPuzzle.Core
{
    public class Oxygen : MonoBehaviour
    {
        // Dependencies
        [SerializeField] private SeaLevel seaLevel;

        // Settings
        [SerializeField] private float maxOxygenPoints = 100;
        [SerializeField] private float depletionPerSecond = 5;
        [SerializeField] private float replenishPerSecond = 10;

        // Status
        private bool isUnderwater = false;
        private bool isDrowned = false;
        private float oxygenPoints;

        // Events
        public event Action<float> OnOxygenChanged = delegate { };
        public event Action OnDrowned = delegate { };

        void Awake()
        {
            oxygenPoints = maxOxygenPoints;
        }

        void Update()
        {
            if (seaLevel.IsInDeepWater)
            {
                if (!isDrowned)
                {
                    isDrowned = true;
                    OnDrowned();
                }
            }
            else if (seaLevel.IsUnderwater)
            {
                float depletion = depletionPerSecond * Time.deltaTime;
                oxygenPoints = Mathf.Max(oxygenPoints - depletion, 0);

                OnOxygenChanged(oxygenPoints / maxOxygenPoints);

                if (oxygenPoints == 0 && !isDrowned)
                {
                    isDrowned = true;
                    OnDrowned();
                }
            }
            else
            {
                float replenishment = replenishPerSecond * Time.deltaTime;
                oxygenPoints = Mathf.Min(oxygenPoints + replenishment, maxOxygenPoints);

                OnOxygenChanged(oxygenPoints / maxOxygenPoints);
            }
        }

        public void Restore()
        {
            oxygenPoints = maxOxygenPoints;
            isDrowned = false;
        }
    }
}