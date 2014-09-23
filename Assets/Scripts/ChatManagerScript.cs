using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatManagerScript : MonoBehaviour {

    // Chat Scripts to give updates to
    public ChatScript[] chatScripts;

    private List<string> messages;

	void Awake () {
	    messages = new List<string>();
	}

    public void AddMessage(string message)
    {
        messages.Add(message);
        foreach (ChatScript chatScript in chatScripts)
        {
            chatScript.UpdateMessages(messages);
        }
    }
}
