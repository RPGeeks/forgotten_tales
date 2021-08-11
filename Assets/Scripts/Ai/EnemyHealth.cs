using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class EnemyHealth : NetworkBehaviour
{
    [SerializeField]
    public float maxHealth = 100;

    [SyncVar]
    [SerializeField]
    public float currentHealth;

    [SerializeField]
    public HealthBar healthBar;

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.FillAmount = maxHealth;
    }

    void Update()
    {
        if (isServer == false)
        {
            return;
        }
        RpcDamageTest();
/*        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.FillAmount = currentHealthPercent;*/
    }

    [ClientRpc]
    public void RpcDamageTest()
    {
        //CurrentHealth -= 10;
        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.FillAmount = currentHealthPercent;
    }


}
