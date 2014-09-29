using UnityEngine;
using System.Collections;

public class ComputerGameScript : MonoBehaviour {

    public GameObject lobbyScreen;
    public GameObject matchScreen;
	public MatchScript matchScript;

    void Awake()
    {
		lobbyScreen.SetActive(true);
		matchScreen.SetActive(false);
    }

    public void StartMatch()
    {
		lobbyScreen.SetActive(false);
		matchScreen.SetActive(true);
		matchScript.StartMatch();
    }

    public void EndMatch()
    {
        // TODO cleanup?
        lobbyScreen.SetActive(true);
        matchScreen.SetActive(false);
    }
}
