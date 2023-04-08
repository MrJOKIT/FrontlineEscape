using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class RandomBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletTail;
    private SoundManager _soundManager;
    private float bulletSpeed = 30f;
    private float time;
    private float timer;

    private void Start()
    {
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        timer = Random.Range(1f,5f);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            var bullet = Instantiate(bulletTail, transform);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            SoundManager.instace.Play(SoundManager.SoundName.EnemyShoot);
            time = 0f;
            timer = Random.Range(1f,5f);
        }
    }
}
