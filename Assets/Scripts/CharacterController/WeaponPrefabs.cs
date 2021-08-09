using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponPrefabs", menuName = "ScriptableObjects/WeaponPrefabs", order = 2)]
public class WeaponPrefabs : ScriptableObject
{
    public GameObject sword;
    public GameObject bow;
    public GameObject sceptre;

    public GameObject GetWeapon(CharacterClass type)
    {
        switch(type)
        {
            case CharacterClass.Knight:
                return sword;
            case CharacterClass.Archer:
                return bow;
            case CharacterClass.Mage:
                return sceptre;
        }

        return sword;
    }
}
