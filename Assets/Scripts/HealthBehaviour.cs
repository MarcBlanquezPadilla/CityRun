using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class HealthBehaviour : MonoBehaviour
{
    [Header("PROPERTIES")]
    [SerializeField] private int maxHealth;

    [Header("INFORMATION")]
    [SerializeField] private bool immortal;
    [SerializeField] private int health;

    [Header("EVENTS")]
    public UnityEvent isDead;
    public UnityEvent<int, int> updateLife;

    void Awake()
    { 
        immortal = false;
        health = maxHealth;
        updateLife.Invoke(health, maxHealth);
    }

    public void Hurt(int dmg)
    {
        if (!immortal)
        {
            health -= dmg;
            updateLife.Invoke(health, maxHealth);
            if (health <= 0)
            {
                health = 0;
                isDead.Invoke();
            }
        }
    }

    public void Healing(int heal)
    {
        health += health;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        updateLife.Invoke(health, maxHealth);
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetImmortal(bool b)
    {
        immortal = b;
    }

    public void SwitchImmortal()
    {
        if (immortal)
        {
            immortal = false;
        }
        else
        {
            immortal = true;
        }
    }
}
