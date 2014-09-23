using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Sesto.RoadTo5k
{
    public class ChatMessageHandler : MonoBehaviour
    {
        public Text textField;

        public void HandleChatMessages(List<ChatMessage> messages)
        {
            string chatText = "";
            foreach (ChatMessage message in messages)
            {
                chatText += message.ToString();
            }

            textField.text = chatText;
        }
    }
}
