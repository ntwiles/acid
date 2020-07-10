
using System;
using System.Collections.Generic;

using UnityEngine;

using IslandPuzzle.Interaction;

namespace IslandPuzzle.IslandScene
{
    class ColorPuzzleButtonListener : MonoBehaviour, ITriggerable
    {
        // Dependencies 
        [SerializeField] private Button greenButton, orangeButton, blueButton, yellowButton;
        [SerializeField] private List<GlyphScreen> glyphScreens = null;
        [SerializeField] private MeshRenderer mainCylinder = null;
        [SerializeField] private Material glowMaterial;

        // Components
        private AudioSource engineHum;

        // Status
        private int[] pressedButtons;
        private int numPressedButtons = 0;
        private bool isSolved = false;

        // Settings
        [SerializeField] private int[] solution;

        // Events
        public event Action<bool> OnTriggered = delegate { };

        void Awake()
        {
            greenButton.OnActivated += onPressGreen;
            orangeButton.OnActivated += onPressOrange;
            blueButton.OnActivated += onPressBlue;
            yellowButton.OnActivated += onPressYellow;

            pressedButtons = new int[solution.Length];

            engineHum = GetComponent<AudioSource>();
        }

        private void handleButtonPress(int buttonIndex)
        {
            if (isSolved) return;

            pressedButtons[numPressedButtons] = buttonIndex;
            numPressedButtons++;

            foreach (GlyphScreen glyphScreen in glyphScreens)
            {
                glyphScreen.WriteEmptyGlyphs(numPressedButtons);
            }

            checkSolved();
        }

        private void onPressGreen(bool obj)
        {
            handleButtonPress(1);
        }

        private void onPressOrange(bool obj)
        {
            handleButtonPress(2);
        }

        private void onPressBlue(bool obj)
        {
            handleButtonPress(3);
        }

        private void onPressYellow(bool obj)
        {
            handleButtonPress(4);
        }

        private void checkSolved()
        {
            if (numPressedButtons >= solution.Length)
            {
                for (int i = 0; i < numPressedButtons; i++)
                {
                    if (pressedButtons[i] != solution[i])
                    {
                        handleWrongSolution();
                        return;
                    }
                }

                handleSolved();
            }
        }

        private void handleSolved()
        {
            isSolved = true;
            engineHum.Play();
            mainCylinder.material = glowMaterial;
            OnTriggered(true);
        }

        private void handleWrongSolution()
        {
            numPressedButtons = 0;
            pressedButtons = new int[solution.Length];
        }
    }
}
