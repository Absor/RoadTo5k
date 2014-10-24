using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WatchMatchScript : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameStateManagerScript.Instance.GetGameTime().hour > 21)
        {
            Dialog dialog = new Dialog();
            dialog.dialogText = "You feel too tired for anything";
            DialogOption ok = new DialogOption();
            ok.optionText = "Ok";
            dialog.dialogOptions.Add(ok);
            DialogManagerScript.Instance.ShowDialog(dialog, noOp);
            return;
        }

        GameEvent watchEvent = RandomGeneratorScript.Instance.GetWatchEvent();
        GameEventManagerScript.Instance.ResolveGameEvent(watchEvent);

        GameStateManagerScript.Instance.AdvanceTime(Random.Range(30, 60));
    }

    private void noOp(DialogOption obj)
    {
    }
}
