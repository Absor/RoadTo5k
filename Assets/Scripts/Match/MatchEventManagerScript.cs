using UnityEngine;
using System.Collections;

public class MatchEventManagerScript : Singleton<MatchEventManagerScript> {

	public void DecideEvent(MatchState matchState) {
        int team1Dead = matchState.Team1CurrentlyDead();
        int team2Dead = matchState.Team2CurrentlyDead();

        if (Mathf.Abs(team1Dead - team2Dead) > 2)
        {
            // TODO kaikkee paskaa
        }

        Dialog dialog = new Dialog();
        dialog.dialogOptions.Add(new DialogOption());
        DialogManagerScript.Instance.ShowDialog(dialog, dialogResolved);
    }

    private void dialogResolved(DialogOption option)
    {
        
        MatchScript.Instance.DialogResolved();
    }
}
