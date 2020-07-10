using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

using IslandPuzzle.Core;

namespace IslandPuzzle.UI
{
    public class GameOverMessage : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;

        void Awake()
        {
            var health = GameObject.FindWithTag("Player").GetComponent<Health>();
            var oxygen = GameObject.FindWithTag("Player").GetComponent<Oxygen>();

            health.OnKilled += onKilled;
            oxygen.OnDrowned += onDrowned;
        }

        private void onDrowned()
        {
            StartCoroutine(showMessage("YOU DROWNED"));
        }

        private void onKilled()
        {
            StartCoroutine(showMessage("YOU DIED"));
        }

        private IEnumerator showMessage(string message)
        {
            text.text = message;

            yield return new WaitForSeconds(2f);

            text.text = "";
        }
    }
}