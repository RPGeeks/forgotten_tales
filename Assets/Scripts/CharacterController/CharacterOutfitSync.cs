using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterOutfitSync : NetworkBehaviour
{
    private HumanoidRigidRig rigParts;

    public void Awake()
    {
        rigParts = GetComponent<CharacterController>().rigParts;
    }

    public void LocalInit()
    {
        ConfigureHead();
    }

    #region HEAD_CONFIGURATION

    [SerializeField] private HeadPrefabs headPrefabs;
    
    [SyncVar(hook = nameof(SetGender))]
    int genderIndex = 0;

    [SyncVar(hook = nameof(SetRace))]
    int raceIndex = 0;

    [Command]
    public void CmdSetGender(int _genderIndex)
    {
        this.genderIndex = _genderIndex;
    }

    [Command]
    public void CmdSetRace(int _raceIndex)
    {
        this.raceIndex = _raceIndex;
    }

    [Command]
    public void CmdSetGenderAndRace(int _genderIndex, int _raceIndex)
    {
        this.genderIndex = _genderIndex;
        this.raceIndex = _raceIndex;
    }

    // Clientside SyncVar hook
    void SetGender(int oldGender, int newGender)
    {
        ChangeHeadTo(newGender, raceIndex);
    }

    // Clientside SyncVar hook
    void SetRace(int oldRace, int newRace)
    {
        ChangeHeadTo(genderIndex, newRace);
    }

    int genderCache = 0;
    int raceCache = 0;
    public void ChangeHeadTo(int gender, int race)
    {
        if (gender == genderCache && race == raceCache) { return; }

        genderCache = gender;
        raceCache = race;

        GameObject newHeadPrefab = headPrefabs.GetHead((Gender)gender, (CharacterRace)race);

        GameObject newHead = Instantiate(newHeadPrefab, transform);
        Destroy(rigParts.head.gameObject);
        rigParts.head = newHead.transform;
    }

    private void ConfigureHead()
    {
        raceIndex = PlayerPrefs.GetInt("RaceSelected", 0);
        genderIndex = PlayerPrefs.GetInt("GenderSelected", 0);

        CmdSetGenderAndRace(genderIndex, raceIndex);

        ChangeHeadTo(genderIndex, raceIndex);
    }
    #endregion HEAD_CONFIGURATION
}
