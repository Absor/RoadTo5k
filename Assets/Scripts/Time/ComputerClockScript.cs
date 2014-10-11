using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComputerClockScript : MonoBehaviour, IClockScript
{
    public Text clockText;

    public void UpdateTime(GameTime time)
    {
        clockText.text = (time.hour < 10 ? "0" + time.hour : "" + time.hour) + ":" + (time.minute < 10 ? "0" + time.minute : "" + time.minute);
    }
}
