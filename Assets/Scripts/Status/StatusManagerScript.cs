using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusManagerScript : Singleton<StatusManagerScript> {

    public PlayerScript playerScript;
    public TooltipManagerScript tooltipManagerScript;
    public GameObject skillUI;
    // Slider containers
    public GameObject rageSliderContainer;
    public GameObject luckSliderContainer;
    public GameObject charismaSliderContainer;
    // Sliders
    private Slider rageSlider;
    private Slider luckSlider;
    private Slider charismaSlider;
    // Add buttons
    private Button luckAddButton;
    private Button charismaAddButton;
    // Texts
    public Text pointsAvailableText;

    private bool skillUiActive = true;

    void Start()
    {
        // Rage stuff
        rageSlider = rageSliderContainer.GetComponentInChildren<Slider>();
        // Luck stuff
        luckSlider = luckSliderContainer.GetComponentInChildren<Slider>();
        luckAddButton = luckSliderContainer.GetComponentInChildren<Button>();
        luckAddButton.onClick.AddListener(() => addSkill(StatusType.LUCK));
        luckSliderContainer.GetComponentInChildren<Text>().text = "Luck";

        // Charisma stuff
        charismaSlider = charismaSliderContainer.GetComponentInChildren<Slider>();
        charismaAddButton = charismaSliderContainer.GetComponentInChildren<Button>();
        charismaAddButton.onClick.AddListener(() => addSkill(StatusType.CHARISMA));
        charismaSliderContainer.GetComponentInChildren<Text>().text = "Charisma";

        ToggleSkillUi();
        playerScriptUpdated();
    }

    private void addSkill(StatusType type)
    {
        if (playerScript.skillPointsAvailable >= playerScript.assignAmount)
        {
            switch (type)
            {
                case StatusType.CHARISMA:
                    playerScript.charisma += playerScript.assignAmount;
                    break;
                case StatusType.LUCK:
                    playerScript.luck += playerScript.assignAmount;
                    break;
            }
            playerScript.skillPointsAvailable -= playerScript.assignAmount;
            playerScriptUpdated();
        }
    }

    private void playerScriptUpdated()
    {
        updateSliders();
        
        if (playerScript.skillPointsAvailable == 0)
        {
            setAddButtonsInteractable(false);
        }

        // Text
        pointsAvailableText.text = (playerScript.skillPointsAvailable / playerScript.assignAmount) + " skill points available";
    }

    private void updateSliders()
    {
        // TODO add all sliders here
        rageSlider.value = playerScript.GetNormalizedRage();
        luckSlider.value = playerScript.GetNormalizedLuck();
        charismaSlider.value = playerScript.GetNormalizedCharisma();
    }

    private void setAddButtonsInteractable(bool interactable)
    {
        // TODO add all buttons here
        luckAddButton.interactable = interactable;
        charismaAddButton.interactable = interactable;
    }

    public void ToggleSkillUi()
    {
        skillUiActive = !skillUiActive;
        skillUI.SetActive(skillUiActive);
    }

    // For unity fails
    public void ShowStatusToolTip(string type)
    {
        switch (type)
        {
            case "Rage":
                showStatusTooltip(StatusType.RAGE);
                break;
        }
    }

    private void showStatusTooltip(StatusType type)
    {
        switch (type)
        {
            case StatusType.RAGE:
                tooltipManagerScript.ShowTooltip("Rage: " + playerScript.rage + "/" + playerScript.maxRage);
                break;
        }
    }

    public void HideTooltip()
    {
        tooltipManagerScript.HideToolTip();
    }

    public void ApplyOutcome(GameEventOutcome outcome)
    {
        updateSliders();
    }
}
