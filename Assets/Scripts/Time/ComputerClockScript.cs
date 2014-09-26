using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComputerClockScript : MonoBehaviour
{

    public Text clockText;

    public void updateTime(int hour, int minute)
    {
        clockText.text = (hour < 10 ? "0" + hour : "" + hour) + ":" + (minute < 10 ? "0" + minute : "" + minute);
    }
}
