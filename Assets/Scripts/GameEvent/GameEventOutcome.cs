using System.Collections.Generic;
using UnityEngine;

public class GameEventOutcome
{
    public StatusType statusType;
    public bool replace;
    public int min;
    public int max;
    public int baseChance;
    public double luckModifier;

    private StatusChange resolvedChange;

    public void Reset()
    {
        resolvedChange = new StatusChange();
        resolvedChange.statusType = statusType;
        resolvedChange.value = Random.Range(min, max + 1);
    }

    public string GetTooltipText()
    {
        int chance = getChance();
        if (chance <= 0)
        {
            return "";
        }
        else if (chance >= 100)
        {
            return formatInt(resolvedChange.value) + " " + statusType.ToNiceString();
        }
        return chance + "% chance for " + formatInt(min) + " to " + formatInt(max) + " of " + statusType.ToNiceString();
    }

    public StatusChange GetStatusChange()
    {
        return resolvedChange;
    }

    private int getChance()
    {
        return baseChance + (int)(luckModifier * GameStateManagerScript.Instance.GetStatus(StatusType.Luck).points);
    }

    private string formatInt(int value)
    {
        return (value > 0 ? "+" + value : "" + value);
    }
}