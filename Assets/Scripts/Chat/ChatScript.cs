using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour, IChatScript {

    public Text textField;
	
	public void UpdateMessages (List<string> messages) {
	    string chatText = "";
        foreach (string message in messages)
        {
            chatText += "\n" + message;
        }

        textField.text = chatText;
	}
}
