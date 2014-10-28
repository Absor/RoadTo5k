using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameEndScript : Singleton<GameEndScript> {

    public Text endText;
    public GameObject endPanel;

	void Start () {
        endPanel.SetActive(false);
	}

    public void ShowEndText(string text)
    {
        endPanel.SetActive(true);
        endText.text = text;
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.LoadLevel(Application.loadedLevelName);
    }
}
