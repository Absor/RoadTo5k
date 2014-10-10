using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class RandomGeneratorScript : Singleton<RandomGeneratorScript> {

    public TextAsset nicknamesTextAsset;
    public TextAsset dailyEventsTextAsset;

    private Dictionary<string, List<GameEvent>> dailyEvents;

    void Start()
    {
        dailyEvents = JsonMapper.ToObject<Dictionary<string, List<GameEvent>>>(dailyEventsTextAsset.text);
    }

    public GameEvent GetDailyEvent(int day)
    {
        string key = day + "";
        List<GameEvent> dailies;
        if (dailyEvents.ContainsKey(key))
        {
            dailies = dailyEvents[key];
        } else {
            dailies = dailyEvents["any"];
        }
        return dailies[Random.Range(0, dailies.Count)];
    }
}
