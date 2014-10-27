using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour {

    private Text moneyText;

    void Awake()
    {
        moneyText = GetComponent<Text>();

        GameStateManagerScript.Instance.OnGameStateUpdate.AddListener(onGameStateChanged);
    }

    private void onGameStateChanged()
    {
        int money = GameStateManagerScript.Instance.GetStatus(StatusType.Money).points;
        moneyText.text = money + "$";
    }
}
