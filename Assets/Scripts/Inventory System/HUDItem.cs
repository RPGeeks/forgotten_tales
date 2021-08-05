using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUDItem : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] private new string name = "New Item";
    [SerializeField] protected Sprite icon = null;
    
    public string Name { get => name; }

    public abstract string Rare { get; }

    public Sprite Icon { get => icon; }

    public abstract string ShowText();
}
