using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.C;
    public SOInt soInt;

    private void Start()
    {
        soInt = ItemManager.Instance.GetLifePacks();
    }

    private void RecoverHealth()
    {
        if (soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK, 1);
            Player.Instance.ResetHealth();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverHealth();
        }
    }
}
