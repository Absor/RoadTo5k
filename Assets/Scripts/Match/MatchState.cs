﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchState {
	public List<Hero> team1Heroes;
	public List<Hero> team2Heroes;
	public List<Hero> team1FightHeroes;
	public List<Hero> team2FightHeroes;
	public List<Hero> matchHeroes;
	public List<Hero> fightDeadHeroes;
	public List<Hero> fightAllHeroes;
	public List<Hero> team1HeroesOrderedByFarm;
	public List<Hero> team2HeroesOrderedByFarm;
	public bool isWon;
    public int matchMinutes;
    public int timeToRushan;
    public int team1Wards;
    public int team2Wards;
	public int team1Towers = 9;
	public int team2Towers = 9;
	public int towerMaxHp = 500;
	public int team1CurrentTowerHp;
	public int team2CurrentTowerHp;

	public MatchState()
	{
		team1Heroes = new List<Hero>();
		team2Heroes = new List<Hero>();
		matchHeroes = new List<Hero>();
		team1HeroesOrderedByFarm = new List<Hero> ();
		team2HeroesOrderedByFarm = new List<Hero> ();
		team1HeroesOrderedByFarm.AddRange (team1Heroes);
		team2HeroesOrderedByFarm.AddRange (team2Heroes);
        isWon = false;
		team1CurrentTowerHp = towerMaxHp;
		team2CurrentTowerHp = towerMaxHp;
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
		hero.deaths++;
		if (team1Heroes.Contains(hero))
			team1FightHeroes.Remove(hero);
		else
			team2FightHeroes.Remove(hero);
	}

	public string ToString()
	{
		string str = "Participants, [team 1 *" + team1FightHeroes.Count + "*] [team 2 *" + team2FightHeroes.Count + "*] ";
		foreach (Hero hero in fightAllHeroes)
		{
			str += " " + hero.ToString();
		}
		return str;
	}

	public string printTowerSituation() {
		return "[Team 1 towers: " + team1Towers + ", current tower hp: "+ team1CurrentTowerHp +"]\n [Team 2 towers: " + team2Towers + ", current tower hp: "+ team2CurrentTowerHp +"]";
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

	public int teamAdvantage() { //>0 advantage for team1, <0 advantage for team2
		return (5 - Team1CurrentlyDead ()) - (5 - Team2CurrentlyDead ());
	}

	public void distributeFarmForTeam(int pool, int teamNo) {
		int goldAttempted;
		double successRating;
		if (teamNo == 1) {
			foreach (Hero hero in team1HeroesOrderedByFarm.OrderBy(o => o.farm)) {
				goldAttempted = pool/2; //first one gets to try for half the available gold, next half of that and so on
				//success getting the farm you're aiming for, half effort, half talent + minor luck bonus
				successRating = (hero.farm / 2) + ((hero.player.talent/2 + hero.player.luck/8 ) / 100) ;
				hero.gold += (int)(goldAttempted * successRating);
				pool -= goldAttempted;
			}
		} else {
			foreach (Hero hero in team2HeroesOrderedByFarm.OrderBy(o => o.farm)) {
				goldAttempted = pool/2; //first one gets to try for half the available gold, next half of that and so on
				//success getting the farm you're aiming for, half effort, half talent + minor luck bonus
				successRating = (hero.farm / 2) + ((hero.player.talent/2 + hero.player.luck/8 ) / 100) ;
				hero.gold += (int)(goldAttempted * successRating);
				pool -= goldAttempted;
			}
		}
	}

	public void destroyTeamTower(int teamNo) {
		if (teamNo == 1)
			team1Towers--;
		else
			team2Towers--;
	}

	//return winning team number if all of their towers are gone, otherwise returns 0
	public int gameWinnerTeam() {
		if (team1Towers < 1)
			return 2;
		else if (team2Towers < 1)
			return 1;
		else
			return 0;
	}

	public void dealDmgToEnemyTower(int myTeamNo, Hero dmgDealer) {
		int dmgDealt = 0;
		if (myTeamNo == 1) {
			dmgDealt = (int) (dmgDealer.damageToTower() * (0.2 + dmgDealer.push * (1 + Team2CurrentlyDead())));
			if (dmgDealer.dead) 
				dmgDealt = dmgDealt/2;
			team2CurrentTowerHp -= dmgDealt;

			if (team2CurrentTowerHp < 0) {
				team2CurrentTowerHp = towerMaxHp;
				team2Towers--;
				foreach (Hero hero in team1Heroes)
					hero.farm += 200;
			}
		} else {
			dmgDealt = (int) (dmgDealer.damageToTower() * (0.2 + dmgDealer.push * (1 + Team1CurrentlyDead())));
			if (dmgDealer.dead) 
				dmgDealt = dmgDealt/2;
			team1CurrentTowerHp -= dmgDealt;
			
			if (team1CurrentTowerHp < 0) {
				team1CurrentTowerHp = towerMaxHp;
				team1Towers--;
				foreach (Hero hero in team2Heroes)
					hero.farm += 200;
			}
		}
	}
}
