using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretFile : Interactable
{
    public QuestOne questOne;
    public GameObject file;

    public void DestroyFile()
    {
        questOne.readSecretFile = true;
        Destroy(file);
    }
}
