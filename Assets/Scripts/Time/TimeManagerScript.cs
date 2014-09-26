using UnityEngine;
using System.Collections;

public class TimeManagerScript : MonoBehaviour {

	public int day;
	public int hour;
	public int minute;

    public WallClockScript wallClockScript;
    public ComputerClockScript computerClockScript;

    void Awake()
    {
        updateTimeHandlers();
    }

    private void updateTimeHandlers()
    {
        wallClockScript.updateTime(hour, minute);
        computerClockScript.updateTime(hour, minute);
    }
}
