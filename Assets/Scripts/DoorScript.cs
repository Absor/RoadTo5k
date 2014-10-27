using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DoorScript : MonoBehaviour, IPointerClickHandler {

    private int day;
    private bool workedToday;
    private int worksMissed;
    private bool allowWork;

    void Start()
    {
        day = GameStateManagerScript.Instance.GetGameTime().day;
        GameStateManagerScript.Instance.OnGameStateUpdate.AddListener(gameStateUpdated);
        workedToday = true;
        worksMissed = 0;
        allowWork = true;
    }

    private void gameStateUpdated()
    {
        if (!allowWork)
        {
            return;
        }
        int oldDay = day;
        int newDay = GameStateManagerScript.Instance.GetGameTime().day;
        day = newDay;
        if (newDay != oldDay && workedToday == false)
        {
            Debug.Log(oldDay + " " + newDay);
            for (int i = day; i <= newDay; i++)
            {
                worksMissed++;
                Debug.Log(worksMissed);
            }
        }

        if (worksMissed >= 3)
        {
            Dialog dialog = new Dialog();
            dialog.dialogText = "Your boss called and fired you. You missed work too many times this month.";
            DialogOption option = new DialogOption();
            option.optionText = "Ok";
            dialog.dialogOptions.Add(option);
            DialogManagerScript.Instance.ShowDialog(dialog, noOp);
            allowWork = false;
            gameObject.SetActive(false);
        }

        if (newDay != oldDay)
        {
            workedToday = false;
        }
    }

    private void noOp(DialogOption obj)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (workedToday)
        {
            Dialog dialog = new Dialog();
            dialog.dialogText = "You already went to work today.";
            DialogOption option = new DialogOption();
            option.optionText = "Ok";
            dialog.dialogOptions.Add(option);
            DialogManagerScript.Instance.ShowDialog(dialog, noOp);
        } 
        else if (allowWork && GameStateManagerScript.Instance.GetGameTime().hour > 14)
        {
            Dialog dialog = new Dialog();
            dialog.dialogText = "It's too late to go to work now.";
            DialogOption option = new DialogOption();
            option.optionText = "Ok";
            dialog.dialogOptions.Add(option);
            DialogManagerScript.Instance.ShowDialog(dialog, noOp);
        }
        else if (allowWork)
        {
            Dialog dialog = new Dialog();
            dialog.dialogText = "You went to work, you earned 20$.";
            DialogOption option = new DialogOption();
            option.optionText = "Ok";
            dialog.dialogOptions.Add(option);
            DialogManagerScript.Instance.ShowDialog(dialog, noOp);
            workedToday = true;
            GameStateManagerScript.Instance.AdvanceTime(8 * 60 + Random.Range(0, 30));
            GameStateManagerScript.Instance.ChangeMoney(20);
        }
    }
}
