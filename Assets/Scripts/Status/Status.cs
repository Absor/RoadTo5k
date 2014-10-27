using UnityEngine;
public enum StatusType : int
{
	Rating = 0,
    Rage = 1,
    Charisma = 2,
    Luck = 3,
    Knowledge_Support = 4,
    Knowledge_Ganker = 5,
    Knowledge_Carry = 6,
    Time = 7,
    Skillpoints_Available = 8,
    Skillpoints_Assign_Amount = 9,
    Day_Start_Time_Min = 10,
    Day_Start_Time_Max = 11,
    Rage_Gain_Modifier = 12,
    Talent = 13,
    Food = 14,
    Money = 15
}

public static class StatusTypeExtensions
{
    public static string ToNiceString(this StatusType type)
    {
		if (type == StatusType.Day_Start_Time_Min){
			return "earliest start time";
		}
		if (type == StatusType.Day_Start_Time_Max){
			return "latest start time";
		}
		return type.ToString().Replace("_", ": ");
    }

    public static bool IsAPlayerAttribute(this StatusType type)
    {
        return type == StatusType.Charisma || type == StatusType.Knowledge_Carry || type == StatusType.Knowledge_Ganker ||
            type == StatusType.Knowledge_Support || type == StatusType.Luck || type == StatusType.Talent;
    }
}

public class Status
{
    public StatusType type;
    private int _points;
    public int points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = Mathf.Clamp(value, 0, maxPoints);
        }
    }
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
