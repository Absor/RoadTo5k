using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class CameraMoveMediator : Mediator
    {
        [Inject]
        public CameraMoveView view { get; set; }

        [Inject]
        public MoveCameraSignal moveCameraSignal { get; set; }

        public override void OnRegister()
        {
            view.moveCameraSignal.AddListener(onMoveCamera);
        }

        public override void OnRemove()
        {
            view.moveCameraSignal.RemoveListener(onMoveCamera);
        }

        private void onMoveCamera(CameraIdentifier newCameraIdentifier, float transitionDuration)
        {
            moveCameraSignal.Dispatch(newCameraIdentifier, transitionDuration);
        }
    }
}
