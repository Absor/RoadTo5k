using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    // Mediator between ComputerScreenView and rest of the program.
    public class ComputerScreenMediator : Mediator
    {
        [Inject]
        public ComputerScreenView view { get; set; }

        [Inject]
        public ComputerScreenStateChangedSignal computerScreenStateChangedSignal { get; set; }

        public override void OnRegister()
        {
            computerScreenStateChangedSignal.AddListener(activateState);
        }

        public override void OnRemove()
        {
            computerScreenStateChangedSignal.RemoveListener(activateState);
        }

        private void activateState(ComputerScreenStateIdentifier active)
        {
            view.ActivateState(active);
        }
    }
}
