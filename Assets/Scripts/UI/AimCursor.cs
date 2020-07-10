using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace IslandPuzzle.UI
{
    class AimCursor : MonoBehaviour
    {
        private Image cursor;

        private void Awake()
        {
            cursor = GetComponent<Image>();
        }

        public void SetCursorStyle(CursorStyle style)
        {
            switch (style)
            {
                case CursorStyle.Default:
                    {
                        cursor.color = Color.white;
                        break;
                    }
                case CursorStyle.Activateable:
                    {
                        cursor.color = Color.red;
                        break;
                    }
            }
        }
    }

    public enum CursorStyle
    {
        Default,
        Activateable
    }
}
