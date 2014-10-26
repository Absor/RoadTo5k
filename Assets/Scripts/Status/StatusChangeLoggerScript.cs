using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusChangeLoggerScript : Singleton<StatusChangeLoggerScript> {

    private Dictionary<StatusType, int> statusChanges;
    private int days;

    void Awake()
    {
        statusChanges = new Dictionary<StatusType, int>();
        days = 1;
    }

    public void LogStatusChange(StatusType type, int pointChange) {
        if (!statusChanges.ContainsKey(type))
        {
            statusChanges[type] = 0;
        }

        statusChanges[type] += pointChange;
    }

    public void PrintLog()
    {
        Debug.Log("START DAY LOG");
        foreach (StatusType key in statusChanges.Keys)
        {
            float perDay = 1.0f * statusChanges[key] / days;
            Debug.Log(key.ToNiceString() + " " + perDay + " per day, predicted to be at " + perDay * 30 + " after 30 days.");
        }
        Debug.Log("END DAY LOG");
    }

    public void AddDay()
    {
        days++;
    }
}
