using UnityEngine;
using System.Collections;

public class ShowToolTipOnMouseOverScript : MonoBehaviour {

    public string tooltipText;
    public TooltipManagerScript tooltipManagerScript;
    public Transform showCameraPosition;
    public CameraScript cameraScript;

    void OnMouseEnter()
    {
        if (cameraScript.GetCameraPosition() == showCameraPosition) {
            tooltipManagerScript.ShowTooltip(tooltipText);
        }
    }

    void OnMouseExit()
    {
        tooltipManagerScript.HideToolTip();
    }
}
