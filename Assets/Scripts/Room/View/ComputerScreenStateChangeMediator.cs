using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class ComputerScreenStateChangeMediator : Mediator
    {
        [Inject]
        public ComputerScreenStateChangeView view { get; set; }

        [Inject]
        public ChangeComputerScreenStateSignal computerScreenStateSignal { get; set; }

        public override void OnRegister()
        {
            view.changeComputerScreenStateSignal.AddListener(onChangeScreenState);
        }

        public override void OnRemove()
        {
            view.changeComputerScreenStateSignal.RemoveListener(onChangeScreenState);
        }

        private void onChangeScreenState(ComputerScreenStateIdentifier newComputerGameState)
        {
            computerScreenStateSignal.Dispatch(newComputerGameState);
        }
    }
}
