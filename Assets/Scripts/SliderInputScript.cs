using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderInputScript : MonoBehaviour {

    public Slider farmSlider;
    public Slider pushSlider;
    public Slider gankSlider;

    void Start()
    {
        farmSlider.value = 0.33f;
        pushSlider.value = 0.33f;
        gankSlider.value = 0.33f;
        farmSlider.onValueChanged.AddListener(farmSliderChanged);
        pushSlider.onValueChanged.AddListener(pushSliderChanged);
        gankSlider.onValueChanged.AddListener(gankSliderChanged);
    }

    private void farmSliderChanged(float value)
    {

    }

    private void pushSliderChanged(float value)
    {

    }

    private void gankSliderChanged(float value)
    {

    }
}
