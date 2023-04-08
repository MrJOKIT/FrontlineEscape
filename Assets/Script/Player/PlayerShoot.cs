using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action ShootInput;
    public static Action ReloadInput;
    [SerializeField] private KeyCode reloadKey = KeyCode.R;
    private WeaponSwitching weaponSwitching;
    private Gun _gun;
    [SerializeField] private float currentAmmo;
    [SerializeField] private TextMeshProUGUI ammoText;
    private float time;
    private float timer = 1f;
    private bool outMag = false;

    private void Start()
    {
        weaponSwitching = GameObject.Find("GunHolder").gameObject.GetComponent<WeaponSwitching>();
    }

    private void Update()
    {
        _gun = weaponSwitching.weapons[weaponSwitching.selectedWeapon].GetComponent<Gun>();
        currentAmmo = _gun.currentAmmo;
        if (_gun.currentAmmo >= 10)
        {
            ammoText.text = "" + _gun.currentAmmo;
        }
        else
        {
            ammoText.text = "0" + _gun.currentAmmo;
        }
        if (Input.GetMouseButton(0))
        {
            ShootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey) && _gun.gunData.magazine > 0)
        {
            ReloadInput?.Invoke();
        }
        else if (Input.GetKeyDown(reloadKey) && _gun.gunData.magazine <= 0)
        {
            _gun.reloadText.text = "No more magazine";
            outMag = true;

        }

        if (outMag)
        {
            time += Time.deltaTime;
            if (time > timer)
            {
                _gun.reloadText.text = "";
                time = 0f;
                outMag = false;
            }
        }
        
    }
}
