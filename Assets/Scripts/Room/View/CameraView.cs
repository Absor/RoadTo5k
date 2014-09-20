using strange.extensions.mediation.impl;
using System.Collections;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class CameraView : View
    {
        // Settable from Unity
        public Transform roomCameraPosition;
        public Transform computerCameraPosition;

        IEnumerator Transition(Transform target, float transitionDuration)
        {
            float t = 0.0f;
            Vector3 startingPos = transform.position;
            Quaternion startingRot = transform.rotation;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / transitionDuration);

                transform.position = Vector3.Lerp(startingPos, target.position, t);
                transform.rotation = Quaternion.Lerp(startingRot, target.rotation, t);
                yield return 0;
            }
        }

        public void MoveCamera(CameraIdentifier cameraIdentifier, float transitionDuration)
        {
            switch (cameraIdentifier)
            {
                case CameraIdentifier.ROOM_CAMERA:
                    StartCoroutine(Transition(roomCameraPosition, transitionDuration));
                    break;
                case CameraIdentifier.COMPUTER_CAMERA:
                    StartCoroutine(Transition(computerCameraPosition, transitionDuration));
                    break;
            }
        }
    }
}
