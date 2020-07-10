
using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandPuzzle.Interaction
{
    // Objects which can be clicked on to activate.
    public interface IActivateable
    {
        void Activate();
        event Action<bool> OnActivated;
    }
}
