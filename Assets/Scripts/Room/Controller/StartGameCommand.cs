using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    public class StartGameCommand : Command
    {
        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public ComputerGameStateChangedSignal computerGameStateChangedSignal { get; set; }

        [Inject]
        public CameraMovedSignal cameraMovedSignal { get; set; }

        public override void Execute()
        {
            gameModel.day = 1;
            // TODO signal
            gameModel.hour = 8;
            // TODO signal

            // Computer starts from game lobby
            gameModel.computerGameState = ComputerGameStateIdentifier.LOBBY;
            computerGameStateChangedSignal.Dispatch(gameModel.computerGameState);

            // Camera starts from room view
            gameModel.currentCamera = CameraIdentifier.ROOM_CAMERA;
            cameraMovedSignal.Dispatch(gameModel.currentCamera, 0f);
        }
    }
}
