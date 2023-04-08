using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private float time;
    private float timer = 1f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            time = 0f;
            Destroy(gameObject);
        }
    }
}
