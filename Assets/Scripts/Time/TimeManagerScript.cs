using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeManagerScript : Singleton<TimeManagerScript> {

    private List<IClockScript> clockScripts;

    void Start()
    {
        clockScripts = this.FindObjectsOfInterface<IClockScript>();
        GameStateManagerScript.Instance.OnGameStateUpdate.AddListener(updateTimeHandlers);
        updateTimeHandlers();
    }

    private void updateTimeHandlers()
    {
        foreach (IClockScript clockScript in clockScripts)
        {
            clockScript.UpdateTime(GameStateManagerScript.Instance.GetGameTime());
        }
    }
}
