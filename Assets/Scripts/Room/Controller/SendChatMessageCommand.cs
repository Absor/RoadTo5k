using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    public class SendChatMessageCommand : Command
    {
        [Inject]
        public ChatMessage chatMessage { get; set; }

        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public ChatChangedSignal chatChangedSignal { get; set; }

        public override void Execute()
        {
            gameModel.currentMatch.chatMessages.Add(chatMessage);
            chatChangedSignal.Dispatch(gameModel.currentMatch.chatMessages);
        }
    }
}
