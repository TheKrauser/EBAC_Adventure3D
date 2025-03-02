using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.Z;

    public Animator anim;
    public ChestItemBase chest;

    public string triggerOpen = "Open";

    private bool canOpen = false;
    private bool opened = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!canOpen) return;
        if (opened) return;

        if (Input.GetKeyDown(keyCode))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        anim.SetTrigger(triggerOpen);
        opened = true;
        Invoke(nameof(ShowItem), 1.5f);
    }

    private void ShowItem()
    {
        chest.ShowItem();
        Invoke(nameof(CollectItems), 1.5f);
    }

    private void CollectItems()
    {
        chest.Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}
