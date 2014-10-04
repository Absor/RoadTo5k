using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public enum StatusType
{
    RAGE,
    CHARISMA,
    LUCK
}

public class PlayerScript : MonoBehaviour {

    public int skillPointsAvailable;

    public int assignAmount;

    public int rage;
    public int maxRage = 100;

    public int charisma;
    public int maxCharisma = 100;

    public int luck;
    public int maxLuck = 100;

    public float GetNormalizedRage()
    {
        return 1f * rage / maxRage;
    }

    public float GetNormalizedLuck()
    {
        return 1f * luck / maxLuck;
    }

    public float GetNormalizedCharisma()
    {
        return 1f * charisma / maxCharisma;
    }
}
