using UnityEngine;
using System.Collections;

public class MatchDialogManagerScript : MonoBehaviour {

	public MatchScript matchScript;
	public GameObject dialogPanel;

    private bool dialogResolved;

	void Awake()
	{
		dialogPanel.SetActive(false);
	}

    public IEnumerator PlayDialogStep(MatchState matchState)
	{
        dialogResolved = false;
		dialogPanel.SetActive(true);

        // Wait for player action
        while (!dialogResolved)
        {
            yield return null;
        }
        // Modify match state based on answer?
        matchState.dialogsPlayed += 1;

        dialogPanel.SetActive(false);
        matchScript.PlayNextStep();
	}

	public void ResolveDialog(/** Choice identifier **/)
	{
        dialogResolved = true;
	}
}
