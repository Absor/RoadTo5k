using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchEndScript : Singleton<MatchEndScript> {

    public Text left;
    public Text right;

    public void ResolveEnd(MatchState matchState)
    {
        showMatchStats(matchState);
        resolveStatGains(matchState);
    }

    private void resolveStatGains(MatchState matchState)
    {
        int rage = 0;
        int rating = 0;

        if (matchState.gameWinnerTeam() == 1)
        {
            rating += Random.Range(50, 70);
            rage += Random.Range(-10, -5);
        }
        else
        {
            rating += Random.Range(20, 40);
            rage += Random.Range(5, 10);
        }

        string text = "";
        text += "You gain " + rating + " rating.\n\n";
        text += (rage > 0 ? "You gain " : "You lose ") + Mathf.Abs(rage) + " rage.";

        right.text = text;

        GameStateManagerScript.Instance.GainRage(rage);
        GameStateManagerScript.Instance.GainRating(rating);
    }

    private void showMatchStats(MatchState matchState)
    {
        string stats = "";
        if (matchState.gameWinnerTeam() == 1)
        {
            stats += "<color=green>Your team won!</color>\n";
        }
        else
        {
            stats += "<color=red>Your team lost!</color>\n";
        }
        for (int i = 0; i < matchState.matchHeroes.Count; i++)
        {
            Hero hero = matchState.matchHeroes[i];
            if (i == 0)
            {
                stats += "Your team\n";
                stats += "Player    Kills/Deaths\n";
            }
            else if (i == 5)
            {
                stats += "\n\nEnemy team\n";
                stats += "Player    Kills/Deaths\n";
            }
            
            stats += string.Format("{0,-10}{1,2}/{2,-2}\n", hero.name, hero.kills, hero.deaths);
        }
        left.text = stats;
    }
}
