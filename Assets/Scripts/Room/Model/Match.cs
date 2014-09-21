using System.Collections.Generic;
namespace Sesto.RoadTo5k
{
    /**
     * Models one match of the computer game (MOBA).
     **/
    public class Match
    {
        public int gameMinutes { get; set; }

        public List<MatchPlayer> ownTeamPlayers { get; set; }

        public List<MatchPlayer> enemyTeamPlayers { get; set; }

        public List<ChatMessage> chatMessages { get; set; }
    }
}
