using UnityEngine;
using System.Collections;

public class ComputerGameScript : MonoBehaviour {

    public GameObject lobbyScreen;
    public GameObject matchScreen;
	public MatchScript matchScript;

    void Start()
    {
		lobbyScreen.SetActive(true);
		matchScreen.SetActive(false);
    }

    public void StartMatch()
    {
        if (GameStateManagerScript.Instance.GetGameTime().hour > 21)
        {
            Dialog dialog = new Dialog();
            dialog.dialogText = "You feel too tired for anything";
            DialogOption ok = new DialogOption();
            ok.optionText = "Ok";
            dialog.dialogOptions.Add(ok);
            DialogManagerScript.Instance.ShowDialog(dialog, noOp);
            return;
        }
		lobbyScreen.SetActive(false);
		matchScreen.SetActive(true);
		matchScript.StartMatch();
    }

    private void noOp(DialogOption obj)
    {
    }

    public void EndMatch()
    {
        // TODO cleanup?
        lobbyScreen.SetActive(true);
        matchScreen.SetActive(false);
    }
}
