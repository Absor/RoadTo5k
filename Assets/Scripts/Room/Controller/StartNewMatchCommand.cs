using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    public class StartNewMatchCommand : Command
    {
        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public ComputerScreenStateChangedSignal computerScreenStateChangedSignal { get; set; }

        public override void Execute()
        {
            gameModel.currentGame = new Match();
            gameModel.currentGame.gameMinutes = 0;

            // Show hero pick screen
            gameModel.computerGameState = ComputerScreenStateIdentifier.HERO_PICK;
            computerScreenStateChangedSignal.Dispatch(gameModel.computerGameState);
        }
    }
}
