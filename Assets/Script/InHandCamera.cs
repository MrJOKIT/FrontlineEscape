using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHandCamera : MonoBehaviour
{
    public Transform camera;

    private void Update()
    {
        transform.position = camera.transform.position;
        transform.rotation = camera.transform.rotation;
    }
}
