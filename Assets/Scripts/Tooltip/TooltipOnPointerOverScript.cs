using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TooltipOnPointerOverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string tooltipText;

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManagerScript.Instance.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManagerScript.Instance.ShowTooltip(tooltipText);
    }
}
