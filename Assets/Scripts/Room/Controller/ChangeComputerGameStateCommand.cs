using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    /**
     * Bound to ChangeComputerGameStateSignal, ComputerGameStateIdentifier
     * is injected from the signal automatically.
     * 
     * Checks if new computer game state can be accepted and if yes, updates
     * game model and sends ComputerGameStateChangedSignal (to views mostly).
     **/
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
                // New game LOBBY -> HERO_PICK
                gameModel.currentGame = new ComputerGame();
                gameModel.currentGame.gameMinutes = 0;

                gameModel.computerGameState = newComputerGameStateIdentifier;
                computerGameStateChangedSignal.Dispatch(newComputerGameStateIdentifier);
            }
            // TODO HERO_PICK -> GAME
            // TODO GAME -> GAME_END
            // TODO GAME_END -> LOBBY
        }
    }
}
