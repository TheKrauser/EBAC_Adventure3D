using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLayoutManager : MonoBehaviour
{
    public ItemLayout prefabLayout;
    public Transform container;

    public List<ItemLayout> itemLayouts;

    private void Start()
    {
        CreateItems();
    }

    private void CreateItems()
    {
        var setups = ItemManager.Instance.itemSetups;

        foreach (var setup in setups)
        {
            var item = Instantiate(prefabLayout, container);
            item.Load(setup);
        }
    }
}
