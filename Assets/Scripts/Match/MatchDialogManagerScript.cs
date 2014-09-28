using UnityEngine;
using System.Collections;
using System;

public class MatchDialogManagerScript : MonoBehaviour {

	public GameObject dialogPanel;

    private bool dialogResolved;

	void Awake()
	{
		dialogPanel.SetActive(false);
	}

    public IEnumerator PlayDialogStep(MatchState matchState, Action done)
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
        done();
	}

	public void ResolveDialog(/** Choice identifier **/)
	{
        dialogResolved = true;
	}
}
