using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasicCameraMoveScript : MonoBehaviour {

    public Transform newCameraPosition;
    public CameraScript cameraScript;
    public float transitionDuration;

    public void MoveCamera()
    {
        cameraScript.MoveCamera(newCameraPosition, transitionDuration);
    }
}
