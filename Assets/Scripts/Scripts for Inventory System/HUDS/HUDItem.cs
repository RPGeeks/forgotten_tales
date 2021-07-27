using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUDItem : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] private new string name = "New Item";
    [SerializeField] private Sprite icon = null;

    public string Name { get => name; }

    public abstract string Rare { get; }

    public Sprite Icon { get => Icon; }

    public abstract string ShowText();
}
