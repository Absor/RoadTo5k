using System.Collections.Generic;
public class Dialog {
    public string dialogText;
    public List<DialogOption> dialogOptions;

    public Dialog()
    {
        dialogOptions = new List<DialogOption>();
    }
}