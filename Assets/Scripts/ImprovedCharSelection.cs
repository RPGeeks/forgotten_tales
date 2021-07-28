using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ImprovedCharSelection : MonoBehaviour
{
    public GameObject[] knights;
    public GameObject[] archers;
    public GameObject[] mages;
    private GameObject[,] characterList;
    private int classIndex = 0;
    private int raceIndex = 0;
    private int genderIndex = 0;
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
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(new Vector3(0.0f, transform.localRotation.eulerAngles.x + 0.2f, 0.0f));
         if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(new Vector3(0.0f, transform.localRotation.eulerAngles.x - 0.2f, 0.0f));
    }

    public void SelectClass(int askedIndex)
    {
        if (askedIndex < 0 || askedIndex >= 3)
        {
            return;
        }

        if (characterList[classIndex, 2 * raceIndex + genderIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);
        classIndex = askedIndex;
        GameObject.Instantiate(characterList[classIndex, 2 * raceIndex + genderIndex], spawnTransform);
    }

    public void SelectRace(int askedIndex)
    {
        if (askedIndex < 0 || askedIndex >= 5)
        {
            return;
        }

        if (characterList[classIndex, 2 * raceIndex + genderIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);
        raceIndex = askedIndex;
        GameObject.Instantiate(characterList[classIndex, 2 * raceIndex + genderIndex], spawnTransform);
    }

    public void SelectGender(int askedIndex)
    {
        if (askedIndex < 0 || askedIndex >= 2)
        {
            return;
        }

        if (characterList[classIndex, 2 * raceIndex + genderIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);
        genderIndex = askedIndex;
        GameObject.Instantiate(characterList[classIndex, 2 * raceIndex + genderIndex], spawnTransform);
    }

    public void ToggleLeft()
    {
        //  Destroy the current model
        if(characterList[classIndex, raceIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);

        raceIndex--;
        switch (classIndex)
        {
            case 0:
                if (raceIndex < 0)
                    raceIndex = knights.Length - 1;
                break;
            case 1:
                if (raceIndex < 0)
                    raceIndex = archers.Length - 1;
                break;
            case 2:
                if (raceIndex < 0)
                    raceIndex = mages.Length - 1;
                break;
            default:
                break;
        }

        // Toggle on the new model
        GameObject.Instantiate(characterList[classIndex, raceIndex], spawnTransform);
    }

    public void ToggleRight()
    {
        // Destroy the Old Model 
        if (characterList[classIndex, raceIndex])
            Destroy(spawnTransform.GetChild(0).gameObject);

        raceIndex++;
        switch (classIndex)
        {
            case 0:
                if (raceIndex >= knights.Length)
                    raceIndex = 0;
                break;
            case 1:
                if (raceIndex >= archers.Length)
                    raceIndex = 0;
                break;
            case 2:
                if (raceIndex >= mages.Length)
                    raceIndex = 0;
                break;
            default:
                break;
        }

        // Toggle on the new model
        GameObject.Instantiate(characterList[classIndex, raceIndex], spawnTransform);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("ClassSelected", classIndex);
        PlayerPrefs.SetInt("RaceSelected", raceIndex);
        PlayerPrefs.SetInt("GenderSelected", genderIndex);
        SceneManager.LoadScene("EmptyScene");
    }
}
