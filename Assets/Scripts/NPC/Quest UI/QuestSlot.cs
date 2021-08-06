using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField] private Image image;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Color idleColor;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI status;

    void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
        defaultSprite = image.sprite;
        idleColor = image.color;

        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        status = transform.Find("Status").GetComponent<TextMeshProUGUI>();
    }

    public void AddItem(Goal newItem)
    {
        text.text = newItem.text;
        status.text = newItem.CurrAmount
                        + "/" + newItem.Ammount;
    }

    public void RemoveItem()
    {
        text.text = "";
        status.text = "";
        image.color = idleColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text == null || text.text.Length == 0)
            return;

        Color tempColor = idleColor;
        tempColor.a = 100;
        image.color = tempColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (text == null || text.text.Length == 0)
            return;

        image.color = idleColor;
    }
}