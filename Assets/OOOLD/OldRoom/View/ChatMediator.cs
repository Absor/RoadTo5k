using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    /**
     * Communicates between ChatView and rest of the program.
     **/
    public class ChatMediator : Mediator
    {
        [Inject]
        public ChatView view { get; set; }

        [Inject]
        public ChatChangedSignal chatChangedSignal { get; set; }

        public override void OnRegister()
        {
            chatChangedSignal.AddListener(chatChanged);
        }

        public override void OnRemove()
        {
            chatChangedSignal.RemoveListener(chatChanged);
        }

        private void chatChanged(List<ChatMessage> messages)
        {
            view.HandleChatMessages(messages);
        }
    }
}
