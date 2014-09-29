using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightManagerScript : MonoBehaviour {

    public MatchScript matchScript;
    public FightAnimatorScript fightAnimatorScript;

    private bool fightEnded;

    public IEnumerator PlayFightStep(MatchState matchState)
    {
        List<Hero> team1Heroes = matchState.team1Heroes;
        List<Hero> team2Heroes = matchState.team2Heroes;
        int team1Hp = countTotalHeroHealth(team1Heroes);
        int team2Hp = countTotalHeroHealth(team2Heroes);

        int turn = 1;
        bool playing = true;
        while (playing)
        {
            Debug.Log("Turn start | team 1 health: " + team1Hp + "| team 2 health: " + team2Hp);
            int team1Damage = countTotalHeroDamage(team1Heroes);
            int team2Damage = countTotalHeroDamage(team2Heroes);
            int team1Healing = countTotalHeroHealing(team1Heroes);
            int team2Healing = countTotalHeroHealing(team2Heroes);

            team1Hp -= team2Damage + team1Healing;
            team2Hp -= team1Damage + team2Healing;

            if (team1Hp <= 0 || team2Hp <= 0 || turn >= 20)
            {
                playing = false;
            }
            turn++;
            Debug.Log("Turn end | team 1 health: " + team1Hp + "| team 2 health: " + team2Hp);
            yield return null;
        }

        matchState.fightsPlayed += 1;
        // TODO null to fight event list
        StartCoroutine(fightAnimatorScript.PlayFight(null, FightAnimated));
    }

    private int countTotalHeroHealth(List<Hero> heroes)
    {
        int health = 0;
        foreach (Hero hero in heroes)
        {
            health += hero.health;
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
    }

    public void Reset()
    {
        fightAnimatorScript.Reset();
    }

    public void FightAnimated()
    {
        matchScript.PlayNextStep();
    }
}
