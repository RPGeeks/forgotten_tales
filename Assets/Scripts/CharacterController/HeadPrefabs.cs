using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeadPrefabs", menuName = "ScriptableObjects/HeadPrefabs", order = 1)]
public class HeadPrefabs : ScriptableObject
{
    public GameObject human_male;
    public GameObject human_female;
    public GameObject dwarf_male;
    public GameObject dwarf_female;
    public GameObject danari_male;
    public GameObject danari_female;
    public GameObject elf_male;
    public GameObject elf_female;
    public GameObject orc_male;
    public GameObject orc_female;

    public GameObject GetHead(Gender gender, CharacterRace race)
    {
        if (gender == Gender.Male)
        {
            switch (race)
            {
                case CharacterRace.Human:
                    return human_male;
                case CharacterRace.Dwarf:
                    return dwarf_male;
                case CharacterRace.Danari:
                    return danari_male;
                case CharacterRace.Elf:
                    return elf_male;
                case CharacterRace.Orc:
                    return orc_male;
            }
        }

        if (gender == Gender.Female)
        {
            switch (race)
            {
                case CharacterRace.Human:
                    return human_female;
                case CharacterRace.Dwarf:
                    return dwarf_female;
                case CharacterRace.Danari:
                    return danari_female;
                case CharacterRace.Elf:
                    return elf_female;
                case CharacterRace.Orc:
                    return orc_female;
            }
        }

        // default
        return human_male;
    }
}
