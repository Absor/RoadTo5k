using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatManagerScript : MonoBehaviour {

    // Chat Scripts to give updates to
    public ChatScript[] chatScripts;

    private List<string> messages;

    public void AddMessage(string message)
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

	public void Reset()
	{
		messages = new List<string>();
		updateMessages();
	}
}
