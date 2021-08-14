using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public enum Gender
{
    Male,
    Female,
    AttackHelicopter
}

public enum CharacterClass
{
    Knight,
    Archer,
    Mage
}

public enum CharacterRace
{
    Human,
    Orc,
    Dwarf,
    Elf,
    Danari
}

public class ImprovedCharSelection : MonoBehaviour
{
    private CharacterClass chosenClass;
    private CharacterRace chosenRace;
    private Gender chosenGender;

    [SerializeField] private GameObject characterPreview;
    [SerializeField] private TMP_InputField nameField;

    private PreviewCharacter previewCharacter;

    private void Start()
    {
        previewCharacter = characterPreview.GetComponent<PreviewCharacter>();

        SelectClass(PlayerPrefs.GetInt("ClassSelected", 0));
        SelectRace(PlayerPrefs.GetInt("RaceSelected", 0));
        SelectGender(PlayerPrefs.GetInt("GenderSelected", 0));
        nameField.text = PlayerPrefs.GetString("NameSelected", "Character name...");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            characterPreview.transform.Rotate(new Vector3(0.0f, transform.localRotation.eulerAngles.x + 0.2f, 0.0f));
        if (Input.GetKey(KeyCode.RightArrow))
            characterPreview.transform.Rotate(new Vector3(0.0f, transform.localRotation.eulerAngles.x - 0.2f, 0.0f));
        if (Input.GetMouseButton(0))
            characterPreview.transform.Rotate(new Vector3(0.0f, - Input.GetAxis("Mouse X") * 7.5f, 0.0f));
    }

    public void SelectClass(int askedIndex)
    {
        if (askedIndex < 0 || askedIndex >= 3)
        {
            return;
        }

        chosenClass = (CharacterClass)askedIndex;

        previewCharacter.ChangeWeaponTo(chosenClass);
    }

    public void SelectRace(int askedIndex)
    {
        if (askedIndex < 0 || askedIndex >= 5)
        {
            return;
        }

        chosenRace = (CharacterRace)askedIndex;

        previewCharacter.ChangeHeadTo(chosenGender, chosenRace);
    }

    public void SelectGender(int askedIndex)
    {
        if (askedIndex < 0 || askedIndex >= 2)
        {
            return;

        }

        chosenGender = (Gender)askedIndex;

        previewCharacter.ChangeHeadTo(chosenGender, chosenRace);
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("ClassSelected", (int)chosenClass);
        PlayerPrefs.SetInt("RaceSelected", (int)chosenRace);
        PlayerPrefs.SetInt("GenderSelected", (int)chosenGender);
        PlayerPrefs.SetString("NameSelected", nameField.text);
        SceneManager.LoadScene("OfflineScene");
    }
}
