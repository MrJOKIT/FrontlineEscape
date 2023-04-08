using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private SoundManager _soundManager;
    private void Start()
    {
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        SoundManager.instace.Play(SoundManager.SoundName.MainMenuBGM);
    }
}
