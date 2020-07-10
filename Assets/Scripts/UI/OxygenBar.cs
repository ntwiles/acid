
using UnityEngine;
using UnityEngine.UI;

using IslandPuzzle.Core; 

namespace IslandPuzzle.UI
{
    public class OxygenBar : MonoBehaviour
    {
        // Components
        private Image oxygenBar;

        void Start()
        {
            oxygenBar = GetComponent<Image>();

            GameObject.FindWithTag("Player").GetComponent<Oxygen>().OnOxygenChanged += onPlayerOxygenChanged;
        }

        void onPlayerOxygenChanged(float percentFull)
        {
            oxygenBar.fillAmount = percentFull;
        }
    }

}