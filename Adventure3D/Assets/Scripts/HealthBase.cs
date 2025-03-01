using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour
{
    public int startHealth = 10;
    public int currentHealth;

    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetHealth();
    }

    protected void ResetHealth()
    {
        currentHealth = startHealth;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
            Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    public void Damage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= (int)damage;

        if (currentHealth <= 0)
        {
            Kill();
            Debug.Log("Killed");
        }

        OnDamage?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5f);
    }
}
