using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MatchScript : Singleton<MatchScript> {

    public GameObject heroPickScreen;
    public GameObject gameScreen;
    public GameObject gameEndScreen;

    public Button gameScreenNextStepButton;
    public SliderInputScript sliderInputScript;

	private MatchState matchState;

    void Start()
    {
        gameScreenNextStepButton.onClick.AddListener(nextStepButtonPressed);
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
        // RANDOMIZE HEROES
        for (int i = 0; i < 10; i++)
        {
            Hero hero = RandomGeneratorScript.Instance.GetRandomHero();
            if (i < 5)
            {
                matchState.team1Heroes.Add(hero);
            }
            else
            {
                matchState.team2Heroes.Add(hero);
            }
			matchState.matchHeroes.Add(hero);
        }

		ActivateScreen(heroPickScreen);
	}
	
	public void PickHero()
	{

	}

	public void HeroPickReady()
	{
		// Check if hero is picked and so on...
		ActivateScreen(gameScreen);

        // Reset fight manager (animations) has to be after activate to update animations
        FightManagerScript.Instance.Reset();
    }

    private void PlayNextStep()
    {
        // Win condition whatevers, could be inside fightmanager or dialogmanager
        if (matchState.dialogsPlayed >= 3 && matchState.fightsPlayed >= 3)
        {
            matchState.isWon = true;
        }

        if (matchState.isWon) {
            matchWon(); // Could go through button too if we dont want to show end screen immediately (public)
        } else {
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
    }

    public void InteractWithChat()
    {
        Debug.Log("INTERACTION"); // TODO
    }

    private void playFightStep()
    {
        StartCoroutine(FightManagerScript.Instance.PlayFightStep(matchState, fightResolved));
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
    }

    private void fightResolved()
    {
        matchState.fightsPlayed++;
        gameScreenNextStepButton.gameObject.SetActive(true);
    }

    private void matchWon()
    {
        ActivateScreen(gameEndScreen);
    }
}
