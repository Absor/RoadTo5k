using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class CameraMediator : Mediator
    {
        [Inject]
        public CameraView view { get; set; }

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
