using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SleepScript : MonoBehaviour, IPointerClickHandler {

    public float sleepScreenFadeDuration;
    public string confirmationDialogText;
    public string positiveAnswerText;
    public string negativeAnswerText;

    private GameObject sleepPanel;
    private Image sleepPanelImage;

    void Start()
    {
        sleepPanel = GameObject.FindGameObjectWithTag("SleepPanel");
        sleepPanelImage = sleepPanel.GetComponent<Image>();
        sleepPanel.SetActive(false);
        showDialogForToday();
    }

    private void NoOp(DialogOption option)
    {
    }

    private void sleep()
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
            t += Time.deltaTime * (Time.timeScale / (sleepScreenFadeDuration / 2));

            sleepPanelImage.color = Color.Lerp(startColor, Color.black, t);

            yield return 0;
        }

        // Time advances!
        TimeManagerScript.Instance.SetTime(TimeManagerScript.Instance.day + 1, Random.Range(8, 12), Random.Range(0, 60));

        t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / (sleepScreenFadeDuration / 2));

            sleepPanelImage.color = Color.Lerp(Color.black, startColor, t);

            yield return 0;
        }

        sleepPanel.SetActive(false);
        showDialogForToday();
    }

    private void showDialogForToday()
    {
        GameEvent dailyEvent = RandomGeneratorScript.Instance.GetDailyEvent(TimeManagerScript.Instance.day);
        GameEventManagerScript.Instance.ResolveGameEvent(dailyEvent);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Dialog dialog = new Dialog();
        dialog.dialogText = confirmationDialogText;
        DialogOption yes = new DialogOption();
        yes.optionText = positiveAnswerText;
        DialogOption no = new DialogOption();
        no.optionText = negativeAnswerText;
        dialog.dialogOptions.Add(yes);
        dialog.dialogOptions.Add(no);
        DialogManagerScript.Instance.ShowDialog(dialog, checkForSleep);
    }

    private void checkForSleep(DialogOption option)
    {
        if (option.optionText == positiveAnswerText)
        {
            sleep();
        }
    }
}
