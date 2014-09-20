using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
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
