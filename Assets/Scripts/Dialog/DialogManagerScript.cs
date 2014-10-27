using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogManagerScript : Singleton<DialogManagerScript> {

    public GameObject dialogContainer;
    public GameObject dialogButtonPrefab;
    public GameObject dialogButtonContainer;
    public Text dialogText;

    private Stack<Dialog> toDo;
    private Dictionary<Dialog, Action<DialogOption>> toDones;

    void Awake()
    {
        dialogContainer.SetActive(false);
        toDo = new Stack<Dialog>();
        toDones = new Dictionary<Dialog, Action<DialogOption>>();
    }

    public void ShowDialog(Dialog dialog, Action<DialogOption> done)
    {
        toDo.Push(dialog);
        toDones[dialog] = done;

        resolveWaitingDialogs();
    }

    private void resolveWaitingDialogs()
    {
        if (!showing && toDo.Count > 0)
        {
            Dialog dialog = toDo.Pop();
            Action<DialogOption> done = toDones[dialog];
            toDones.Remove(dialog);
            ShowDialogus(dialog, done);
        }
    }

    private bool showing = false;

    private void ShowDialogus(Dialog dialog, Action<DialogOption> done)
    {
        showing = true;
        dialogText.text = dialog.dialogText;

        List<GameObject> dialogButtons = new List<GameObject>();
        foreach (Transform child in dialogButtonContainer.transform)
        {
            dialogButtons.Add(child.gameObject);
        }
        dialogButtons.ForEach(child => Destroy(child));

        dialog.dialogOptions.ForEach(option =>
        {
            GameObject dialogButton = Instantiate(dialogButtonPrefab) as GameObject;
            Text buttonText = dialogButton.GetComponentInChildren<Text>();
            buttonText.text = option.optionText;
            dialogButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                dialogContainer.SetActive(false);
                done(option);
                showing = false;
                resolveWaitingDialogs();
            });
            dialogButton.transform.parent = dialogButtonContainer.transform;
            TooltipOnPointerOverScript tooltipScript = dialogButton.GetComponent<TooltipOnPointerOverScript>();
            if (option.tooltipText != null)
            {
                tooltipScript.tooltipText = option.tooltipText;
            }
            else
            {
                Destroy(tooltipScript);
            }
        });

        // Doesn't update parents instantly (content fitter doesn't notice), so wait a moment before showing
        StartCoroutine(activateDialog());
    }

    private IEnumerator activateDialog()
    {
        yield return new WaitForEndOfFrame();
        dialogContainer.SetActive(true);
    }
}