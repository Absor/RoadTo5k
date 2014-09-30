using UnityEngine;
using System.Collections;

public class GameStartScript : MonoBehaviour {

    public DialogManagerScript dialogManagerScript;

    public string startDialogText;
    public string startOptionText;

	void Start () {
        Dialog dialog = new Dialog();
        dialog.dialogText = startDialogText;
        DialogOption option = new DialogOption();
        option.optionText = startOptionText;
        dialog.dialogOptions.Add(option);
        dialogManagerScript.ShowDialog(dialog, NoOp);
	}

    private void NoOp(DialogOption option)
    {

    }
}
