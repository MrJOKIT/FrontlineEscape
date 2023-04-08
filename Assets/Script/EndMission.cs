using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class EndMission : MonoBehaviour
{
    public QuestOne questOne;
    public GameObject endMissionMenu;
    public GameController gameController;
    public PlayerHealth playerHealth;
    public TextMeshProUGUI enemyKillScore;
    public TextMeshProUGUI remainingAmmo;
    public TextMeshProUGUI remainingHealth;
    public TextMeshProUGUI remainingTime;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI questResult;
    public TextMeshProUGUI comment;
    private int currentAmmo;
    private int currentHealth;
    private int currentTime;
    private int curentEnemyKill;

    private int resultScore;
    private int ammoScore;
    private int healthScore;
    private int timeScore;
    private int enemyScore;
    
    
    private float time;
    private float timeDelPoint;

    private void Update()
    {
        time += Time.deltaTime;
        enemyKillScore.text = curentEnemyKill + " X 500 =" + enemyScore;
        remainingAmmo.text = currentAmmo + " X 100 = " + ammoScore;
        remainingHealth.text = currentHealth + " X 100 = " + healthScore;
        remainingTime.text = currentTime + " = " + timeScore;
        resultScoreText.text = resultScore + " POINT";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endMissionMenu.SetActive(true);
            curentEnemyKill = gameController.enemyKill;
            currentTime = Convert.ToInt32(time);
            currentHealth = Convert.ToInt32(playerHealth.Health);
            currentAmmo = Convert.ToInt32(gameController._gunDataSaves[0]._gun.currentAmmo);
            currentAmmo += Convert.ToInt32(gameController._gunDataSaves[1]._gun.currentAmmo);
            enemyScore = curentEnemyKill * 500;
            ammoScore = currentAmmo * 100;
            healthScore = currentHealth * 100;
            timeDelPoint = time * 10;
            timeScore = 50000 - Convert.ToInt32(timeDelPoint);
            resultScore = ammoScore + healthScore + timeScore;
            if (!questOne.readSecretFile)
            {
                resultScore -= 50000;
                questResult.fontSize = 27;
                questResult.text = "MISSION FAILED";
                comment.fontSize = 27;
                comment.text = "Not received Secret File";
            }
            else if (questOne.readSecretFile)
            {
                questResult.fontSize = 27;
                questResult.text = "MISSION COMPLETE";
                comment.fontSize = 36;
                comment.text = "Good job Soldier";
            }
            gameController.EndMission();
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
