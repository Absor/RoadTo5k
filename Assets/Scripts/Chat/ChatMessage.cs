public enum ChatMessageType
{
    ALL,
    ALLIES,
    SYSTEM
}

public class ChatMessage
{
    // Check http://docs.unity3d.com/Manual/StyledText.html for colors and styling

    public ChatMessage()
    {
        senderColor = "black";
        messageColor = "black";
    }

    public string senderName { get; set; }

    public string senderColor { get; set; }

    public string message { get; set; }

    public string messageColor { get; set; }

    public ChatMessageType messageType { get; set; }

    public override string ToString()
    {
        switch (this.messageType) {
            case ChatMessageType.ALL:
                return "[ALL]" + "<color=" + senderColor + ">"+ senderName + "</color>: " + "<color=" + messageColor + ">" + message + "</color>";
            case ChatMessageType.ALLIES:
                return "[ALLIES]" + "<color=" + senderColor + ">" + senderName + "</color>: " + "<color=" + messageColor + ">" + message + "</color>";
            case ChatMessageType.SYSTEM:
                return "<color=" + messageColor + ">"+ message + "</color>";
        }
        return message;
    }
}

