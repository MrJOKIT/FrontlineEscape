using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public PlayerHealth enemyHealth;
    public HitBoxPart HitBoxParts;

    public enum HitBoxPart
    {
        Head,
        Body,
        Foot,
    }
}
