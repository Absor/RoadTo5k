using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SleepScript : MonoBehaviour {

    public TimeManagerScript timeManagerScript;
    public DialogManagerScript dialogManagerScript;
    public GameObject sleepPanel;
    public float fadeDuration;
    public TextAsset dailyDialogs;

    private Image sleepPanelImage;

    void Start()
    {
        sleepPanelImage = sleepPanel.GetComponent<Image>();
        sleepPanel.SetActive(false);
        showDialogForToday();
    }

    private void NoOp(DialogOption option)
    {
    }

    public void Sleep()
    {
        sleepPanel.SetActive(true);
        StartCoroutine(sleepCycle());
    }

    private IEnumerator sleepCycle()
    {
        Color startColor = sleepPanelImage.color;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / (fadeDuration / 2));

            sleepPanelImage.color = Color.Lerp(startColor, Color.black, t);

            yield return 0;
        }

        // Time advances!
        timeManagerScript.SetDayAndTime(timeManagerScript.day + 1, Random.Range(8, 12), Random.Range(0, 60));

        t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / (fadeDuration / 2));

            sleepPanelImage.color = Color.Lerp(Color.black, startColor, t);

            yield return 0;
        }

        sleepPanel.SetActive(false);
        showDialogForToday();
    }

    private void showDialogForToday()
    {

    }
}
