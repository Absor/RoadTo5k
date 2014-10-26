using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum MatchPhase
{
    HERO_PICK,
    GAME,
    END
}

public class MatchScript : Singleton<MatchScript> {

    public GameObject heroPickScreen;
    public GameObject gameScreen;
    public GameObject gameEndScreen;

    public Button gameScreenNextStepButton;
	public MatchState matchState;

    private MatchPhase currentPhase;

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
        currentPhase = MatchPhase.HERO_PICK;

        ChatMessage startMessage = new ChatMessage();
        startMessage.messageType = ChatMessageType.SYSTEM;
        startMessage.message = "Joined game. Pick your hero!";
        ChatManagerScript.Instance.AddMessage(startMessage);
	}

	public void HeroPickReady()
	{
		ActivateScreen(gameScreen);
        // Reset fight manager (animations) has to be after activate to update animations
        FightManagerScript.Instance.Reset();
        List<string> names = new List<string>();
        for (int i = 0; i < matchState.matchHeroes.Count; i++)
        {
            if (i == 0)
            {
                names.Add("You");
            }
            else
            {
                names.Add(matchState.matchHeroes[i].name);
            }
        }
        FightAnimatorScript.Instance.SetTexts(names);

        currentPhase = MatchPhase.GAME;
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

		//push
		foreach (Hero hero in matchState.team1Heroes) {
			matchState.dealDmgToEnemyTower(1, hero);
		}
		foreach (Hero hero in matchState.team2Heroes) {
			matchState.dealDmgToEnemyTower(2, hero);
		}

		Debug.Log (matchState.printTowerSituation ());
		//distribute farm...
		int baseGold = 2500; //per side
		int advantage = matchState.teamAdvantage ();
		matchState.distributeFarmForTeam (baseGold + advantage * 200, 1);
		matchState.distributeFarmForTeam (baseGold - advantage * 200, 2);

        // or who knows what based on matchState
    }

    public void InteractWithChat()
    {
        switch (currentPhase)
        {
            case MatchPhase.HERO_PICK:
                HeroPickManagerScript.Instance.ChatInterAction();
                break;
            case MatchPhase.GAME:
                // TODO HERE
                break;
        }
    }

    private void playFightStep()
    {
        FightManagerScript.Instance.PlayFightStep();
    }

    private void playDialogStep()
    {
        MatchEventManagerScript.Instance.DecideEvent(matchState);
    }

    public void StepResolved()
    {
        gameScreenNextStepButton.gameObject.SetActive(true);
        int addMinutes = Random.Range(5, 10);
        matchState.matchMinutes += addMinutes;
        GameStateManagerScript.Instance.AdvanceTime(addMinutes);
        matchState.team1Wards -= addMinutes;
        matchState.team2Wards -= addMinutes;
        matchState.timeToRushan -= addMinutes;
        checkForVictory();
    }
	                  

    private void checkForVictory()
    {
        // Win condition whatevers, could be inside fightmanager or dialogmanager
        if (matchState.team1Towers == 0 || matchState.team2Towers == 0)
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
        currentPhase = MatchPhase.END;
        MusicManagerScript.Instance.ChangeMusicType(MusicType.Theme);
    }
}
