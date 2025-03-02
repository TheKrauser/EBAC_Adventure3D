using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBase
{
    public GunBase gunBase;

    protected override void Init()
    {
        base.Init();
        gunBase.StartShoot();
    }

    protected override void OnKill()
    {
        base.OnKill();
        gunBase.StopShoot();
    }
}
