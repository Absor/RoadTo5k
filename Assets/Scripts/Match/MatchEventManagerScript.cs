using UnityEngine;
using System.Collections;

public enum MatchEventType
{
    WARDS,
    OWN_RUSHAN,
    ENEMY_RUSHAN,
    BUYHELP,
    CARRYPOWER,
    SUCKYTEAMMATE,
    BADGAME
}

public class MatchEventManagerScript : Singleton<MatchEventManagerScript> {

    private MatchEventType eventType;

	public void DecideEvent(MatchState matchState) {
        int team1Dead = matchState.Team1CurrentlyDead();
        int team2Dead = matchState.Team2CurrentlyDead();

        if (Mathf.Abs(team1Dead - team2Dead) >= 3)
        {
            if (matchState.timeToRushan < 0 && matchState.matchMinutes >= 15) {
                // RuSHAAAN
                if (team1Dead > team2Dead)
                {
                    // enemy rushan
                }
                else
                {
                    // our rushan
                }
                // TODO move to resolve part
                matchState.timeToRushan = 15;
            }
        } else if (matchState.team1Wards <= 0) {
            // wardeeeei
            eventType = MatchEventType.WARDS;
            Dialog dialog = getWardsDialog();
            DialogManagerScript.Instance.ShowDialog(dialog, dialogResolved);
        }
        else if (Random.Range(0, 100) < 5)
        {
            // what to buy
        }

        
    }

    private void dialogResolved(DialogOption option)
    {
        switch (eventType)
        {
            case MatchEventType.WARDS:
                resolveWardsDialog(option);
                break;
        }
        MatchScript.Instance.DialogResolved();
    }

    private void resolveWardsDialog(DialogOption option)
    {
        switch (option.optionId)
        {
            case "team":

                break;
            case "self":

                break;
            case "neutral":
                break;
        }
    }

    private Dialog getWardsDialog()
    {
        Dialog dialog = new Dialog();
        dialog.dialogText = "You notice your teams doesn't have wards";
        DialogOption option = new DialogOption();
        option.optionId = "team";
        option.optionText = "Tell your team to buy wards";
        dialog.dialogOptions.Add(option);
        option = new DialogOption();
        option.optionId = "self";
        option.optionText = "Buy wards yourself";
        dialog.dialogOptions.Add(option);
        option = new DialogOption();
        option.optionId = "neutral";
        option.optionText = "Do nothing";
        dialog.dialogOptions.Add(option);

        return dialog;
    }
}
