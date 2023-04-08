using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    protected override void Interact()
    {
        //สิงที่จะแสดงผลเมื่อ Interact
        Debug.Log("Interacted with " + gameObject.name);
    }
    
}
