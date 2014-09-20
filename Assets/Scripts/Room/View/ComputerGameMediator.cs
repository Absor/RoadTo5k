using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class ComputerGameMediator : Mediator
    {
        [Inject]
        public ComputerGameView view { get; set; }

        [Inject]
        public ComputerGameStateChangedSignal computerGameStateChangedSignal { get; set; }

        public override void OnRegister()
        {
            computerGameStateChangedSignal.AddListener(activateState);
        }

        public override void OnRemove()
        {
            computerGameStateChangedSignal.RemoveListener(activateState);
        }

        private void activateState(ComputerGameStateIdentifier active)
        {
            view.ActivateState(active);
        }
    }
}
