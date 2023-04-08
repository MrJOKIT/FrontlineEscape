using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public QuestOne questOne;
    [SerializeField] private Animator _animator;
    private bool isOpen = false;
    private float time;
    private float timer = 1f;

    private void Start()
    {
        
    }

    public void UseGate()
    {
        if (questOne.haveKey && isOpen)
        {
            isOpen = false;
            _animator.SetBool("DoorOpen",false);
        }
        else if(questOne.haveKey && !isOpen)
        {
            isOpen = true;
            _animator.SetBool("DoorOpen",true);
        }
        else
        {
            promptMessage = "NO KEY";
        }
    }

    private void Update()
    {
        if (questOne.haveKey == false)
        {
            promptMessage = "NO KEY";
        }
        else if (questOne.haveKey && !isOpen)
        {
            promptMessage = "Open Gate";
        }
        else if (questOne.haveKey && isOpen)
        {
            promptMessage = "Close Gate";
        }
        
    }
    
    
}
