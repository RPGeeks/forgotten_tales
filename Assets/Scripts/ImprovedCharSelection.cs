using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ImprovedCharSelection : MonoBehaviour
{
    private GameObject[] knights;
    private GameObject[] archers;
    private GameObject[] mages;
    private GameObject[,] characterList;
    private int classIndex = 0;
    private int colorIndex = 0;
    private GameObject spawnObject;
    private Transform spawnTransform;

    private void Start()
    {
        // characterList is a matrix where I save all the skins 
        // on the first row knights
        // second archers 
        // third mages

        knights = Resources.LoadAll<GameObject>("KnightSkins");
        archers = Resources.LoadAll<GameObject>("ArcherSkins");
        mages = Resources.LoadAll<GameObject>("MageSkins");

        characterList = new GameObject[3, Math.Max(Math.Max(knights.Length, archers.Length), mages.Length) ];

        for (int skin = 0; skin < knights.Length; skin++)
        {
            characterList[0, skin] = knights[skin];
        }
        for (int skin = 0; skin < archers.Length; skin++)
        {
            characterList[1, skin] = archers[skin];
        }
        for (int skin = 0; skin < mages.Length; skin++)
        {
            characterList[2, skin] = mages[skin];
        }

        spawnObject = GameObject.Find("SpawnPoint");
        spawnTransform = spawnObject.transform;
        GameObject.Instantiate(characterList[0, 0], spawnTransform);

    }

    
    private void Update()
    {
        if (Input.GetMouseButton(0))
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f));
    }

    public void Select(int askedIndex)
    {
        if (askedIndex == classIndex && colorIndex == 0)
        {
            return;
        }

        if (askedIndex < 0 || askedIndex >= 3)
        {
            return;
        }

        colorIndex = 0;
        if (characterList[classIndex, colorIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);
        classIndex = askedIndex;
        GameObject.Instantiate(characterList[classIndex, colorIndex], spawnTransform);
    }

    public void ToggleLeft()
    {
        //  Destroy the current model
        if(characterList[classIndex, colorIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);

        colorIndex--;
        switch (classIndex)
        {
            case 0:
                if (colorIndex < 0)
                    colorIndex = knights.Length - 1;
                break;
            case 1:
                if (colorIndex < 0)
                    colorIndex = archers.Length - 1;
                break;
            case 2:
                if (colorIndex < 0)
                    colorIndex = mages.Length - 1;
                break;
            default:
                break;
        }

        // Toggle on the new model
        GameObject.Instantiate(characterList[classIndex, colorIndex], spawnTransform);
    }

    public void ToggleRight()
    {
        // Destroy the Old Model 
        if (characterList[classIndex, colorIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);

        colorIndex++;
        switch (classIndex)
        {
            case 0:
                if (colorIndex >= knights.Length)
                    colorIndex = 0;
                break;
            case 1:
                if (colorIndex >= archers.Length)
                    colorIndex = 0;
                break;
            case 2:
                if (colorIndex >= mages.Length)
                    colorIndex = 0;
                break;
            default:
                break;
        }

        // Toggle on the new model
        GameObject.Instantiate(characterList[classIndex, colorIndex], spawnTransform);
    }


    public void StartGame()
    {
        PlayerPrefs.SetInt("ClassSelected", classIndex);
        PlayerPrefs.SetInt("ColorSelected", colorIndex);
        SceneManager.LoadScene("EmptyScene");
    }
}
