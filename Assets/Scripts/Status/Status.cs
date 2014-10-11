public enum StatusType
{
    Rage,
    Charisma,
    Luck,
    Knowledge_Support,
    Knowledge_Ganker,
    Knowledge_Carry,
    Time,
    Skillpoints_Available,
    Skillpoints_Assign_Amount,
    Day_Start_Time_Min,
    Day_Start_Time_Max
}

public static class StatusTypeExtensions
{
    public static string ToNiceString(this StatusType type)
    {
        return type.ToString().Replace("_", ": ");
    }
}

public class Status
{
    public StatusType type;
    public int points;
    public int maxPoints;

    public Status(StatusType type, int points, int maxPoints)
    {
        this.type = type;
        this.points = points;
        this.maxPoints = maxPoints;
    }

    public float GetNormalizedPoints()
    {
        return 1f * points / maxPoints;
    }

    public static Status operator +(Status status, StatusChange change)
    {
        return null;
    }
}
