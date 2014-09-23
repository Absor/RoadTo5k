using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchScript : MonoBehaviour {

    public GameObject heroPickScreen;
    public GameObject gameScreen;
    public GameObject gameEndScreen;

    public ChatManagerScript chatManagerScript;

	private List<Hero> team1Heroes;
	private List<Hero> team2Heroes;
	private bool playing;

    private void ActivateScreen(GameObject screen)
    {
        heroPickScreen.SetActive(heroPickScreen == screen);
        gameScreen.SetActive(gameScreen == screen);
        gameEndScreen.SetActive(gameEndScreen == screen);
	}

	public void StartMatch()
	{
		chatManagerScript.EmptyChat();
		team1Heroes = new List<Hero>();
		team2Heroes = new List<Hero>();
		playing = false;

		// RANDOMIZE HEROES
		for (int i = 0; i < 10; i++) {
			Hero hero = new Hero();
			hero.damage = Random.Range (2, 4);
			hero.healing = Random.Range (1, 3);
			hero.health = Random.Range (12, 15);
			if (i < 5) {
				team1Heroes.Add (hero);
			} else {
				team2Heroes.Add (hero);
			}
		}

		ActivateScreen(heroPickScreen);
	}
	
	public void PickHero()
	{

	}

	public void HeroPickReady()
	{
		// Check if hero is picked and so on...
		ActivateScreen(gameScreen);
		playing = true;
		StartCoroutine(Play());
	}

	IEnumerator Play() {
		int team1Hp = countTotalHeroHealth(team1Heroes);
		int team2Hp = countTotalHeroHealth(team2Heroes);

		int turn = 1;
		while(playing) {
			Debug.Log ("Turn start | team 1 health: "+team1Hp + "| team 2 health: " + team2Hp);
			int team1Damage = countTotalHeroDamage(team1Heroes);
			int team2Damage = countTotalHeroDamage(team2Heroes);
			int team1Healing = countTotalHeroHealing(team1Heroes);
			int team2Healing = countTotalHeroHealing(team2Heroes);

			team1Hp -= team2Damage + team1Healing;
			team2Hp -= team1Damage + team2Healing;

			if (team1Hp <= 0 || team2Hp <= 0 || turn >= 20) {
				playing = false;
			}
			turn++;
			Debug.Log ("Turn end | team 1 health: "+team1Hp + "| team 2 health: " + team2Hp);
			yield return new WaitForSeconds(0.5f);
		}
	}

	private int countTotalHeroHealth(List<Hero> heroes) {
		int health = 0;
		foreach(Hero hero in heroes) {
			health += hero.health;
		}
		return health;
	}

	private int countTotalHeroDamage(List<Hero> heroes) {
		int damage = 0;
		foreach(Hero hero in heroes) {
			damage += hero.damage;
		}
		return damage;
	}

	private int countTotalHeroHealing(List<Hero> heroes) {
		int healing = 0;
		foreach(Hero hero in heroes) {
			healing += hero.healing;
		}
		return healing;
	}
}
