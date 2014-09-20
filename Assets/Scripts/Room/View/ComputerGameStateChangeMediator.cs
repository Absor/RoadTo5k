using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class ComputerGameStateChangeMediator : Mediator
    {
        [Inject]
        public ComputerGameStateChangeView view { get; set; }

        [Inject]
        public ChangeComputerGameStateSignal computerGameStateSignal { get; set; }

        public override void OnRegister()
        {
            view.changeComputerGameStateSignal.AddListener(onChangeComputerState);
        }

        public override void OnRemove()
        {
            view.changeComputerGameStateSignal.RemoveListener(onChangeComputerState);
        }

        private void onChangeComputerState(ComputerGameStateIdentifier newComputerGameState)
        {
            computerGameStateSignal.Dispatch(newComputerGameState);
        }
    }
}
