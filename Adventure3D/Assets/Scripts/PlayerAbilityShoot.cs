using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase firstGun;
    public GunBase secondGun;

    public Transform gunPosition;

    private GunBase primaryGun;
    private GunBase secondaryGun;

    private GunBase currentGun;

    private void Update()
    {

    }

    protected override void Init()
    {
        base.Init();

        CreateGuns();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
        inputs.Gameplay.ChangeWeapon1.performed += cts => SwitchGun(1);
        inputs.Gameplay.ChangeWeapon2.performed += cts => SwitchGun(2);
    }

    private void CreateGuns()
    {
        primaryGun = Instantiate(firstGun, gunPosition);
        secondaryGun = Instantiate(secondGun, gunPosition);


        primaryGun.transform.localPosition = primaryGun.transform.localEulerAngles = Vector3.zero;
        secondaryGun.transform.localPosition = secondaryGun.transform.localEulerAngles = Vector3.zero;

        currentGun = primaryGun;
    }

    private void SwitchGun(int n)
    {
        currentGun.StopShoot();
        
        if (n == 1)
        {
            currentGun = primaryGun;
        }
        else
        {
            currentGun = secondaryGun;
        }
    }

    private void StartShoot()
    {
        currentGun.StartShoot();
    }

    private void CancelShoot()
    {
        currentGun.StopShoot();
    }
}
