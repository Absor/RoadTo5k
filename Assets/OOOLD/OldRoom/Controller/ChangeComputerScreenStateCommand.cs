using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    /**
     * Bound to ChangeComputerGameStateSignal, ComputerGameStateIdentifier
     * is injected from the signal automatically.
     * 
     * Checks if new computer game state can be accepted and if yes, sends
     * necessary signals to continue game.
     **/
    public class ChangeComputerScreenStateCommand : Command
    {
        [Inject]
        public ComputerScreenStateIdentifier newComputerGameStateIdentifier { get; set; }

        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public StartNewMatchSignal startNewMatchSignal { get; set; }

        [Inject]
        public HeroPickReadySignal heroPickReadySignal { get; set; }

        public override void Execute()
        {
            if (newComputerGameStateIdentifier == ComputerScreenStateIdentifier.HERO_PICK &&
                gameModel.computerGameState == ComputerScreenStateIdentifier.LOBBY)
            {
                // LOBBY -> HERO_PICK, starts new match
                startNewMatchSignal.Dispatch();
            }
            else if (newComputerGameStateIdentifier == ComputerScreenStateIdentifier.GAME &&
              gameModel.computerGameState == ComputerScreenStateIdentifier.HERO_PICK)
            {
                // HERO_PICK -> GAME, attemps to continue to the actual play
                heroPickReadySignal.Dispatch();
            }
            // TODO HERO_PICK -> GAME
            // TODO GAME -> GAME_END
            // TODO GAME_END -> LOBBY
        }
    }
}
