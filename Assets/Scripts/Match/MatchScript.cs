﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MatchScript : Singleton<MatchScript> {

    public GameObject heroPickScreen;
    public GameObject gameScreen;
    public GameObject gameEndScreen;

    public Button gameScreenNextStepButton;
	public MatchState matchState;

    private SliderInputScript sliderInputScript;

    void Awake()
    {
        gameScreenNextStepButton.onClick.AddListener(nextStepButtonPressed);
        sliderInputScript = GetComponentInChildren<SliderInputScript>();
    }

    private void nextStepButtonPressed()
    {
        gameScreenNextStepButton.gameObject.SetActive(false);
        PlayNextStep();
    }

    private void ActivateScreen(GameObject screen)
    {
        heroPickScreen.SetActive(heroPickScreen == screen);
        gameScreen.SetActive(gameScreen == screen);
        gameEndScreen.SetActive(gameEndScreen == screen);
	}

	public void StartMatch()
	{
		// Cleanup
        ChatManagerScript.Instance.EmptyChat();
		// Prepare new
		matchState = new MatchState();
        ActivateScreen(heroPickScreen);
        HeroPickManagerScript.Instance.StartNewHeroPick();
	}

	public void HeroPickReady()
	{
		ActivateScreen(gameScreen);
        // Reset fight manager (animations) has to be after activate to update animations
        FightManagerScript.Instance.Reset();
    }

    private void PlayNextStep()
    {
        matchState.matchHeroes[0].gank = sliderInputScript.GetGank();
        matchState.matchHeroes[0].farm = sliderInputScript.GetFarm();
        matchState.matchHeroes[0].push = sliderInputScript.GetPush();
        // Dialog or Fight
        if (Random.Range(0, 2) == 0)
        {
            playDialogStep();
        }
        else
        {
            playFightStep();
        }
        // or who knows what based on matchState
    }

    public void InteractWithChat()
    {
        Debug.Log("INTERACTION"); // TODO
    }

    private void playFightStep()
    {
        FightManagerScript.Instance.PlayFightStep();
    }

    private void playDialogStep()
    {
        Dialog dialog = new Dialog();
        dialog.dialogOptions.Add(new DialogOption());
        DialogManagerScript.Instance.ShowDialog(dialog, dialogResolved);
    }

    private void dialogResolved(DialogOption option)
    {
        matchState.dialogsPlayed++;
        gameScreenNextStepButton.gameObject.SetActive(true);
        checkForVictory();
    }

    public void FightResolved()
    {
        matchState.fightsPlayed++;
        gameScreenNextStepButton.gameObject.SetActive(true);
        checkForVictory();
    }

    private void checkForVictory()
    {
        // Win condition whatevers, could be inside fightmanager or dialogmanager
        if (matchState.dialogsPlayed >= 3 && matchState.fightsPlayed >= 3)
        {
            matchState.isWon = true;
        }

        if (matchState.isWon)
        {
            matchWon(); // Could go through button too if we dont want to show end screen immediately (public)
        }
    }

    private void matchWon()
    {
        ActivateScreen(gameEndScreen);
    }
}
