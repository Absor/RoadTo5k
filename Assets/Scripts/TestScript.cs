using UnityEngine;
using System.Collections;
using LitJson;

public class TestScript : MonoBehaviour {

	void Start () {
        GameEvent ge = new GameEvent();

        Dialog daily = new Dialog();
        daily.dialogText = "You get home from work, you feel bored. You decide to aim for 5k rating in MOBA. You feel a bit better about yourself for making this great decision.";
        DialogOption ok = new DialogOption();
        ok.optionText = "Ok";
        ok.optionId = "positive";
        daily.dialogOptions.Add(ok);
        ge.dialog = daily;

        GameEventOutcome outcome = new GameEventOutcome();

        ge.outcomes.Add("positive", outcome);

        string json = JsonMapper.ToJson(ge);

        //Debug.Log(json);
	}
}
