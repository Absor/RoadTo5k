namespace Sesto.RoadTo5k
{
    public class GameModel : IGameModel
    {
        public int day { get; set; }

        public int hour { get; set; }

        public int minute { get; set; }

        public ComputerGameStateIdentifier computerGameState { get; set; }

        public CameraIdentifier currentCamera { get; set; }

        public ComputerGame currentGame { get; set; }
    }
}
