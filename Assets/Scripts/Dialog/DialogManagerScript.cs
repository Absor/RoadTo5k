﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogManagerScript : MonoBehaviour {

    public GameObject dialogContainer;
    public GameObject dialogButtonPrefab;
    public GameObject dialogButtonContainer;
    public Text dialogText;
    public CameraScript cameraScript;

    void Awake()
    {
        dialogContainer.SetActive(false);
    }

    public void ShowDialog(Dialog dialog, Action<DialogOption> done)
    {
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
                cameraScript.SetCanTransition(true);
                done(option);
            });
            dialogButton.transform.parent = dialogButtonContainer.transform;
        });

        // Doesn't update parents instantly (content fitter doesn't notice), so wait a moment before showing
        StartCoroutine(activateDialog());
    }

    private IEnumerator activateDialog()
    {
        yield return new WaitForEndOfFrame();
        dialogContainer.SetActive(true);
        cameraScript.SetCanTransition(false);
    }
}
