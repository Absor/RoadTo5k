using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public enum CameraPositionIdentifier
{
    Room,
    Computer
}

public class CameraManagerScript : Singleton<CameraManagerScript> {

    public CameraPositionIdentifier startPositionIdentifier;
    public Transform roomCameraPosition;
    public Transform computerCameraPosition;

    public class CameraMoveEvent : UnityEvent<CameraPositionIdentifier> {}
    public CameraMoveEvent OnCameraMove = new CameraMoveEvent();

    void Start()
    {
        MoveCamera(startPositionIdentifier, 0);
    }

    public void MoveCamera(CameraPositionIdentifier newPositionIdentifier, float transitionDuration)
    {
        Transform newPosition = null;
        switch (newPositionIdentifier)
        {
            case CameraPositionIdentifier.Room:
                newPosition = roomCameraPosition;
                break;
            case CameraPositionIdentifier.Computer:
                newPosition = computerCameraPosition;
                break;
        }
        if (transitioning || newPosition == null || newPosition == cameraPosition)
        {
            return;
        }
        StopCoroutine("TransitionCamera");
        this.transitionDuration = transitionDuration;
        this.cameraPosition = newPosition;
        transitioning = true;
        OnCameraMove.Invoke(newPositionIdentifier);
        StartCoroutine("TransitionCamera");
    }

    // Transition handler
    private Transform cameraPosition;
    private float transitionDuration;
    private bool transitioning;

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
        transitioning = false;
    }
}