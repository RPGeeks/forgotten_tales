using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;

    public void SetMaxHealth(float percent)
    {
        foregroundImage.fillAmount = percent;
    }

    public void SetHealth(float percent)
    {
        foregroundImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
