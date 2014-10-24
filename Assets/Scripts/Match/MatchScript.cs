using UnityEngine;
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
		Hero.state = matchState;
        ActivateScreen(heroPickScreen);
        HeroPickManagerScript.Instance.StartNewHeroPick();
        MusicManagerScript.Instance.ChangeMusicType(MusicType.Battle);
	}

	public void HeroPickReady()
	{
		ActivateScreen(gameScreen);
        // Reset fight manager (animations) has to be after activate to update animations
        FightManagerScript.Instance.Reset();
    }

    private void PlayNextStep()
    {
		int loop = 0;
		foreach (Hero hero in matchState.matchHeroes) {
			if (loop == 0) {
				matchState.matchHeroes[0].gank = sliderInputScript.GetGank();
				matchState.matchHeroes[0].farm = sliderInputScript.GetFarm();
				matchState.matchHeroes[0].push = sliderInputScript.GetPush();
				matchState.matchHeroes[0].myTeamNo = 1;
			} else {
				if (loop > 4)
					matchState.matchHeroes[loop].myTeamNo = 2;
				else
					matchState.matchHeroes[loop].myTeamNo = 1;
				matchState.matchHeroes[loop].setSliders();
			}
			loop++;
		}

		//roll for participants
		List<Hero> team1 = new List<Hero>();
		List<Hero> team2 = new List<Hero>();
		foreach (Hero hero in matchState.matchHeroes) {
			if (hero.gank > Random.Range (0f, 1f)) {
				if (hero.myTeamNo == 1) {
					team1.Add(hero);
				}
				else {
					team2.Add(hero);
				}
			}
		}
		if (team1.Count == 0)
			team1.Add(matchState.teamHighestFarmer(1));
		if (team2.Count == 0)
			team2.Add(matchState.teamHighestFarmer(2));

		matchState.newFight(team1, team2);

        // Dialog or Fight
        if (Random.Range(0, 10) == 0)
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
        MatchEventManagerScript.Instance.DecideEvent(matchState);
    }

    public void DialogResolved()
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
        MusicManagerScript.Instance.ChangeMusicType(MusicType.Theme);
    }
}
