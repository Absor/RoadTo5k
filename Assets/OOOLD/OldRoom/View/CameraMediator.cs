using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    /**
     * Communicates between CameraView and rest of the program.
     **/
    public class CameraMediator : Mediator
    {
        [Inject]
        public CameraView view { get; set; }

        // Gets this signal if camera move has been accepted by the game logic.
        [Inject]
        public CameraMovedSignal cameraMovedSignal { get; set; }

        public override void OnRegister()
        {
            cameraMovedSignal.AddListener(moveCamera);
        }

        public override void OnRemove()
        {
            cameraMovedSignal.RemoveListener(moveCamera);
        }

        private void moveCamera(CameraIdentifier cameraIdentifier, float transitionDuration)
        {
            view.MoveCamera(cameraIdentifier, transitionDuration);
        }
    }
}
