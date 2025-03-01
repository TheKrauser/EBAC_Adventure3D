using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemLayout : MonoBehaviour
{
    public Image itemImage;
    public TMP_Text itemAmount;

    private ItemSetup currentSetup;
    public void Load(ItemSetup itemSetup)
    {
        currentSetup = itemSetup;
        UpdateUI();
    }

    public void UpdateUI()
    {
        itemImage.sprite = currentSetup.itemSprite;
    }

    private void Update()
    {
        itemAmount.text = "x" + currentSetup.soInt.value.ToString();
    }
}
