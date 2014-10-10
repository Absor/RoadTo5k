using System.Collections.Generic;
public class GameEvent
{
    public Dialog dialog;

    public Dictionary<string, GameEventOutcome> outcomes;

    public GameEvent()
    {
        outcomes = new Dictionary<string, GameEventOutcome>();
    }
}