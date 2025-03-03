using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBase
{
    public GunBase gunBase;

    public Player player;
    private bool inRange;

    protected override void Init()
    {
        base.Init();
    }

    protected override void OnKill()
    {
        base.OnKill();
        gunBase.StopShoot();
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(transform.position, player.transform.position) <= 30 && !inRange)
        {
            inRange = true;
            gunBase.StartShoot();
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 30 && inRange)
        {
            inRange = false;
            gunBase.StopShoot();
        }
    }
}
