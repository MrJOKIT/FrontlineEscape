using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCam;
    private float xRotation = 0f;
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;
    [SerializeField] private Slider xSensitivitySetting;
    [SerializeField] private Slider ySensitivitySetting;


    private void Start()
    {
        xSensitivitySetting.value = xSensitivity / 30f;
        ySensitivitySetting.value = ySensitivity / 30f;
    }

    public void ProcessLook(Vector2 input)
    {
        xSensitivity = xSensitivitySetting.value * 30f;
        ySensitivity = ySensitivitySetting.value * 30f;
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //apply this to our camera tranform.
        playerCam.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //rotate player to look left and rigt
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
