using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManagerScript : MonoBehaviour {

	public int day;
	public int hour;
	public int minute;

    public WallClockScript wallClockScript;
    public ComputerClockScript computerClockScript;
    public Text dayText;

    void Start()
    {
        updateTimeHandlers();
    }

    private void updateTimeHandlers()
    {
        wallClockScript.UpdateTime(hour, minute);
        computerClockScript.UpdateTime(hour, minute);
        dayText.text = "Day " + day;
    }

    public void SetDayAndTime(int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
        updateTimeHandlers();
    }
}
