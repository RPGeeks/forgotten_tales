using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class EnemyHealth : NetworkBehaviour
{
    [SerializeField]
    private float maxHealth = 100;

    private float currentHealth;

    [SerializeField]
    private HealthBar healthBar;

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.FillAmount = maxHealth;
    }

    void Update()
    {

    }

    [Server]
    public void CmdDamageTest()
    {
        CurrentHealth -= 10;
        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.FillAmount = currentHealthPercent;
    }
}
