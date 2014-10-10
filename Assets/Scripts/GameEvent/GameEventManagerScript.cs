using UnityEngine;
using System.Collections;

public class GameEventManagerScript : Singleton<GameEventManagerScript> {

    private GameEvent currentEvent;

    public void ResolveGameEvent(GameEvent gameEvent)
    {
        currentEvent = gameEvent;
        if (gameEvent.dialog != null)
        {
            DialogManagerScript.Instance.ShowDialog(currentEvent.dialog, checkOption);
        }
    }

    private void checkOption(DialogOption option)
    {
        GameEventOutcome outcome = currentEvent.outcomes[option.optionId];
        if (outcome == null)
        {
            Debug.Log("GameEventOutcome was not in the GameEvent for DialogOption " + option.optionId);
            return;
        }
        StatusManagerScript.Instance.ApplyOutcome(outcome);
    }
}
