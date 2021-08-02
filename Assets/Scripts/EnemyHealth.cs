using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;

    private float currentHealth;

    public HealthBar healthBar;

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.FillAmount = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentHealth -= 10;
            float currentHealthPercent = (float)currentHealth / (float)maxHealth;
            healthBar.FillAmount = currentHealthPercent;
        }
    }
}
