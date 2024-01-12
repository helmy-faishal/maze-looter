using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IBaseState
{
    public Transform specificDestination = null;
    Transform destination;
    bool isMoving;

    public void EnterState(EnemyAI enemy)
    {
        Debug.Log("Enter Patrol");
        isMoving = false;
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log("Exit Patrol");
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (!isMoving)
        {
            isMoving = true;

            if (specificDestination != null)
            {
                destination = specificDestination;
                specificDestination = null;
            }
            else
            {
                destination = enemy.GetRandomWaypoints();
            }
            if (destination == null) return;

            enemy.agent.SetDestination(destination.position);
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position,destination.position) <= 3)
            {
                isMoving = false;
            }
        }
    }
}
