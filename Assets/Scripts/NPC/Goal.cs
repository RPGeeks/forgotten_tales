using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal : ScriptableObject
{
    [HideInInspector]
    public string text = "N/A";

    protected int ammount;
    protected int currAmount;
    protected bool completed;

    public int Ammount { get => ammount; }
    public int CurrAmount { get => currAmount; }
    public bool Completed { get => completed; set => completed = value; }

  
    public abstract void Finish();
}