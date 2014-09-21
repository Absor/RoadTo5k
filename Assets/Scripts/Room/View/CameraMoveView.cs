using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    /**
     * General view to send MoveCameraSignal forward.
     * MoveCamera function can be bound e.g. to buttons.
     **/
    public class CameraMoveView : View
    {
        public float transitionDuration;
        public CameraIdentifier newCameraIdentifier;

        public Signal<CameraIdentifier, float> moveCameraSignal = new Signal<CameraIdentifier, float>();

        public void MoveCamera()
        {
            moveCameraSignal.Dispatch(newCameraIdentifier, transitionDuration);
        }
    }
}
