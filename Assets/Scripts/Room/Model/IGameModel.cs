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

        ComputerScreenStateIdentifier computerGameState { get; set; }

        CameraIdentifier currentCamera { get; set; }

        Match currentGame { get; set; }
    }
}
