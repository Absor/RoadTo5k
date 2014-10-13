using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class FightManagerScript : Singleton<FightManagerScript> {

    private bool fightEnded;

    public void PlayFightStep()
    {
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

        matchState.newFight();

        foreach (Hero hero in matchHeroes)
        {
            hero.startFight(matchState);
        }

        while (playing)
        {
            Debug.Log("Turn " + turn + "start | " + matchState.ToString());
            matchHeroes.OrderBy(o => o.initiative);
            foreach (Hero hero in matchHeroes)
            {
                if (!hero.dead && !matchState.AnyTeamDead())
                {
                    Debug.Log(matchState.AnyTeamDead());
					Debug.Log("Total farm in team 1 : " + matchState.getMyTeamFarmSliderTotal(1));
                    hero.fightAction();
                }
            }
            yield return 0;
            /*
            int team1Damage = countTotalHeroDamage(team1Heroes);
            int team2Damage = countTotalHeroDamage(team2Heroes);
            int team1Healing = countTotalHeroHealing(team1Heroes);
            int team2Healing = countTotalHeroHealing(team2Heroes);

            team1Hp -= team2Damage + team1Healing;
            team2Hp -= team1Damage + team2Healing;

            if (team1Hp <= 0 || team2Hp <= 0 || turn >= 20)
            {
                playing = false;
            }*/

            if (turn >= 20 || matchState.AnyTeamDead())
            {
                playing = false;
            }

            turn++;
            Debug.Log("Turn " + (turn - 1) + "end | " + matchState.ToString());
        }

        Debug.Log("FIGHT ENDED, team1 deaths: " + matchState.Team1CurrentlyDead() + " team2 deaths: " + matchState.Team2CurrentlyDead());
        // TODO null to fight event list
        FightAnimatorScript.Instance.PlayFight(null);
    }

    public void AnimationDone()
    {
        MatchScript.Instance.FightResolved();
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

    public void Reset()
    {
        FightAnimatorScript.Instance.Reset();
    }
}
