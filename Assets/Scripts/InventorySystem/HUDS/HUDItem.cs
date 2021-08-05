using RPGeeks.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class HUDItem : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] private new string name = "New Item";
    [SerializeField] protected Sprite icon = null;

    [Header("Item Prefab")]
    [SerializeField] private GameObject prefab = null;

    public string Name { get => name; }

    public abstract string Rarity { get; }

    public Sprite Icon { get => icon; }

    public GameObject Prefab { get => prefab; }

    public abstract string ShowInfo();
}
