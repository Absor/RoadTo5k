using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PortraitScript : MonoBehaviour {

    private Text text;

	void Awake () {
        text = GetComponentInChildren<Text>();
	}

    public void UpdatePortrait(Hero hero)
    {
        if (hero.heroType != null)
        {
            text.text = hero.heroType.ToString();
        }
        else
        {
            text.text = "";
        }        
    }
}
