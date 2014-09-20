using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    public class ChangeComputerGameStateCommand : Command
    {
        [Inject]
        public ComputerGameStateIdentifier newComputerGameStateIdentifier { get; set; }

        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public ComputerGameStateChangedSignal computerGameStateChangedSignal { get; set; }

        public override void Execute()
        {
            // TODO
            if (gameModel.computerGameState != newComputerGameStateIdentifier)
            {
                gameModel.computerGameState = newComputerGameStateIdentifier;
                computerGameStateChangedSignal.Dispatch(newComputerGameStateIdentifier);
            }
        }
    }
}
