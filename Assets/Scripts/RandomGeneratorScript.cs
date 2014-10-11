﻿using UnityEngine;
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

        // TODO REMOVE BELOW
        Dictionary<string, List<GameEvent>> events = new Dictionary<string, List<GameEvent>>();
        GameEvent day1 = new GameEvent();

        Dialog daily = new Dialog();
        daily.dialogText = "You get home from work, you feel bored. You decide to aim for a 5k rating in MOBA. You feel a bit better about yourself for making this great decision.";
        DialogOption ok = new DialogOption();
        ok.optionText = "Ok";
        ok.optionId = "ok";
        daily.dialogOptions.Add(ok);
        day1.dialog = daily;

        List<GameEventOutcome> outcomes = new List<GameEventOutcome>();
        day1.outcomes.Add("ok", outcomes);

        List<GameEvent> day1event = new List<GameEvent>();
        day1event.Add(day1);
        events.Add("1", day1event);


        GameEvent e = new GameEvent();

        Dialog d = new Dialog();
        d.dialogText = "You chatted with your friend. He told you some gameplay tips.";
        DialogOption opt = new DialogOption();
        opt.optionText = "Ok";
        opt.optionId = "ok";
        d.dialogOptions.Add(opt);
        e.dialog = d;

        GameEventOutcome o = new GameEventOutcome();
        o.min = 2;
        o.max = 4;
        o.statusType = StatusType.Knowledge_Carry;
        o.baseChance = 100;
        o.luckModifier = 0;

        List<GameEventOutcome> os = new List<GameEventOutcome>();
        os.Add(o);
        e.outcomes.Add("ok", os);

        List<GameEvent> re = new List<GameEvent>();
        re.Add(e);
        events.Add("random", re);

        dailyEvents = events;


        string json = JsonMapper.ToJson(e);

        Debug.Log(json);
    }

    public GameEvent GetDailyEvent(int day)
    {
        string key = day + "";
        List<GameEvent> dailies;
        if (dailyEvents.ContainsKey(key))
        {
            dailies = dailyEvents[key];
        } else {
            dailies = dailyEvents["random"];
        }
        return dailies[Random.Range(0, dailies.Count)];
    }
}
