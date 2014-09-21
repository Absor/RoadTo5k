using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    public class StartNewMatchCommand : Command
    {
        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public ComputerScreenStateChangedSignal computerScreenStateChangedSignal { get; set; }

        [Inject]
        public SendChatMessageSignal sendChatMessageSignal { get; set; }

        public override void Execute()
        {
            gameModel.currentMatch = new Match();
            gameModel.currentMatch.gameMinutes = 0;
            gameModel.currentMatch.heroPicked = false;

            // Send join game message
            ChatMessage message = new ChatMessage();
            message.message = "Joined game.";
            message.messageColor = "red";
            message.messageType = ChatMessageType.SYSTEM;
            sendChatMessageSignal.Dispatch(message);

            // Show hero pick screen
            gameModel.computerGameState = ComputerScreenStateIdentifier.HERO_PICK;
            computerScreenStateChangedSignal.Dispatch(gameModel.computerGameState);
        }
    }
}
