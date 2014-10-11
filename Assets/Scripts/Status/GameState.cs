using System.Collections.Generic;

public class GameState
{
    public Dictionary<StatusType, Status> statuses;
    public List<StatusChange> temporaryChanges;
}