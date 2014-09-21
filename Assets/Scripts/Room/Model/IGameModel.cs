namespace Sesto.RoadTo5k
{
    /**
     * The main game state interface.
     **/
    public interface IGameModel
    {
        int day { get; set; }

        int hour { get; set; }

        int minute { get; set; }

        ComputerGameStateIdentifier computerGameState { get; set; }

        CameraIdentifier currentCamera { get; set; }

        ComputerGame currentGame { get; set; }
    }
}
