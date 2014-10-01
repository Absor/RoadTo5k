using UnityEngine;
using System.Collections;

public class TooltipScript : MonoBehaviour {

    public string tooltipText;
    public TooltipManagerScript tooltipManagerScript;

    public void ShowTooltip()
    {
            tooltipManagerScript.ShowTooltip(tooltipText);
    }

    public void HideTooltip()
    {
        tooltipManagerScript.HideToolTip();
    }
}
