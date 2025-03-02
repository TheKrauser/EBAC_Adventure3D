using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour, IDamageable
{
    public HealthBase healthBase;

    public bool hasDrop = false;
    public int dropAmount;
    public GameObject prefabCoin;

    private void OnValidate()
    {
        if (healthBase == null) GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += HealthBase_OnDamage;
        healthBase.OnKill += HealthBase_OnKill;
    }

    private void HealthBase_OnKill(HealthBase obj)
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Vector3 pos = transform.position;
            pos.x += UnityEngine.Random.Range(-2f, 2);
            pos.y += 3f;
            pos.z += UnityEngine.Random.Range(-2f, 2);
            var coin = Instantiate(prefabCoin, pos, Quaternion.identity);
        }
    }

    private void HealthBase_OnDamage(HealthBase obj)
    {
        transform.DOShakeScale(.1f, Vector3.up, 5);
    }

    public void Damage(float damage)
    {
        healthBase.Damage(damage);
    }
}
