using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class ChangeColorScript : MonoBehaviour
{
    public Text text;
    public Color selectedColor;
    public GameObject[] allButtonsInThisCategory;
    public GameObject[] children;
    public TextMeshProUGUI[] texts;
    public int currentIndex;

    // Start is called before the first frame update
    public void Start()
    {
        int index = 0;
        children = new GameObject[10];
        texts = new TextMeshProUGUI[10];

        //allButtonsInThisCategory = GameObject.FindGameObjectsWithTag(gameObject.transform.GetChild(0).gameObject.tag);
        foreach (GameObject gameObject in allButtonsInThisCategory)
        {
            children[index] = gameObject.transform.GetChild(0).gameObject;
            texts[index] = children[index].GetComponent<TextMeshProUGUI>();
            index++;

        }
    }

    public void ChangeColor(int selectedIndex)
    {
        selectedColor = new Color(1, 0, 0, 1);
        Color white = new Color(1, 1, 1, 1);
        texts[currentIndex].color = white;
        texts[selectedIndex].color = selectedColor;
        currentIndex = selectedIndex;
    }
}
