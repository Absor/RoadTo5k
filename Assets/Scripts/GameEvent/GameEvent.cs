using System.Collections.Generic;
public class GameEvent
{
    public Dialog dialog;
    public Dictionary<string, List<GameEventOutcome>> outcomes;

    public GameEvent()
    {
        outcomes = new Dictionary<string, List<GameEventOutcome>>();
    }

    public void Reset()
    {
        foreach (string key in outcomes.Keys)
        {
            foreach (GameEventOutcome outcome in outcomes[key])
            {
                outcome.Reset();
            }
        }
        setupTooltips();
    }

    private void setupTooltips()
    {
        foreach (DialogOption option in dialog.dialogOptions)
        {
            option.tooltipText = "";
            foreach (GameEventOutcome outcome in outcomes[option.optionId])
            {
                option.tooltipText += outcome.GetTooltipText();
            }
            option.tooltipText.TrimEnd(new char[]{'\n'});

            if (option.tooltipText.Trim().Length == 0)
            {
                option.tooltipText = null;
            }
        }
    }

    public bool HasDialog()
    {
        return dialog != null;
    }
}