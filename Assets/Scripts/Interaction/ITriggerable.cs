using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandPuzzle.Interaction
{
    // TODO: We shouldn't use ITriggerable. Instead of IActivatable objects to be ones that can be
    // activate with a click, and ITriggerable ones which can only be triggered via code, do it with
    // IActivatable and IClickable. Every object that can be activated, regardless of whether it's 
    // by click or by code, should be an IActivatable. 
    interface ITriggerable
    {
        event Action<bool> OnTriggered;
    }
}
