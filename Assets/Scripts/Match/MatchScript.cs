using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MatchScript : MonoBehaviour {

    public GameObject heroPickScreen;
    public GameObject gameScreen;
    public GameObject gameEndScreen;

    public ChatManagerScript chatManagerScript;
	public DialogManagerScript dialogManagerScript;
    public FightManagerScript fightManagerScript;

    public Button gameScreenNextStepButton;

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
		chatManagerScript.EmptyChat();

		// Prepare new
		matchState = new MatchState();
        // RANDOMIZE HEROES
        for (int i = 0; i < 10; i++)
        {
            Hero hero = new Hero();
            hero.damage = Random.Range(2, 4);
            hero.healing = Random.Range(1, 3);
            hero.maxhp = Random.Range(12, 15);
			hero.initiative = Random.Range(1, 100);
			hero.currenthp = hero.maxhp;
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
        fightManagerScript.Reset();
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

    private void playFightStep()
    {
        StartCoroutine(fightManagerScript.PlayFightStep(matchState, fightResolved));
    }

    private void playDialogStep()
    {
        Dialog dialog = new Dialog();
        dialog.dialogOptions.Add(new DialogOption());
        dialogManagerScript.ShowDialog(dialog, dialogResolved);
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
