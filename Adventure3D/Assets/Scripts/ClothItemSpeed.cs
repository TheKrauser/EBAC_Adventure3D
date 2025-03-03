using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemSpeed : ClothItemBase
{
    public float targetSpeed = 2f;
    public override void Collect()
    {
        base.Collect();
        Player.Instance.ChangeSpeed(targetSpeed, duration);
        AudioManager.Instance.PlaySound("Speed");
    }
}
