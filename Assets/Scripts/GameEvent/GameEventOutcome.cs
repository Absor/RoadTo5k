using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventOutcome
{
    public StatusType statusType;
    public bool replace;
    public int min;
    public int max;
    public double baseChance;
    public double luckModifier;
    public bool permanent;

    private StatusChange resolvedChange;

    public void Reset()
    {
        resolvedChange = new StatusChange();
        resolvedChange.statusType = statusType;
        resolvedChange.permanent = permanent;
        resolvedChange.replace = replace;
        // TODO chance, also in tooltip
        resolvedChange.value = UnityEngine.Random.Range(min, max + 1);
    }

    public string GetTooltipText()
    {
        double chance = getChance();
        if (chance <= 0)
        {
            return "";
        }
        else if (chance >= 1)
        {
			if (statusType.Equals(StatusType.Day_Start_Time_Min) || statusType.Equals(StatusType.Day_Start_Time_Max)){
				return formatIntToTime(resolvedChange.value) + " " + statusType.ToNiceString();
			}
            return formatInt(resolvedChange.value) + " " + statusType.ToNiceString();
        }
        return Math.Round(chance * 100, 1) + "% chance for " + formatInt(min) + " to " + formatInt(max) + " of " + statusType.ToNiceString();
    }

    public StatusChange GetStatusChange()
    {
        return resolvedChange;
    }

    private double getChance()
    {
        return baseChance + luckModifier * GameStateManagerScript.Instance.GetStatus(StatusType.Luck).GetNormalizedPoints();
    }

    private string formatInt(int value)
    {
        return (value > 0 ? "+" + value : "" + value);
    }
	private string formatIntToTime(int value)
	{
		TimeSpan t = TimeSpan.FromMinutes(value);

		string time = string.Format("{0:D2}:{1:D2}", 
		                              t.Hours, 
		                              t.Minutes);
		                            
		return time;
	}
}