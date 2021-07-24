using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void ModifyHealth (int amount)
    {
        currentHealth += amount;

        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.SetHealth(currentHealthPercent);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModifyHealth(-10);
        }
    }
}
