using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class FromCharSelectionStartGame : MonoBehaviour
{
    private GameObject[] knights;
    private GameObject[] archers;
    private GameObject[] mages;
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
        classIndex = PlayerPrefs.GetInt("ClassSelected", classIndex);
        raceIndex = PlayerPrefs.GetInt("RaceSelected", raceIndex);
        genderIndex = PlayerPrefs.GetInt("GenderSelected", genderIndex);

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
        GameObject.Instantiate(characterList[classIndex, 2 * raceIndex + genderIndex], spawnTransform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
