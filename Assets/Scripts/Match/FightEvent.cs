using System.Collections.Generic;

public enum FightEventTargetType
{
    HERO
}

public class FightEventTarget
{
    public FightEventTargetType targetType;
    public int id;
}

public enum FightEventType
{
    JOIN_FIGHT,
    LEAVE_FIGHT
}

public class FightEvent
{
    public List<FightEventTarget> targets;
    public FightEventType eventType;

    public FightEvent()
    {
        targets = new List<FightEventTarget>();
    }
}
