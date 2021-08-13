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
        ConfigureWeapon();
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

    #region WEAPON_CONFIGURATION

    [SerializeField] private WeaponPrefabs weaponPrefabs;

    [SyncVar(hook = nameof(SetClass))]
    int classIndex = 0;

    void SetClass(int oldClass, int newClass)
    {
        ChangeWeaponTo(newClass);
    }

    public void ChangeWeaponTo(int weapon)
    {
        Transform grip = rigParts.rightHand.Find("weapon-grip");


        for (int i = grip.childCount - 1; i >= 0; i--)
        {
            Destroy(grip.GetChild(i).gameObject);
        }

        GameObject newWeaponPrefab = weaponPrefabs.GetWeapon((CharacterClass)weapon);
        Instantiate(newWeaponPrefab, grip);
    }

    private void ConfigureWeapon()
    {
        classIndex = PlayerPrefs.GetInt("ClassSelected", 0);

        CmdSetWeapon(classIndex);

        ChangeWeaponTo(classIndex);
    }

    [Command]
    public void CmdSetWeapon(int _classIndex)
    {
        this.classIndex = _classIndex;
    }

    public int GetClassIndex()
    {
        return classIndex;
    }

    #endregion WEAPON_CONFIGURATION
}
