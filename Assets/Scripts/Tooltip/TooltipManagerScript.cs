using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipManagerScript : MonoBehaviour {

    public GameObject panel;
    public Text text;

    void Start()
    {
        HideToolTip();
    }

    public void ShowTooltip(string newText)
    {
        text.text = newText;
        panel.SetActive(true);
    }

    public void HideToolTip()
    {
        panel.SetActive(false);
    }
}
