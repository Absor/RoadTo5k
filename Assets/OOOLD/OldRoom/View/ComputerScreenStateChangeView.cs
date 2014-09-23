using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    /**
     * General view to send ScreenStateIdentifier forward.
     * ChangeScreenState function can be bound e.g. to buttons.
     **/
    public class ComputerScreenStateChangeView : View
    {
        public ComputerScreenStateIdentifier newComputerScreenStateIdentifier;

        public Signal<ComputerScreenStateIdentifier> changeComputerScreenStateSignal = new Signal<ComputerScreenStateIdentifier>();

        public void ChangeComputerScreenState()
        {
            changeComputerScreenStateSignal.Dispatch(newComputerScreenStateIdentifier);
        }
    }
}
