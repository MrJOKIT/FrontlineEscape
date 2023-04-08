using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int wayPointIndex;
    public float waitTimer;
    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        PartrolCycle();
    }
    public override void Exit()
    {
        
    }

    public void PartrolCycle()
    {
        //implement our partrol logic
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > _stateMachine.waitTime)
            {
                
                if (wayPointIndex < enemy.path.wayPoints.Count - 1)
                {
                    
                    wayPointIndex++;
                }
                else
                {
                    
                    wayPointIndex = 0;
                }

                Debug.Log("Walk");
                _stateMachine.enemyAnimator.SetBool("Walk",true);
                _stateMachine.enemyAnimator.SetBool("Idle",false);
                enemy.Agent.SetDestination(enemy.path.wayPoints[wayPointIndex].position);
                waitTimer = 0;
            }
            else if (waitTimer < _stateMachine.waitTime)
            {
                Debug.Log("Idle");
                _stateMachine.enemyAnimator.SetBool("Walk",false);
                _stateMachine.enemyAnimator.SetBool("Idle",true);
            }
            
        }
        
    }
}
