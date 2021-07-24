using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public float SliderMaxValue
    {
        set
        {
            slider.maxValue = value;
            slider.value = value;
        }
    }

    public float SliderValue
    {
        set
        {
            slider.value = value;
        }
    }
}
