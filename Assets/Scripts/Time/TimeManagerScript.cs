using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeManagerScript : Singleton<TimeManagerScript> {

	public int day;
	public int hour;
	public int minute;

    private List<IClockScript> clockScripts;

    void Start()
    {
        clockScripts = this.FindObjectsOfInterface<IClockScript>();
        updateTimeHandlers();
    }

    private void updateTimeHandlers()
    {
        foreach (IClockScript clockScript in clockScripts)
        {
            clockScript.UpdateTime(day, hour, minute);
        }
    }

    public void SetTime(int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
        updateTimeHandlers();
    }
}
