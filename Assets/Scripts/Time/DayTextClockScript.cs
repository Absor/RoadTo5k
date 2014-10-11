using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayTextClockScript : MonoBehaviour, IClockScript {

    private Text text;

    void Start()
    {
        text = gameObject.GetSafeComponentInChildren<Text>();
    }

    public void UpdateTime(GameTime time)
    {
        text.text = "Day " + time.day;
    }
}
