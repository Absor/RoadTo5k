using UnityEngine;
using System.Collections;

public class ComputerMonitorScript : OnPointerClickCameraMoveScript
{

    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        CameraManagerScript.Instance.OnCameraMove.AddListener(cameraMoved);
    }

    private void cameraMoved(CameraPositionIdentifier newCameraPositionIdentifier)
    {
        if (newCameraPositionIdentifier == CameraPositionIdentifier.Computer)
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
    }
}
