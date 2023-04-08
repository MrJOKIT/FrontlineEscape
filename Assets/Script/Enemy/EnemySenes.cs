using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenes : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;

    public GameObject player;
    public StateMachine stateMachine;
    public Enemy enemy;

    private void Update()
    {
        Vector3 playerTarget = (player.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget <= viewRadius)
            {
                if (Physics.Raycast(transform.position,playerTarget,distanceToTarget,obstacleMask) == false)
                {
                    
                    Debug.Log("Haha I have seen you!!");
                    
                    enemy.foundPlayer = true;
                }
            }
        }
        
        float hearDistance = Vector3.Distance(transform.position, player.transform.position);
        
    }
}
