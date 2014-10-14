using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState {
	public List<Hero> team1Heroes;
	public List<Hero> team2Heroes;
	public List<Hero> team1FightHeroes;
	public List<Hero> team2FightHeroes;
	public List<Hero> matchHeroes;
	public List<Hero> fightDeadHeroes;
	public List<Hero> fightAllHeroes;
    public bool isWon;
    public int matchMinutes;
    public int dialogsPlayed;
    public int fightsPlayed;

	public MatchState()
	{
		team1Heroes = new List<Hero>();
		team2Heroes = new List<Hero>();
		matchHeroes = new List<Hero>();
        isWon = false;
        dialogsPlayed = 0;
        fightsPlayed = 0;
	}

	public void newFight()
	{
		fightDeadHeroes = new List<Hero>();

		team1FightHeroes = new List<Hero>();
		team1FightHeroes.AddRange(team1Heroes);

		team2FightHeroes = new List<Hero>();
		team2FightHeroes.AddRange(team2Heroes);

		fightAllHeroes = new List<Hero>();
		fightAllHeroes.AddRange(team1FightHeroes);
		fightAllHeroes.AddRange(team2FightHeroes);

	}

	public List<Hero> getMyFightTeam(int teamNo)
	{
		if (teamNo == 1)
			return team1FightHeroes;
		else
			return team2FightHeroes;
	}

	public float getMyTeamFarmSliderTotal(int teamNo)
	{
		float totalFarmSlider = 0;
		if (teamNo == 1) {
			foreach (Hero hero in team1Heroes) {
				totalFarmSlider += hero.farm;
			}
		} else {
			foreach (Hero hero in team2Heroes)
				totalFarmSlider += hero.farm + hero.gank + hero.push;
		}

		return totalFarmSlider;
	}

	public List<Hero> getEnemyFightTeam(int teamNo)
	{
		if (teamNo == 1)
			return team2FightHeroes;
		else
			return team1FightHeroes;
	}

	public void heroDied(Hero hero)
	{
		fightDeadHeroes.Add(hero);
		hero.dead = true;
		if (team1Heroes.Contains(hero))
			team1FightHeroes.Remove(hero);
		else
			team2FightHeroes.Remove(hero);
	}

	public string ToString()
	{
		string str = "";
		foreach (Hero hero in matchHeroes)
		{
			str += " " + hero.ToString();
		}
		return str;
	}

	public bool Team1Dead()
	{
		bool wholeTeamDead = true;
		foreach (Hero hero in team1FightHeroes)
		{
			if (!hero.dead)
				wholeTeamDead = false;
		}
		return wholeTeamDead;


	}
	public bool Team2Dead()
	{
		bool wholeTeamDead = true;
		foreach (Hero hero in team2FightHeroes)
		{
			if (!hero.dead)
				wholeTeamDead = false;
		}
		return wholeTeamDead;
	}

	public int Team1CurrentlyDead()
	{
		int dead = 0;
		foreach (Hero hero in team1Heroes)
		{
			if (hero.dead)
				dead++;
		}
		return dead;
	}

	public int Team2CurrentlyDead() 
	{
		int dead = 0;
		foreach (Hero hero in team2Heroes)
		{
			if (hero.dead)
				dead++;
		}
		return dead;
	}

	public bool AnyTeamDead()
	{	
		return Team1Dead() || Team2Dead();
	}
}
