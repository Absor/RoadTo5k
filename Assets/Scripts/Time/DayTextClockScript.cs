using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayTextClockScript : MonoBehaviour, IClockScript {

    private Text text;

    void Start()
    {
        text = gameObject.GetSafeComponentInChildren<Text>();
    }

    public void UpdateTime(int day, int hour, int minute)
    {
        text.text = "Day " + day;
    }
}
