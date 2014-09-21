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
            // TODO check if hero has been picked, send chat message if not and so on


            // Show game screen
            gameModel.computerGameState = ComputerScreenStateIdentifier.GAME;
            computerScreenStateChangedSignal.Dispatch(gameModel.computerGameState);
        }
    }
}
