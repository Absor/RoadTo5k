using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    /**
     * General view to send ComputerGameStateIdentifier forward.
     * ChangeComputerGameState function can be bound e.g. to buttons.
     **/
    public class ComputerGameStateChangeView : View
    {
        public ComputerGameStateIdentifier newComputerGameStateIdentifier;

        public Signal<ComputerGameStateIdentifier> changeComputerGameStateSignal = new Signal<ComputerGameStateIdentifier>();

        public void ChangeComputerGameState()
        {
            changeComputerGameStateSignal.Dispatch(newComputerGameStateIdentifier);
        }
    }
}
