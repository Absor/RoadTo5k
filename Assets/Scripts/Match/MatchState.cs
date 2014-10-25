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
    public int timeToRushan;
    public int team1Wards;
    public int team2Wards;

	public MatchState()
	{
		team1Heroes = new List<Hero>();
		team2Heroes = new List<Hero>();
		matchHeroes = new List<Hero>();
        isWon = false;
        timeToRushan = 0;
        team1Wards = 0;
        team2Wards = 0;
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

		foreach (Hero hero in fightAllHeroes)
		{
			hero.startFight();
		}

	}

	public void newFight(List<Hero> team1Participants, List<Hero> team2Participants)
	{
		fightDeadHeroes = new List<Hero>();
		
		team1FightHeroes = new List<Hero>();
		team1FightHeroes.AddRange(team1Participants);
		
		team2FightHeroes = new List<Hero>();
		team2FightHeroes.AddRange(team2Participants);
		
		fightAllHeroes = new List<Hero>();
		fightAllHeroes.AddRange(team1FightHeroes);
		fightAllHeroes.AddRange(team2FightHeroes);
		
		foreach (Hero hero in fightAllHeroes)
		{
			hero.startFight(); //currently just sets everyone alive and full hp
		}
		
	}

	public void resetFight() {
		fightDeadHeroes = new List<Hero>();
		team1FightHeroes = new List<Hero>();
		team2FightHeroes = new List<Hero>();
		fightAllHeroes = new List<Hero>();
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
				totalFarmSlider += hero.farm;
		}

		return totalFarmSlider;
	}

	public Hero teamHighestFarmer(int teamNo) {
		Hero highest;
		if (teamNo == 1) {
			highest = team1Heroes[0];
			foreach (Hero hero in team1Heroes) {
				if (hero.farm > highest.farm)
					highest = hero;
			}
		} else {
			highest = team2Heroes[0];
			foreach (Hero hero in team2Heroes)
				if (hero.farm > highest.farm)
					highest = hero;
		}
		return highest;
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
		string str = "Participants, team 1 *" + team1FightHeroes.Count + "* team 2 *" + team2FightHeroes.Count + "* ";
		foreach (Hero hero in fightAllHeroes)
		{
			str += " " + hero.ToString();
		}
		return str;
	}

	public bool Team1Dead()
	{
		bool allFightersDead = true;
		foreach (Hero hero in team1FightHeroes)
		{
			if (!hero.dead)
				allFightersDead = false;
		}
		return allFightersDead;


	}
	public bool Team2Dead()
	{
		bool allFightersDead = true;
		foreach (Hero hero in team2FightHeroes)
		{
			if (!hero.dead)
				allFightersDead = false;
		}
		return allFightersDead;
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

	public int TeamCurrentlyDeadInFight(int teamNo)
	{
		int dead = 0;
		foreach (Hero hero in fightDeadHeroes)
		{
			if (hero.myTeamNo == teamNo)
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
