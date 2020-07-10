
using UnityEngine;
using UnityEngine.UI;

using IslandPuzzle.Core;

namespace IslandPuzzle.UI
{
    public class HealthBar : MonoBehaviour
    {
        // Components
        private Image healthBar;

        void Start()
        {
            healthBar = GetComponent<Image>();

            GameObject.FindWithTag("Player").GetComponent<Health>().OnHealthChanged += onPlayerHealthChanged;
        }

        void onPlayerHealthChanged(float percentFull)
        {
            healthBar.fillAmount = percentFull;
        }
    }

}