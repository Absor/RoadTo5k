using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PizzaScript : MonoBehaviour, IPointerClickHandler {

    private int slices = 2;
    private TooltipOnPointerOverScript scripta;

    public AudioSource eatSound;

    void Awake()
    {
        scripta = GetComponent<TooltipOnPointerOverScript>();
        updateTooltip();
    }

    private void updateTooltip()
    {
        scripta.tooltipText = "Eat pizza, slices left: " + slices;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slices > 0)
        {
            slices--;
            eatSound.Play();
            Status food = GameStateManagerScript.Instance.GetStatus(StatusType.Food);

            GameStateManagerScript.Instance.Eat(Random.Range(40, 61));
            updateTooltip();
        }
        else
        {
            Dialog dialog = new Dialog();
            dialog.dialogText = "You have no more pizza.";
            DialogOption option = new DialogOption();
            option.optionText = "Ok";
            option.optionId = "ok";
            dialog.dialogOptions.Add(option);
            if (GameStateManagerScript.Instance.GetStatus(StatusType.Money).points >= 20)
            {
                option = new DialogOption();
                option.optionText = "Order more";
                option.optionId = "order";
                option.tooltipText = "-20$";
                dialog.dialogOptions.Add(option);
            }            
            DialogManagerScript.Instance.ShowDialog(dialog, optionChosen);
        }
    }

    private void optionChosen(DialogOption option)
    {
        if (option.optionId == "order")
        {
            slices = 2;
            GameStateManagerScript.Instance.ChangeMoney(-20);
            updateTooltip();
        }
    }
}
