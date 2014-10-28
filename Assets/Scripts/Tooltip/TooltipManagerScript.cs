using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipManagerScript : Singleton<TooltipManagerScript> {

    private Text text;

    void Awake()
    {
        text = gameObject.GetSafeComponentInChildren<Text>();
        HideToolTip();
    }

    public void ShowTooltip(string newText)
    {
        text.text = newText;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void HideToolTip()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
