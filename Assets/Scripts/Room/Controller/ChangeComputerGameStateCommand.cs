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
            if (newComputerGameStateIdentifier == ComputerGameStateIdentifier.HERO_PICK &&
                gameModel.computerGameState == ComputerGameStateIdentifier.LOBBY)
            {
                // New game
                gameModel.currentGame = new ComputerGame();
                gameModel.currentGame.gameMinutes = 0;

                gameModel.computerGameState = newComputerGameStateIdentifier;
                computerGameStateChangedSignal.Dispatch(newComputerGameStateIdentifier);
            }
        }
    }
}
