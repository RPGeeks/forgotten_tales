using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : ScriptableObject
{
    public int ammount;
    public CharacterController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")
                            .GetComponent<CharacterController>();
    }

    public abstract void GiveReward();
}