using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour, IChatScript {

    private Text textField;

    private Button[] buttons;

    void Awake()
    {
        textField = GetComponentInChildren<Text>();
        buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(chatButtonClicked);
        }
    }

    public void UpdateMessages(List<ChatMessage> messages)
    {
	    string chatText = "";
        foreach (ChatMessage message in messages)
        {
            chatText += "\n" + message.ToString();
        }

        textField.text = chatText;
	}

    private void chatButtonClicked()
    {
        MatchScript.Instance.InteractWithChat();
    }
}
