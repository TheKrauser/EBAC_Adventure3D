using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemBase : MonoBehaviour
{
    public ClothType clothType;
    public float duration = 5f;

    [SerializeField] private string compareTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    public virtual void Collect()
    {
        var setup = ClothManager.Instance.GetSetupByType(clothType);
        Player.Instance.ChangeTexture(setup, duration);
        gameObject.SetActive(false);
    }
}
