using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class AttributeUiManagerScript : Singleton<AttributeUiManagerScript> {

    public GameObject skillUI;
    public GameObject slidersContainer;
    public GameObject sliderContainerPrefab;
    public Text gainText;
    // Texts
    private Text pointsAvailableText;
    private Dictionary<StatusType, Slider> sliders;
    private Dictionary<StatusType, TooltipOnPointerOverScript> tooltipScripts;
    private List<Button> addButtons;

    private bool skillUiActive = true;

    void Start()
    {
        pointsAvailableText = GetComponentInChildren<Text>();
        sliders = new Dictionary<StatusType, Slider>();
        tooltipScripts = new Dictionary<StatusType, TooltipOnPointerOverScript>();
        addButtons = new List<Button>();
        Slider rageSlider = GetComponentInChildren<Slider>();
        sliders.Add(StatusType.Rage, rageSlider);
        TooltipOnPointerOverScript rageTooltipScript = rageSlider.GetComponentInChildren<TooltipOnPointerOverScript>();
        tooltipScripts.Add(StatusType.Rage, rageTooltipScript);
        gainText.color = new Color(0, 0, 0, 0);

        List<StatusType> attributes = new List<StatusType>();
        attributes.Add(StatusType.Charisma);
        attributes.Add(StatusType.Luck);
        attributes.Add(StatusType.Talent);
        attributes.Add(StatusType.Knowledge_Carry);
        attributes.Add(StatusType.Knowledge_Ganker);
        attributes.Add(StatusType.Knowledge_Support);
        foreach (StatusType type in attributes)
        {
            if (type != StatusType.Rage)
            {
                GameObject sliderContainer = Instantiate(sliderContainerPrefab) as GameObject;
                sliderContainer.transform.SetParent(slidersContainer.transform, false);
                Slider slider = sliderContainer.GetComponentInChildren<Slider>();
                sliders.Add(type, slider);
                Button addButton = sliderContainer.GetComponentInChildren<Button>();
                addButtons.Add(addButton);
                TooltipOnPointerOverScript tooltipScript = sliderContainer.GetComponentInChildren<TooltipOnPointerOverScript>();
                tooltipScripts.Add(type, tooltipScript);
                StatusType saveType = type;
                addButton.onClick.AddListener(() => addSkill(saveType));
                sliderContainer.GetComponentInChildren<Text>().text = type.ToNiceString();
            }            
        }

        ToggleSkillUi();
        gameStateUpdated();
        GameStateManagerScript.Instance.OnGameStateUpdate.AddListener(gameStateUpdated);
    }

    private void addSkill(StatusType type)
    {
        GameStateManagerScript.Instance.AssignPoint(type);
    }

    private void gameStateUpdated()
    {
        updateSliders();
        updateTooltips();

        if (GameStateManagerScript.Instance.GetAvailableSkillPoints() == 0)
        {
            setAddButtonsInteractable(false);
        }

        // Text
        pointsAvailableText.text = GameStateManagerScript.Instance.GetAvailableSkillPoints() + " skill points available";
    }

    private void updateSliders()
    {
        foreach (StatusType key in sliders.Keys)
        {
            sliders[key].value = GameStateManagerScript.Instance.GetStatus(key).GetNormalizedPoints();
        }
    }

    private void setAddButtonsInteractable(bool interactable)
    {
        foreach (Button button in addButtons)
        {
            button.interactable = interactable;
        }
    }

    public void ToggleSkillUi()
    {
        skillUiActive = !skillUiActive;
        skillUI.SetActive(skillUiActive);
    }

    private void updateTooltips()
    {
        foreach (StatusType key in tooltipScripts.Keys)
        {
            tooltipScripts[key].tooltipText = key.ToNiceString();
        }
    }

    private bool transitioning = false;
    private Stack<string> texts = new Stack<string>();

    public void PointsGained(string text)
    {
        texts.Push(text);
        if (!transitioning)
        {
            transitioning = true;
            StartCoroutine(gainAnimation());
        }        
    }

    private IEnumerator gainAnimation()
    {
        while (texts.Count > 0)
        {
            string text = texts.Pop();
            gainText.text = text;

            float t = 0.0f;
            Vector3 startingPos = gainText.rectTransform.position;
            Vector3 endPos = startingPos + Vector3.up * 50;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / 2f);

                gainText.color = Color.Lerp(Color.white, new Color(0, 0, 0, 0), t);
                gainText.rectTransform.position = Vector3.Lerp(startingPos, endPos, t);
                yield return 0;
            }
            gainText.rectTransform.position = startingPos;
            transitioning = false;
        }        
    }
}
