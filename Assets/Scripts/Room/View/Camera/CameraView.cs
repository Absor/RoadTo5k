using strange.extensions.mediation.impl;
using System.Collections;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    // Takes care of everything main camera related.
    public class CameraView : View
    {
        // Settable from Unity
        public Transform roomCameraPosition;
        public Transform computerCameraPosition;

        private Transform cameraPosition;
        private float transitionDuration;

        // Does the camera transition
        IEnumerator TransitionCamera()
        {
            float t = 0.0f;
            Vector3 startingPos = transform.position;
            Quaternion startingRot = transform.rotation;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / transitionDuration);

                transform.position = Vector3.Lerp(startingPos, cameraPosition.position, t);
                transform.rotation = Quaternion.Lerp(startingRot, cameraPosition.rotation, t);
                yield return 0;
            }
        }

        public void MoveCamera(CameraIdentifier cameraIdentifier, float transitionDuration)
        {
            // Stop old transition
            StopCoroutine("TransitionCamera");
            // and start a new one
            this.transitionDuration = transitionDuration;
            switch (cameraIdentifier)
            {
                case CameraIdentifier.ROOM_CAMERA:
                    this.cameraPosition = roomCameraPosition;
                    StartCoroutine("TransitionCamera");
                    break;
                case CameraIdentifier.COMPUTER_CAMERA:
                    this.cameraPosition = computerCameraPosition;
                    StartCoroutine("TransitionCamera");
                    break;
            }
        }
    }
}
