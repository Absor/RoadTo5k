using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayTextClockScript : MonoBehaviour, IClockScript {

    private Text text;

    void Awake()
    {
        text = gameObject.GetSafeComponent<Text>();
    }

    public void UpdateTime(GameTime time)
    {
        text.text = "Day " + time.day;
    }
}
