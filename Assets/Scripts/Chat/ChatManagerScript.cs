using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatManagerScript : Singleton<ChatManagerScript> {

    // Chat Scripts to give updates to
    private List<IChatScript> chatScripts;

    private List<ChatMessage> messages;

    void Awake()
    {
        chatScripts = this.FindObjectsOfInterface<IChatScript>();
    }

    public void AddMessage(ChatMessage message)
    {
        messages.Add(message);
		updateMessages ();
    }

	private void updateMessages()
	{
		foreach (ChatScript chatScript in chatScripts)
		{
			chatScript.UpdateMessages(messages);
		}
	}

	public void EmptyChat()
	{
        messages = new List<ChatMessage>();
		updateMessages();
	}
}
