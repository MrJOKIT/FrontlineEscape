using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject endMissionMenu;
    public GameObject menu;
    public GameObject playerUI;
    //public Camera cam;
    public GameObject mission;
    public GunDataSave[] _gunDataSaves;
    public GameObject deathMenu;
    public Enemy[] enemyList;
    private bool haveEnemy = false;
    private bool menuActive = false;
    public int enemyKill;

    


    private void Awake()
    {
        endMissionMenu.SetActive(false);
        menu.SetActive(false);
        mission.SetActive(true);
        deathMenu.SetActive(false);
        Time.timeScale = 0f; //0
        Cursor.visible = true; //true
    }

    private void Start()
    {
        for (int i = 0; i < _gunDataSaves.Length; i++)
        {
            _gunDataSaves[i]._gun.magSize = _gunDataSaves[i].saveMaxAmmo;
            _gunDataSaves[i]._gun.currentAmmo = _gunDataSaves[i].saveMaxAmmo;
            _gunDataSaves[i]._gun.magazine = _gunDataSaves[i].saveMagazine;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuActive)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            menuActive = true;
            menu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuActive)
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            menuActive = false;
            menu.SetActive(false);
        }

        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (haveEnemy == false && enemyList[i] != null)
            {
                haveEnemy = true;
            }
            else if (haveEnemy == true && enemyList[i] == null)
            {
                haveEnemy = false;
            }

            if (enemyList[i] == true)
            {
                for (int j = 0; j < enemyList.Length; j++)
                {
                    enemyList[i].foundPlayer = true;
                }
            }
            
        }
    }

    public void Death()
    {
        Cursor.lockState = CursorLockMode.Confined;
    
        playerUI.SetActive(false);
        foreach (var t in _gunDataSaves)
        {
            t._gun.currentAmmo += 1;
        }
        Cursor.visible = true;
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void EndMission()
    {
        playerUI.SetActive(false);
        foreach (var t in _gunDataSaves)
        {
            t._gun.currentAmmo += 1;
        }
    }

    public void MissionBegin()
    {
        mission.SetActive(false);
        Time.timeScale = 1f; //0
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; //true
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Credit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
