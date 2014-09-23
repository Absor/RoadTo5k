using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour {

    public Text textField;
	
	public void UpdateMessages (List<string> messages) {
	    string chatText = "";
        foreach (string message in messages)
        {
            chatText += message;
        }

        textField.text = chatText;
	}
}
