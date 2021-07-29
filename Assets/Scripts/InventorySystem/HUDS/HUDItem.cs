using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class HUDItem : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] private new string name = "New Item";
    [SerializeField] private Sprite icon = null;

    public string Name { get => name; }

    public abstract string Rarity { get; }

    public Sprite Icon { get => icon; }

    public abstract string ShowInfo();
}
