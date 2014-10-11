using System.Collections.Generic;
using UnityEngine;
public enum StatusType
{
    Rage,
    Charisma,
    Luck,
    Knowledge_Support,
    Knowledge_Ganker,
    Knowledge_Carry
}

public class Status
{
    public StatusType type;
    public string name;
    public int points;
    public int maxPoints;
    public string tooltipText;

    public Status(StatusType type, int points, int maxPoints)
    {
        this.type = type;
        this.name = type.ToString().Replace("_", ":\n");
        this.points = points;
        this.maxPoints = maxPoints;
        this.tooltipText = this.name;
    }

    public float GetNormalizedPoints()
    {
        return 1f * points / maxPoints;
    }
}

public class GameTime {
    public int day;
    public int hour;
    public int minute;

    public GameTime(int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }
}

public class GameTimeRange
{
    public GameTime min;
    public GameTime max;

    public GameTime GetTimeInRange()
    {
        // Only hours and minutes now
        int minMinutes = min.hour * 60 + min.minute;
        int maxMinutes = max.hour * 60 + max.minute;
        int minutes = Random.Range(minMinutes, maxMinutes + 1);
        return new GameTime(0, minutes / 60, minutes % 60);
    }
}

public class GameState
{
    public int skillPointsAvailable;
    public int assignAmount;
    public Dictionary<StatusType, Status> statuses;
    public GameTime time;
    public GameTimeRange startOfNewDayTime;
}
