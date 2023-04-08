using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    private GameController _gameController;
    public int gunDataNumber;
    public WeaponObject gunData;
    [SerializeField] private Transform muzzle;
    
    private Camera cam;
    public TextMeshProUGUI reloadText;
    
    [Header("SFX")] 
    [SerializeField] private Transform bloodSplash;
    [SerializeField] private Transform sfxGun;
    [SerializeField] private VisualEffect sfxGunShot;

    [Header("Ref")] 
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Animator gunShootAnimator;
    [SerializeField] private Animator gunReloadAnimator;

    public float currentAmmo;
    private float time;
    private float timer = 1f;
    private bool outMag = false;

    private float timeSinceLastShot;
    

    private void Start()
    {
        cam = Camera.main;
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gunData = _gameController._gunDataSaves[gunDataNumber]._gun;
        PlayerShoot.ShootInput += Shoot;
        PlayerShoot.ReloadInput += StartReload;
        currentAmmo = gunData.currentAmmo;
    }

    private void OnDisable()
    {
        gunData.reloading = false;
        

    }

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        reloadText.text = "Reloading";
        
        yield return new WaitForSeconds(gunData.reloadTime);
        
        reloadText.text = "";
        gunData.currentAmmo = gunData.magSize;
        gunData.magazine--;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(cam.transform.position,cam.transform.forward,out RaycastHit hitInfo,gunData.maxDistance))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Hitbox>() != null)
                    {
                        var hitbox = hitInfo.collider.gameObject.GetComponent<Hitbox>();
                        if (hitbox.HitBoxParts == Hitbox.HitBoxPart.Head)
                        {
                            hitbox.enemyHealth.Health -= gunData.damage * 1.5f;
                            Instantiate(bloodSplash, hitInfo.transform);
                            Debug.Log(hitInfo.transform.name + "HP : " + hitbox.enemyHealth.Health);
                        }
                        else if (hitbox.HitBoxParts == Hitbox.HitBoxPart.Body)
                        {
                            hitbox.enemyHealth.Health -= gunData.damage * 1f;
                            Instantiate(bloodSplash, hitInfo.transform);
                            Debug.Log(hitInfo.transform.name + "HP : " + hitbox.enemyHealth.Health);
                        }
                        else if (hitbox.HitBoxParts == Hitbox.HitBoxPart.Foot)
                        {
                            hitbox.enemyHealth.Health -= gunData.damage * 0.75f;
                            Instantiate(bloodSplash, hitInfo.transform);
                            Debug.Log(hitInfo.transform.name + "HP : " + hitbox.enemyHealth.Health);
                        }
                        else
                        {
                            Debug.LogError("Hitbox failed");
                        }
                    }
                    else
                    {
                        //Debug.Log(hitInfo.transform.name);
                    }
                    
                }
                
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
            
        }
        /*else if (gunData.currentAmmo <= 0 && gunData.magazine > 0) 
        {
            StartCoroutine(Reload());
        }
        else if (gunData.currentAmmo <= 0 && gunData.magazine <= 0)
        {
            //reloadText.text = "No more magazine";
            outMag = true;
        }
        else
        {
            Debug.Log("cancel reload");
        }*/
    }

    private void Update()
    {
        /*if (_gameController.cam == null)
        {
            _gameController.cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }*/
        
        if (outMag)
        {
            time += Time.deltaTime;
            if (time > timer)
            {
                reloadText.text = "";
                time = 0f;
                outMag = false;
            }
        }
        currentAmmo = gunData.currentAmmo;
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(cam.transform.position,cam.transform.forward * gunData.maxDistance,Color.red);
    }
    
    

    private void OnGunShot()
    {
        Instantiate(sfxGun,muzzle.position,muzzle.rotation);
        gunShootAnimator.SetTrigger("Shoot");
        SoundManager.instace.Play(gunData.SoundName);
    }
}
