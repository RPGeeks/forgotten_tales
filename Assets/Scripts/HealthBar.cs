using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Billboard
{
    [SerializeField]
    private Image foregroundImage;

    public float FillAmount { get { return foregroundImage.fillAmount; } set { foregroundImage.fillAmount = value; } }
}
