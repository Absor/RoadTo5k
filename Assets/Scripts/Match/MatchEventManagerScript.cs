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
    private MatchState matchState;

	public void DecideEvent(MatchState matchState) {
        this.matchState = matchState;
        int team1Dead = matchState.Team1CurrentlyDead();
        int team2Dead = matchState.Team2CurrentlyDead();

        if (Mathf.Abs(team1Dead - team2Dead) >= 3)
        {
            if (matchState.timeToRushan < 0 && matchState.matchMinutes >= 15) {
                // RuSHAAAN
                if (team1Dead > team2Dead)
                {
                    // enemy rushan
                    eventType = MatchEventType.ENEMY_RUSHAN;
                    Dialog dialog = getEnemyRushanDialog();
                    DialogManagerScript.Instance.ShowDialog(dialog, dialogResolved);
                }
                else
                {
                    // our rushan
                }
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

    private Dialog getEnemyRushanDialog()
    {
        Dialog dialog = new Dialog();
        dialog.dialogText = "You notice that the enemy teams is attempting to kill Rushan";
        DialogOption option = new DialogOption();
        option.optionId = "team";
        option.optionText = "Tell your team to buyback and try to kill them";
        dialog.dialogOptions.Add(option);
        option = new DialogOption();
        option.optionId = "self";
        option.optionText = "Try to prevent the kill yourself";
        dialog.dialogOptions.Add(option);
        option = new DialogOption();
        option.optionId = "neutral";
        option.optionText = "Do nothing";
        dialog.dialogOptions.Add(option);

        return dialog;
    }

    private void dialogResolved(DialogOption option)
    {
        switch (eventType)
        {
            case MatchEventType.WARDS:
                resolveWardsDialog(option);
                break;
            case MatchEventType.ENEMY_RUSHAN:
                resolveEnemyRushanDialog(option);
                break;
        }
        MatchScript.Instance.StepResolved();
    }

    private void resolveEnemyRushanDialog(DialogOption option)
    {
        switch (option.optionId)
        {
            case "team":
                matchState.timeToRushan = 15;
                break;
            case "self":
                matchState.timeToRushan = 15;
                break;
            case "neutral":
                break;
        }
    }

    private void resolveWardsDialog(DialogOption option)
    {
        switch (option.optionId)
        {
            case "team":
                
                break;
            case "self":
                matchState.team1Wards = 15;
                break;
            case "neutral":
                break;
        }
    }

    private Dialog getWardsDialog()
    {
        Dialog dialog = new Dialog();
        dialog.dialogText = "You notice your team doesn't have wards";
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
