using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal : ScriptableObject
{
    [HideInInspector]
    public string text = "N/A";

    public int ammount;
    public int currAmount;
    public bool completed;

    public bool Completed { get => completed; set => completed = value; }

    public abstract void Init();
    public abstract void Finish();
}