using System.Collections.Generic;
namespace Sesto.RoadTo5k
{
    /**
     * Models one match of the computer game (MOBA).
     **/
    public class ComputerGame
    {
        public int gameMinutes { get; set; }

        public List<ComputerGamePlayer> ownTeamPlayers { get; set; }

        public List<ComputerGamePlayer> enemyTeamPlayers { get; set; }

        public List<ChatMessage> chatMessages { get; set; }
    }
}
