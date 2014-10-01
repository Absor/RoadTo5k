using UnityEngine;
using System.Collections;

public class TooltipScript : MonoBehaviour {

    public string tooltipText;
    public TooltipManagerScript tooltipManagerScript;
    public Transform showCameraPosition;
    public CameraScript cameraScript;

    public void ShowTooltip()
    {
        if (cameraScript.GetCameraPosition() == showCameraPosition) {
            tooltipManagerScript.ShowTooltip(tooltipText);
        }
    }

    public void HideTooltip()
    {
        tooltipManagerScript.HideToolTip();
    }
}
