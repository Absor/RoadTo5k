using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum HeroType
{
    Carry,
    Ganker,
    Support
}

public class Hero
{
	public int id;

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
    public HeroType heroType;

    public float gank;
    public float farm;
    public float push;

	public MatchState state;

	//once per match, set the sides for easier access
	public void startFight(MatchState state) {
		this.state = state;
		dead = false;		//if you've been selected for a fight, you most likely shouldn't be dead
		currenthp = maxhp;	//TODO: this isn't true, but for now lets keep it this way
		if (state.team1Heroes.Contains(this))
			myTeamNo = 1;
		else 
			myTeamNo = 2;
	}

	public void fightAction() {
		//get some basic idea what's going on in the match, choose your action and the target for it
		Hero currentTarget = state.getEnemyFightTeam(myTeamNo)[(Random.Range(0, state.getEnemyFightTeam(myTeamNo).Count))];
		foreach (Hero hero in state.getMyFightTeam(myTeamNo))
		{
			//check need for defensive actions
		}
		foreach (Hero hero in state.getEnemyFightTeam(myTeamNo))
		{
			//check need for offensive actions
		}
		//make the decision
		autoAttack(currentTarget);
	}

	public void autoAttack(Hero target)
	{
		if (target.myTeamNo == myTeamNo)
			Debug.Log ("eijumalauta*****************");
		target.currenthp -= damage;
		if (target.currenthp <= 0)
		{
			state.heroDied(target); //update game state with the info
		}
	}

	public void useSkill(Hero target)
	{

	}

	public void setSliders() {
		float timeToUse = 1;
		int actionPrio = 1;
		string[] actionsList = getPreferredActions();
		foreach (string action in actionsList) {
			timeToUse = setSlider(action, timeToUse, actionPrio);
		}
	}

	private float setSlider(string action, float timeToUse, int actionPrio) {
		float timeSpent = 0;
		int ratingEffect = getGameRatingEffect(GameStateManagerScript.Instance.GetStatus(StatusType.Rating).points);
		switch (actionPrio)
		{
		case 1:
			timeSpent = Random.Range((20 + ratingEffect), 100) / 100.0f;
			break;
		case 2:
			timeSpent = Random.Range(0, (int)(timeToUse * 100)) / 100.0f;
			break;
		case 3:
			timeSpent = timeToUse;
			break;
		}
		setGankFarmPush(action, timeSpent);
		return timeToUse - timeSpent;
	}

	public string[] getPreferredActions() {
		if (heroType == HeroType.Carry) {
			//check hero power, if high, can gank and if the game is late or people dead, push
			return new string[]{"farm", "push", "gank"};
		} else if (heroType == HeroType.Ganker) {
			return new string[]{"gank", "push", "farm"};
		} else { //HeroType Support
			return new string[]{"push", "gank", "farm"};
		}
	}


	//this should probably be somewhere else
	//possible range: 0-56 (depending on starting rating, which at this point is at 2200)
	public int getGameRatingEffect(int rating) {
		return rating / 50 - 44;
	}

	private void setGankFarmPush(string action, float value) {
		if (action.Equals ("farm")) {
			this.farm = value;
		} else if (action.Equals ("gank")) {
			this.gank = value;
		} else if (action.Equals ("push")) {
			this.push = value;
		}
	}

	public string ToString() {
		if (dead)
			return "team"+myTeamNo+" DEAD";
		else
			return "team" + myTeamNo +" "+ currenthp + "hp";
	}

}

