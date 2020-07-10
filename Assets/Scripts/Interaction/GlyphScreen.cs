using System;
using System.Collections.Generic;

using UnityEngine;

namespace IslandPuzzle.Interaction
{
    class GlyphScreen : MonoBehaviour
    {
        // Settings 
        [SerializeField] private float glyphSpacing = .5f;
        [SerializeField] private float padding = .25f;

        // Dependencies
        private GameObject glyph1Prefab;
        private GameObject glyph2Prefab;
        private GameObject glyph3Prefab;
        private GameObject glyph4Prefab;
        private GameObject glyph5Prefab;
        private GameObject glyph6Prefab;
        private GameObject glyphDotPrefab;

        void Awake()
        {
            glyph1Prefab = (GameObject)Resources.Load("Glyph1");
            glyph2Prefab = (GameObject)Resources.Load("Glyph2");
            glyph3Prefab = (GameObject)Resources.Load("Glyph3");
            glyph4Prefab = (GameObject)Resources.Load("Glyph4");
            glyph5Prefab = (GameObject)Resources.Load("Glyph5");
            glyph6Prefab = (GameObject)Resources.Load("Glyph6");
            glyphDotPrefab = (GameObject)Resources.Load("GlyphDot");
        }

        public void WriteEmptyGlyphs(int numGlyphs)
        {
            clearGlyphs();

            for (int i = 0; i < numGlyphs; i++)
            {
                writeGlyph(0, i);
            }
        }

        public void WriteGlyphs(List<int> glyphs)
        {
            clearGlyphs();

            for (int i = 0; i < glyphs.Count; i++)
            {
                var glyph = glyphs[i];
                writeGlyph(glyph, i);
            }
        }

        private void clearGlyphs()
        {
            // Destroy all existing glyphs.
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void writeGlyph(int glyph, int glyphPosition)
        {
            GameObject prefabToInstantiate = null;

            switch (glyph)
            {
                case 0:
                    prefabToInstantiate = glyphDotPrefab;
                    break;
                case 1:
                    prefabToInstantiate = glyph1Prefab;
                    break;
                case 2:
                    prefabToInstantiate = glyph2Prefab;
                    break;
                case 3:
                    prefabToInstantiate = glyph3Prefab;
                    break;
                case 4:
                    prefabToInstantiate = glyph4Prefab;
                    break;
                case 5:
                    prefabToInstantiate = glyph5Prefab;
                    break;
                case 6:
                    prefabToInstantiate = glyph6Prefab;
                    break;
            }

            GameObject glyphImage = Instantiate(prefabToInstantiate, transform);

            glyphImage.transform.position += new Vector3(padding + (glyphPosition * glyphSpacing), 0, 0);   
        }
    }
}
