using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestOne : MonoBehaviour
{
    public bool haveKey;
    public bool readSecretFile;
    public GameObject player;
    public GameObject cam;
    public TextMeshProUGUI questText;
    

    private void Update()
    {
        if (!haveKey && !readSecretFile)
        {
            
            questText.fontSize = 26;
            questText.text = "FIND SECRET FILE";
        }
        else if (!haveKey && readSecretFile)
        {
            
            questText.fontSize = 26;
            questText.text = "FIND KEY";
        }
        else if (haveKey && readSecretFile)
        {
            
            questText.fontSize = 20;
            questText.text = "OPEN GATE & ESCAPE";
        }
        
    }
}
