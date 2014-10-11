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

        List<StatusType> attributes = new List<StatusType>();
        attributes.Add(StatusType.Charisma);
        attributes.Add(StatusType.Luck);
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
}
