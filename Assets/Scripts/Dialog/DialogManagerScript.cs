using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogManagerScript : MonoBehaviour {

    public GameObject dialogPanel;
    public GameObject dialogButtonPrefab;
    public GameObject dialogButtonContainer;
    public CameraScript cameraScript;

    void Awake()
    {
        dialogPanel.SetActive(false);
    }

    public void ShowDialog(List<DialogOption> dialogOptions, Action<DialogOption> done)
    {
        List<GameObject> dialogButtons = new List<GameObject>();
        foreach (Transform child in dialogButtonContainer.transform)
        {
            dialogButtons.Add(child.gameObject);
        }
        dialogButtons.ForEach(child => Destroy(child));

        dialogOptions.ForEach(option =>
        {
            GameObject dialogButton = Instantiate(dialogButtonPrefab) as GameObject;
            dialogButton.transform.parent = dialogButtonContainer.transform;
            dialogButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                dialogPanel.SetActive(false);
                cameraScript.SetCanTransition(true);
                done(option);
            });
        });

        // Doesn't update parents instantly (content fitter doesn't notice), so wait a moment before showing
        StartCoroutine(activateDialog());
    }

    private IEnumerator activateDialog()
    {
        yield return new WaitForEndOfFrame();
        dialogPanel.SetActive(true);
        cameraScript.SetCanTransition(false);
    }
}
