using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShots = 0.3f;
    public float speed = 20f;

    private Coroutine currentCoroutine;

    public Action OnShoot;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.transform.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

        OnShoot?.Invoke();
    }

    public void StartShoot()
    {
        StopShoot();
        currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
    }

    public virtual int GetMaxAmmo()
    {
        return 0;
    }

    public virtual int GetCurrentAmmo()
    {
        return 0;
    }
}
