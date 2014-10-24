using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class HeroPickManagerScript : Singleton<HeroPickManagerScript> {

    public GameObject portraitsContainer;
    public GameObject teamPortraitsContainerPrefab;
    public GameObject portraitPrefab;
    public Button readyButton;
    public GameObject pickerContainer;
    public GameObject pickerPrefab;

    private PortraitScript[] portraitScripts;
    private Dictionary<HeroType, Toggle> heroPickers;

    void Awake()
    {
        portraitScripts = new PortraitScript[10];
        GameObject team2portraits = Instantiate(teamPortraitsContainerPrefab) as GameObject;
        team2portraits.transform.SetParent(portraitsContainer.transform, false);
        GameObject team1portraits = Instantiate(teamPortraitsContainerPrefab) as GameObject;
        team1portraits.transform.SetParent(portraitsContainer.transform, false);        
        for (int i = 0; i < 10; i++)
        {
            GameObject portrait = Instantiate(portraitPrefab) as GameObject;
            portraitScripts[i] = portrait.GetComponent<PortraitScript>();
            if (i < 5)
            {
                portrait.transform.SetParent(team1portraits.transform, false);
            }
            else
            {
                portrait.transform.SetParent(team2portraits.transform, false);
            }
        }

        readyButton.onClick.AddListener(heroPickReady);

        heroPickers = new Dictionary<HeroType,Toggle>();
        HeroType[] types = Enum.GetValues(typeof(HeroType)) as HeroType[];
        foreach (HeroType heroType in types)
        {
            GameObject picker = Instantiate(pickerPrefab) as GameObject;
            picker.transform.SetParent(pickerContainer.transform, false);
            Toggle heroPickerToggle = picker.GetComponent<Toggle>();
            HeroType savedType = heroType;
            heroPickers[savedType] = heroPickerToggle;
            picker.GetComponentInChildren<Text>().text = heroType.ToString();
            heroPickerToggle.onValueChanged.AddListener((bool newValue) =>
            {
                heroPickChange(savedType, newValue);
            });
        }
    }

    private void heroPickChange(HeroType typePick, bool newPick)
    {
        if (newPick)
        {
            MatchScript.Instance.matchState.matchHeroes[0].heroType = typePick;
            heroPickChanged();
        }
        updatePickers();
    }

    private void heroPickChanged()
    {
        updatePortraits();
    }

    private void heroPickReady()
    {
        MatchScript.Instance.HeroPickReady();
    }

    public void StartNewHeroPick()
    {
        // RANDOMIZE HEROES
        for (int i = 0; i < 10; i++)
        {
            Hero hero = RandomGeneratorScript.Instance.GetRandomHero();
			hero.id = i;
            if (i < 5)
            {
                MatchScript.Instance.matchState.team1Heroes.Add(hero);
            }
            else
            {
                MatchScript.Instance.matchState.team2Heroes.Add(hero);
            }
            MatchScript.Instance.matchState.matchHeroes.Add(hero);
        }

        updatePortraits();

        updatePickers();
    }

    private void updatePickers()
    {
        foreach (HeroType ht in heroPickers.Keys)
        {
            heroPickers[ht].isOn = ht == MatchScript.Instance.matchState.matchHeroes[0].heroType;
        }
    }

    private void updatePortraits()
    {
        for (int i = 0; i < 10; i++)
        {
            portraitScripts[i].UpdatePortrait(MatchScript.Instance.matchState.matchHeroes[i]);
        }
    }
}
