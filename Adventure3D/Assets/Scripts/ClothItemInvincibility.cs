using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemInvincibility : ClothItemBase
{
    public override void Collect()
    {
        base.Collect();
        Player.Instance.TurnInvincibility(duration);
        AudioManager.Instance.PlaySound("Invincible");
    }
}
