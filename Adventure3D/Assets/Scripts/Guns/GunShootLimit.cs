using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public int maxBullets = 5;
    public float timeToReload = 1f;

    private int currentBullets;
    private bool reloading = false;

    protected override IEnumerator ShootCoroutine()
    {
        if (reloading) yield break;

        while(true)
        {
            if (currentBullets < maxBullets)
            {
                Shoot();
                currentBullets++;
                CheckReload();
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    private void CheckReload()
    {
        if (currentBullets >= maxBullets)
        {
            StopShoot();
            Reload();
        }
    }

    private void Reload()
    {
        reloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(timeToReload);
        reloading = false;
        currentBullets = 0;
    }

    public override int GetMaxAmmo()
    {
        return maxBullets;
    }

    public override int GetCurrentAmmo()
    {
        return currentBullets;
    }
}
