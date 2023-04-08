using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private bool isPlayer;

    private GameController _gameController;
    //[SerializeField] private GameObject enemyObject;
    [Header("Health System")]
    
    private float health;
    private float lerpTimer;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float dangerHealth = 20f;
    [SerializeField] private float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Header("Damage Overlay")] 
    public Image overlay; // our DamageOverlay GameObject
    [SerializeField] private float duration; //how long the image stays fully opaque
    [SerializeField] private float fadeSpeed; // how quickly the image will fade
    [SerializeField] private ScreenDamage _screenDamage;
    private float durationTimer; // timer to check against the duration
    

    

    private void Start()
    {
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        if (isPlayer)
        {
            health = maxHealth;
            _screenDamage.maxHealth = maxHealth;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        }
        else
        {
            health = maxHealth;
        }
        
    }

    private void Update()
    {
        
        
        if (isPlayer)
        {
            _screenDamage.CurrentHealth = health;
            _screenDamage.criticalHealth = dangerHealth;
            health = Mathf.Clamp(health, 0, maxHealth);
            //UpdateHealthUI();

            if (overlay.color.a > 0)
            {
                if (health < dangerHealth)
                {
                    return;
                }
                durationTimer += Time.deltaTime;
                if (durationTimer > duration)
                {
                    //fade the image
                    float tempAlpha = overlay.color.a;
                    tempAlpha -= Time.deltaTime * fadeSpeed;
                    overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
                }
            }

            
        }
        else
        {
            if (health <= 0)
            {
                _gameController.enemyKill++;
                Destroy(gameObject);
            }
        }
        

        //test
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(Random.Range(5,10));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RestoreHealth(Random.Range(5,10));
        }*/
        
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        healthText.text = health + "%";
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        
    }

    public void RestoreHealth(float healthUp)
    {
        health += healthUp;
        lerpTimer = 0f;
        
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    
}
