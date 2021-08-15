using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    private Camera myCamera;

    public float FillAmount {
        get { return foregroundImage.fillAmount; }
        set { foregroundImage.fillAmount = value; }
    }

    private void Start()
    {
        myCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(myCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}
