using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderInputScript : MonoBehaviour {

    public Slider farmSlider;
    public Slider pushSlider;
    public Slider gankSlider;

    private float farm = 0.33f;
    private float push = 0.33f;
    private float gank = 1f - 0.33f - 0.33f;

    void Start()
    {
        updateSliderValues();
        farmSlider.onValueChanged.AddListener(farmSliderChanged);
        pushSlider.onValueChanged.AddListener(pushSliderChanged);
        gankSlider.onValueChanged.AddListener(gankSliderChanged);
    }

    private void updateSliderValues()
    {
        farmSlider.value = farm;
        pushSlider.value = push;
        gankSlider.value = gank;
    }

    private void farmSliderChanged(float newFarm)
    {
        float change = farm - newFarm;
        farm = newFarm;
        if (0 > push + (change / 2))
        {
            change += push;
            push = 0;
            gank = Mathf.Max(0f, gank + change);
        } else if (0 > gank + (change / 2)) {
            change += gank;
            gank = 0;
            push = Mathf.Max(0f, push + change);
        }
        else
        {
            push += change / 2;
            gank += change / 2;
        }
        updateSliderValues();
    }

    private void pushSliderChanged(float newPush)
    {
        float change = push - newPush;
        push = newPush;
        if (0 > farm + (change / 2))
        {
            change += farm;
            farm = 0;
            gank = Mathf.Max(0f, gank + change);
        }
        else if (0 > gank + (change / 2))
        {
            change += gank;
            gank = 0;
            farm = Mathf.Max(0f, farm + change);
        }
        else
        {
            farm += change / 2;
            gank += change / 2;
        }
        updateSliderValues();
    }

    private void gankSliderChanged(float newGank)
    {
        float change = gank - newGank;
        gank = newGank;
        if (0 > farm + (change / 2))
        {
            change += farm;
            farm = 0;
            push = Mathf.Max(0f, push + change);
        }
        else if (0 > push + (change / 2))
        {
            change += push;
            push = 0;
            farm = Mathf.Max(0f, farm + change);
        }
        else
        {
            farm += change / 2;
            push += change / 2;
        }
        updateSliderValues();
    }

    public float GetFarm()
    {
        return farm;
    }

    public float GetPush()
    {
        return push;
    }

    public float GetGank()
    {
        return gank;
    }
}
