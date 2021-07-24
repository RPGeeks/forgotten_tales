using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImprovedCharSelection : MonoBehaviour
{
    private GameObject[,] characterList;
    private int class_index = 0;
    private int color_index = -1;

    private void Start()
    {
        class_index = PlayerPrefs.GetInt("ClassSelected");
        color_index = PlayerPrefs.GetInt("ColorSelected");

        // presupun ca nu vor fi mai mult de 5 skinuri
        // in principiu vom avea fix 3
        characterList = new GameObject[transform.childCount, 3];
        
        for(int j = 0; j < transform.childCount; j++)
        {
            Transform child = transform.GetChild(j);
            for (int i = 0; i < child.transform.childCount; i++)
            {
                characterList[j, i] = child.transform.GetChild(i).gameObject;
            }
        }
        
        foreach(GameObject go in characterList)
        {
            if (go != null)
                go.SetActive(false);
        }

        if(color_index == -1 && characterList[0, class_index])
        {
            characterList[0, class_index].SetActive(true);
        }
        else if (characterList[class_index + 1, color_index])
        {
            characterList[class_index+1, color_index].SetActive(true);
        }
           
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f));
    }

    public void Select(int asked_index)
    {
        if (asked_index == class_index && color_index == -1)
        {
            return;
        }

        if (asked_index < 0 || asked_index >= 3)
        {
            return;
        }

        if(color_index == -1)
        {
            if (characterList[0, class_index])
                characterList[0, class_index].SetActive(false);
            class_index = asked_index;
            color_index = -1;
            characterList[0, class_index].SetActive(true);
        }
        else
        {
            if (characterList[class_index + 1, color_index])
                characterList[class_index + 1, color_index].SetActive(false);
            class_index = asked_index;
            color_index = -1;
            characterList[0, class_index].SetActive(true);
        }
        
    }

    public void ToggleLeft()
    {
        //  Toggle off the current model
        if(color_index == -1 && characterList[0, class_index])
            characterList[0, class_index].SetActive(false);
        else
            characterList[class_index + 1, color_index].SetActive(false);

        color_index--;
        if (color_index < 0)
            color_index = characterList.GetLength(1) - 1;

        // Toggle on the new model
        characterList[class_index + 1, color_index].SetActive(true);
        }

    public void ToggleRight()
        {
        //  Toggle off the current model
        if (color_index == -1 && characterList[0, class_index])
            characterList[0, class_index].SetActive(false);
        else
            characterList[class_index + 1, color_index].SetActive(false);

        color_index++;
           if (color_index >= characterList.GetLength(1))
                color_index = 0;

            // Toggle on the new model
            characterList[class_index + 1, color_index].SetActive(true);
        }


    public void StartGame()
    {
        PlayerPrefs.SetInt("ClassSelected", class_index);
        PlayerPrefs.SetInt("ColorSelected", color_index);
        SceneManager.LoadScene("EmptyScene");
    }

}
