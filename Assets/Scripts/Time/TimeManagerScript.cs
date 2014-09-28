using UnityEngine;
using System.Collections;

public class TimeManagerScript : MonoBehaviour {

	public int day;
	public int hour;
	public int minute;

    public WallClockScript wallClockScript;
    public ComputerClockScript computerClockScript;

    // Start because it depends on the other scripts
    void Start()
    {
        updateTimeHandlers();
    }

    private void updateTimeHandlers()
    {
        wallClockScript.updateTime(hour, minute);
        computerClockScript.updateTime(hour, minute);
    }
}
