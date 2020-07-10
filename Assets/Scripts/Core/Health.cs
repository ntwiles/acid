
using System;

using UnityEngine;

namespace IslandPuzzle.Core
{
    public class Health : MonoBehaviour
    {
        // Settings
        [SerializeField] private float maxHealthPoints = 100;

        // Status
        private float healthPoints;
        private bool isDead;

        // Events
        public event Action<float> OnHealthChanged = delegate { };
        public event Action OnDamaged = delegate { };
        public event Action OnKilled = delegate { };

        void Awake()
        {
            healthPoints = maxHealthPoints;
        }

        void Update()
        {
        }

        public void Restore()
        {
            healthPoints = maxHealthPoints;
            isDead = false;
        }

        public void Heal(float points)
        {
            healthPoints = Mathf.Min(healthPoints + points, maxHealthPoints);

            OnHealthChanged(healthPoints / maxHealthPoints);
        }

        public void Damage(float points)
        {
            healthPoints = Mathf.Max(healthPoints - points, 0);

            OnHealthChanged(healthPoints / maxHealthPoints);
            OnDamaged();

            if (healthPoints == 0 && !isDead)
            {
                OnKilled();
                isDead = true;
            }
        }
    }

}