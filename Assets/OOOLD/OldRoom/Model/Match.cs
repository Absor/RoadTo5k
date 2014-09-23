using System.Collections.Generic;
namespace Sesto.RoadTo5k
{
    /**
     * Models one match of the computer game (MOBA).
     **/
    public class Match
    {
        public Match()
        {
            ownTeamPlayers = new List<MatchPlayer>();
            enemyTeamPlayers = new List<MatchPlayer>();
            chatMessages = new List<ChatMessage>();
        }

        public int gameMinutes { get; set; }

        public List<MatchPlayer> ownTeamPlayers { get; set; }

        public List<MatchPlayer> enemyTeamPlayers { get; set; }

        public List<ChatMessage> chatMessages { get; set; }

        public bool heroPicked { get; set; }
    }
}
