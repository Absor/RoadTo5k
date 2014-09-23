using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    public class HeroPickReadyCommand : Command
    {
        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public ComputerScreenStateChangedSignal computerScreenStateChangedSignal { get; set; }

        public override void Execute()
        {
            if (gameModel.currentMatch.heroPicked)
            {
                // Show game screen
                gameModel.computerGameState = ComputerScreenStateIdentifier.GAME;
                computerScreenStateChangedSignal.Dispatch(gameModel.computerGameState);
            }
            else
            {
                // Send chat error message
            }            
        }
    }
}
