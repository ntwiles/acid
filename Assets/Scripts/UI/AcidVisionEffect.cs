
using UnityEngine;
using UnityEngine.UI;

using IslandPuzzle.Core;

namespace IslandPuzzle.UI
{
    public class AcidVisionEffect : MonoBehaviour
    {
        // Components
        private Image acidEffect;

        void Start()
        {
            acidEffect = GetComponent<Image>();

            GameObject.FindWithTag("Player").GetComponent<Health>().OnHealthChanged += onPlayerHealthChanged;
        }

        void onPlayerHealthChanged(float percentFull)
        {
            float alpha = Mathf.Min(1 - percentFull, .5f);
            Color oldColor = acidEffect.color;
            acidEffect.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
        }
    }
}
