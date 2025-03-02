using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    COIN,
    LIFE_PACK
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public List<ItemSetup> itemSetups;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Reset();
        AddByType(ItemType.LIFE_PACK, SaveManager.Instance.saveSetup.healthPacks);
        AddByType(ItemType.COIN, SaveManager.Instance.saveSetup.coins);
    }

    private void Reset()
    {
        foreach (var item in itemSetups)
        {
            item.soInt.value = 0;
        }

        //InterfaceInformation.Instance.UpdateInterface();
    }

    public void AddByType(ItemType itemType, int amount = 1)
    {
        itemSetups.Find(item => item.itemType == itemType).soInt.value += amount;
    }

    public void RemoveByType(ItemType itemType, int amount = 1)
    {
        var item = itemSetups.Find(item => item.itemType == itemType).soInt;
        item.value -= amount;

        if (item.value <= 0)
            item.value = 0;
    }

    public SOInt GetCoins()
    {
        return itemSetups.Find(item => item.itemType == ItemType.COIN).soInt;
    }

    public SOInt GetLifePacks()
    {
        return itemSetups.Find(item => item.itemType == ItemType.LIFE_PACK).soInt;
    }

    [NaughtyAttributes.Button]
    public void RemoveCoin()
    {
        RemoveByType(ItemType.COIN, 1);
    }
}

[System.Serializable]
public class ItemSetup
{
    public ItemType itemType;
    public SOInt soInt;
    public Sprite itemSprite;
}
