using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero
{
	public string name = "uliuli"; 	//TODO: get these generated from somewhere
	public int initiative; 			//the bigger the number, the better the chances of going first in a fight
	public int maxhp;
	public int currenthp;
	public int regen;				//might or might not want this, regen within a combat is ignored; 
									//use this to gain hp outside of combat (so being low hp in next fight to begin with is possible
	public int damage;
	public int healing;				//TODO: extract to a skill

	public bool dead = false;

	public int myTeamNo;
    public Player player;

	//once per match, set the sides for easier access
	public void startFight(MatchState state) {
		dead = false;		//if you've been selected for a fight, you most likely shouldn't be dead
		currenthp = maxhp;	//TODO: this isn't true, but for now lets keep it this way
		if (state.team1Heroes.Contains(this))
			myTeamNo = 1;
		else 
			myTeamNo = 2;
	}

	public void fightAction(MatchState state) {
		//get some basic idea what's going on in the match, choose your action and the target for it
		Debug.Log(state.getEnemyFightTeam(myTeamNo).Count);
		Hero currentTarget = state.getEnemyFightTeam(myTeamNo)[(Random.Range(0, state.getEnemyFightTeam(myTeamNo).Count- 1))];
		foreach (Hero hero in state.getMyFightTeam(myTeamNo))
		{
			//check need for defensive actions
		}
		foreach (Hero hero in state.getEnemyFightTeam(myTeamNo))
		{
			//check need for offensive actions
		}
		//make the decision
		autoAttack(currentTarget, state);
	}

	public void autoAttack(Hero target, MatchState state)
	{
		target.currenthp -= damage;
		if (target.currenthp <= 0)
		{
			state.heroDied(target); //update game state with the info
		}
	}

	public void useSkill(Hero target)
	{

	}

	public string ToString() {
		if (dead)
			return "team"+myTeamNo+" DEAD";
		else
			return "team" + myTeamNo +" "+ currenthp + "hp";
	}

}

