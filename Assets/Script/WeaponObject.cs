using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponProfile", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponObject : ScriptableObject
{
    [Header("GUN INFO")]
    public string names;
    public SoundManager.SoundName SoundName;
    
    [Header("GUN STATS")]
    public float damage;
    public float fireRate;
    public float maxDistance;
    
    [Header("Reload")]
    public float currentAmmo;
    public float magazine;
    public float magSize;
    public float reloadTime;
    public bool reloading;

}
