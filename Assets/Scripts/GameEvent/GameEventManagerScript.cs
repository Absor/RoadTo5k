using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventManagerScript : Singleton<GameEventManagerScript> {

    private GameEvent currentEvent;

    public void ResolveGameEvent(GameEvent gameEvent)
    {
        currentEvent = gameEvent;

        currentEvent.Reset();

        if (currentEvent.HasDialog())
        {
            DialogManagerScript.Instance.ShowDialog(currentEvent.dialog, checkOption);
        }
        else
        {
            chooseOption("random");
        }
    }

    private void checkOption(DialogOption option)
    {
        chooseOption(option.optionId);
    }

    private void chooseOption(string option)
    {
        List<StatusChange> statusChanges = new List<StatusChange>();

        currentEvent.outcomes[option].ForEach(
            (GameEventOutcome outcome) => statusChanges.Add(outcome.GetStatusChange()));
        GameStateManagerScript.Instance.ApplyStatusChanges(statusChanges);
    }
}
