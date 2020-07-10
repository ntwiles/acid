
using System;
using System.Collections.Generic;

using UnityEngine;

using IslandPuzzle.Interaction;

namespace IslandPuzzle.IslandScene
{
    class LogoPuzzleButtonListener : MonoBehaviour, ITriggerable
    {
        // Dependencies 
        [SerializeField] private Button button1 = null;
        [SerializeField] private Button button2 = null;
        [SerializeField] private Button button3 = null;
        [SerializeField] private Button button4 = null;
        [SerializeField] private Button button5 = null;
        [SerializeField] private Button button6 = null;
        [SerializeField] private Button finishButton = null;
        [SerializeField] private List<GlyphScreen> glyphScreens = null;

        // Status
        [SerializeField] private bool[] buttonData;
        private bool isCorrect = false;

        // Settings
        [SerializeField] private bool[] solution;

        // Events
        public event Action<bool> OnTriggered = delegate { };

        void Awake()
        {
            button1.OnActivated += delegate (bool isOn) { handleButtonPress(1); };
            button2.OnActivated += delegate (bool isOn) { handleButtonPress(2); };
            button3.OnActivated += delegate (bool isOn) { handleButtonPress(3); };
            button4.OnActivated += delegate (bool isOn) { handleButtonPress(4); };
            button5.OnActivated += delegate (bool isOn) { handleButtonPress(5); };
            button6.OnActivated += delegate (bool isOn) { handleButtonPress(6); };

            finishButton.OnActivated += checkSolved;

            buttonData = new bool[6];

            for (int i = 0; i < 6; i++)
            {
                buttonData[i] = false;
            }
        }

        private void handleButtonPress(int buttonIndex)
        {
            buttonData[buttonIndex - 1] = !buttonData[buttonIndex - 1];
            checkCorrect();
            writeToGlyphScreen();
        }

        private void checkCorrect()
        {
            for (int i = 0; i < buttonData.Length; i++)
            {
                bool guess = buttonData[i];
                bool answer = solution[i];

                isCorrect = ((buttonData[i] == true && solution[i] == true) || (buttonData[i] == false && solution[i] == false));

                if (!isCorrect) { return; }
            }

            isCorrect = true;
        }

        private void writeToGlyphScreen()
        {
            List<int> glyphScreenData = new List<int>();

            for (int i = 0; i < buttonData.Length; i++)
            {
                if (buttonData[i] == true)
                {
                    glyphScreenData.Add(i + 1);
                }
            }

            foreach (GlyphScreen glyphScreen in glyphScreens)
            {
                glyphScreen.WriteGlyphs(glyphScreenData);
            }
        }

        private void checkSolved( bool isOn)
        {
            if (isCorrect) handleSolved();
        }

        private void handleSolved()
        {
            button1.Disable();
            button2.Disable();
            button3.Disable();
            button4.Disable();
            button5.Disable();
            button6.Disable();

            finishButton.Disable();

            OnTriggered(true);
        }
    }
}
