namespace Sesto.RoadTo5k
{
    /**
     * The main game state implementation.
     **/
    public class GameModel : IGameModel
    {
        public int day { get; set; }

        public int hour { get; set; }

        public int minute { get; set; }

        public ComputerScreenStateIdentifier computerGameState { get; set; }

        public CameraIdentifier currentCamera { get; set; }

        public Match currentMatch { get; set; }
    }
}
