using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public QuestOne questOne;
    public GameObject key;

    public void CollectKey()
    {
        questOne.haveKey = true;
        Destroy(key);
    }
}
