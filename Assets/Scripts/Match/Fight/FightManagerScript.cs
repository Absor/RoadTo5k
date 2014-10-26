using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class FightManagerScript : Singleton<FightManagerScript> {

    private bool fightEnded;
    private List<FightEvent> fightEvents;

    public void PlayFightStep()
    {
        fightEvents = new List<FightEvent>();
        StartCoroutine(simulateFight());
    }

    private IEnumerator simulateFight()
    {
        MatchState matchState = MatchScript.Instance.matchState;
        List<Hero> team1Heroes = matchState.team1Heroes;
        List<Hero> team2Heroes = matchState.team2Heroes;
        List<Hero> matchHeroes = matchState.matchHeroes;
        //int team1Hp = countTotalHeroHealth(team1Heroes);
        // int team2Hp = countTotalHeroHealth(team2Heroes);

        int turn = 1;
        bool playing = true;

        //matchState.newFight();

        while (playing)
        {
            Debug.Log("Turn " + turn + "start | " + matchState.ToString());
            matchHeroes.OrderBy(o => o.initiative);
            foreach (Hero hero in matchHeroes)
            {
                if (!hero.dead && !matchState.AnyTeamDead())
                {
                    //Debug.Log(matchState.AnyTeamDead());
					//Debug.Log("Total farm in team 1 : " + matchState.getMyTeamFarmSliderTotal(1));
                    hero.fightAction();
                }
            }
            yield return 0;
      
            if (turn >= 20 || matchState.AnyTeamDead()) //TODO: figure some way to alter turn limit?
            {
                playing = false;
            }

            turn++;
            Debug.Log("Turn " + (turn - 1) + "end | " + matchState.ToString());
        }

		Debug.Log("FIGHT ENDED, team1 deaths: " + matchState.TeamCurrentlyDeadInFight(1) + " team2 deaths: " + matchState.TeamCurrentlyDeadInFight(2));
    
		animateFight(matchState);
        
    }

    public void AnimationDone()
    {
        MatchScript.Instance.StepResolved();
    }
	/*
    private int countTotalHeroHealth(List<Hero> heroes)
    {
        int health = 0;
        foreach (Hero hero in heroes)
        {
            health += hero.currenthp;
        }
        return health;
    }

    private int countTotalHeroDamage(List<Hero> heroes)
    {
        int damage = 0;
        foreach (Hero hero in heroes)
        {
            damage += hero.damage;
        }
        return damage;
    }

    private int countTotalHeroHealing(List<Hero> heroes)
    {
        int healing = 0;
        foreach (Hero hero in heroes)
        {
            healing += hero.healing;
        }
        return healing;
    }*/

	private void animateFight(MatchState matchState) {
		FightEvent join = new FightEvent();
		join.eventType = FightEventType.JOIN_FIGHT;
		FightEventTarget target;

		FightEvent leave = new FightEvent();
		leave.eventType = FightEventType.LEAVE_FIGHT;
		
		foreach (Hero hero in matchState.fightAllHeroes) {
			target = new FightEventTarget();
			target.targetType = FightEventTargetType.HERO;
			target.id = hero.id;
			join.targets.Add(target);
			leave.targets.Add(target);
		}
		
		FightEvent die = new FightEvent();
		die.eventType = FightEventType.DEATH;
		
		foreach (Hero hero in matchState.fightDeadHeroes) {
			target = new FightEventTarget();
			target.targetType = FightEventTargetType.HERO;
			target.id = hero.id;
			die.targets.Add(target);
		}

		fightEvents.Add(join);
		fightEvents.Add(die);
		fightEvents.Add(leave);
		
		FightAnimatorScript.Instance.PlayFight(fightEvents);
	}

    public void Reset()
    {
        FightAnimatorScript.Instance.Reset();
    }
}
