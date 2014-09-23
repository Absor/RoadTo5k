using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState {
	public List<Hero> team1Heroes;
	public List<Hero> team2Heroes;
    public bool isWon;
    public int matchMinutes;
    public int dialogsPlayed;
    public int fightsPlayed;

	public MatchState()
	{
		team1Heroes = new List<Hero>();
		team2Heroes = new List<Hero>();
        isWon = false;
        dialogsPlayed = 0;
        fightsPlayed = 0;
	}
}
