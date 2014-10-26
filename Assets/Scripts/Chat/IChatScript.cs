using System.Collections.Generic;
public interface IChatScript
{
    void UpdateMessages(List<ChatMessage> messages);
}