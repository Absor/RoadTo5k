namespace Sesto.RoadTo5k
{
    public enum ChatMessageType
    {
        ALL,
        ALLIES,
        SYSTEM
    }

    public class ChatMessage
    {
        public string senderName { get; set; }

        public string message { get; set; }

        // e.g. Allies, All
        public ChatMessageType messageType { get; set; }

        public override string ToString()
        {
            switch (this.messageType) {
                case ChatMessageType.ALL:
                    return "[ALL]" + senderName + ": " + message;
                case ChatMessageType.ALLIES:
                    return "[ALLIES]" + senderName + ": " + message;
                case ChatMessageType.SYSTEM:
                    return message;
            }
            return message;
        }
    }
}
