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
}
