namespace Sesto.RoadTo5k
{
    public interface IGameModel
    {
        int day { get; set; }

        int hour { get; set; }

        ComputerGameStateIdentifier computerGameState { get; set; }

        CameraIdentifier currentCamera { get; set; }
    }
}
