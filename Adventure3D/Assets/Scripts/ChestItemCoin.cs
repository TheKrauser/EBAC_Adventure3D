using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestItemCoin : ChestItemBase
{
    public int coinAmount = 5;
    public GameObject prefabCoin;

    private List<GameObject> items = new List<GameObject>();

    private void CreateItems()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            var item = Instantiate(prefabCoin);
            item.transform.position = transform.position;
            items.Add(item);
        }
    }

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    public override void Collect()
    {
        base.Collect();
        StartCoroutine(CollectCoroutine());
    }

    private IEnumerator CollectCoroutine()
    {
        foreach (var item in items)
        {
            item.transform.DOMoveY(1.5f, 0.5f).SetEase(Ease.OutBack).SetRelative().OnComplete(()=>
            {
                item.gameObject.SetActive(false);
            });
            ItemManager.Instance.AddByType(ItemType.COIN, 1);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
