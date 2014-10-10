using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnPointerClickCameraMoveScript : MonoBehaviour, IPointerClickHandler
{
    public CameraPositionIdentifier newCameraPositionIdentifier;
    public float transitionDuration;

    public void OnPointerClick(PointerEventData eventData)
    {
        CameraManagerScript.Instance.MoveCamera(newCameraPositionIdentifier, transitionDuration);
    }
}
