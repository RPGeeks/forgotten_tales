using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImprovedCharSelection : MonoBehaviour
{
    private GameObject[,] characterList;
    private int classIndex = 0;
    private int colorIndex = -1;

    private void Start()
    {
        classIndex = PlayerPrefs.GetInt("ClassSelected");
        colorIndex = PlayerPrefs.GetInt("ColorSelected");

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

        if(colorIndex == -1 && characterList[0, classIndex])
        {
            characterList[0, classIndex].SetActive(true);
        }
        else if (characterList[classIndex + 1, colorIndex])
        {
            characterList[classIndex+1, colorIndex].SetActive(true);
        }
           
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f));
    }

    public void Select(int askedIndex)
    {
        if (askedIndex == classIndex && colorIndex == -1)
        {
            return;
        }

        if (askedIndex < 0 || askedIndex >= 3)
        {
            return;
        }

        if(colorIndex == -1)
        {
            if (characterList[0, classIndex])
                characterList[0, classIndex].SetActive(false);
            classIndex = askedIndex;
            colorIndex = -1;
            characterList[0, classIndex].SetActive(true);
        }
        else
        {
            if (characterList[classIndex + 1, colorIndex])
                characterList[classIndex + 1, colorIndex].SetActive(false);
            classIndex = askedIndex;
            colorIndex = -1;
            characterList[0, classIndex].SetActive(true);
        }
        
    }

    public void ToggleLeft()
    {
        //  Toggle off the current model
        if(colorIndex == -1 && characterList[0, classIndex])
            characterList[0, classIndex].SetActive(false);
        else
            characterList[classIndex + 1, colorIndex].SetActive(false);

        colorIndex--;
        if (colorIndex < 0)
            colorIndex = characterList.GetLength(1) - 1;

        // Toggle on the new model
        characterList[classIndex + 1, colorIndex].SetActive(true);
        }

    public void ToggleRight()
        {
        //  Toggle off the current model
        if (colorIndex == -1 && characterList[0, classIndex])
            characterList[0, classIndex].SetActive(false);
        else
            characterList[classIndex + 1, colorIndex].SetActive(false);

        colorIndex++;
           if (colorIndex >= characterList.GetLength(1))
                colorIndex = 0;

            // Toggle on the new model
            characterList[classIndex + 1, colorIndex].SetActive(true);
        }


    public void StartGame()
    {
        PlayerPrefs.SetInt("ClassSelected", classIndex);
        PlayerPrefs.SetInt("ColorSelected", colorIndex);
        SceneManager.LoadScene("EmptyScene");
    }

}
