using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class RandomGeneratorScript : Singleton<RandomGeneratorScript> {

    public TextAsset nicknamesTextAsset;
    public TextAsset dailyEventsTextAsset;
    public TextAsset watchEventsTextAsset;

    private Dictionary<string, List<GameEvent>> dailyEvents;
    private List<GameEvent> watchEvents;
    private List<string> nickNames;

    void Awake()
    {
        dailyEvents = JsonMapper.ToObject<Dictionary<string, List<GameEvent>>>(dailyEventsTextAsset.text);
        watchEvents = JsonMapper.ToObject<List<GameEvent>>(watchEventsTextAsset.text);

        nickNames = new List<string>();
        string[] nicks = nicknamesTextAsset.text.Split(new char[]{'\n'});
        nickNames.AddRange(nicks);

        // TODO REMOVE BELOW
		/*
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
        o.baseChance = 0.5;
        o.luckModifier = 0;

        List<GameEventOutcome> os = new List<GameEventOutcome>();
        os.Add(o);
        e.outcomes.Add("ok", os);

        List<GameEvent> re = new List<GameEvent>();
        re.Add(e);
        events.Add("random", re);

        dailyEvents = events;


        string json = JsonMapper.ToJson(dailyEvents);

        Debug.Log(json);
        */
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

    public GameEvent GetWatchEvent()
    {
		return watchEvents[Random.Range(0, watchEvents.Count)];
    }

    public Player GetRandomPlayer()
    {
        // TODO rating based?
        float rage = Random.Range(0f, 1f);
        float charisma = Random.Range(0f, 1f);
        float luck = Random.Range(0f, 1f);
        float talent = Random.Range(0f, 1f);
        float knowledgeCarry = Random.Range(0f, 1f);
        float knowledgeGanker = Random.Range(0f, 1f);
        float knowledgeSupport = Random.Range(0f, 1f);

        return new Player(rage, charisma, luck, talent, knowledgeCarry, knowledgeGanker, knowledgeSupport);
    }

    public Hero GetRandomHero()
    {
        Hero hero = new Hero();
        hero.damage = Random.Range(2, 4);
        hero.healing = Random.Range(1, 3);
        hero.maxhp = Random.Range(12, 15);
        hero.initiative = Random.Range(1, 100);
        hero.currenthp = hero.maxhp;
        hero.heroType = HeroType.Carry;
        hero.name = GetRandomNickname();

        hero.player = GetRandomPlayer();
        return hero;
    }

    public string GetRandomNickname()
    {
        return nickNames[Random.Range(0, nickNames.Count)];
    }
}
