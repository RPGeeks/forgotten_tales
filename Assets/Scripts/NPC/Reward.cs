using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : ScriptableObject
{
    protected int ammount;
    private CharacterController player;

    private void Awake()
    {
        player = FindObjectOfType<CharacterController>();
    }

    public abstract void GiveReward();
}