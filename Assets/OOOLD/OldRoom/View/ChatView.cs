using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class ChatView : View
    {
        // Settable from Unity
        public ChatMessageHandler chatMessageHandler;

        public void HandleChatMessages(List<ChatMessage> messages)
        {
            chatMessageHandler.HandleChatMessages(messages);
        }
    }
}
