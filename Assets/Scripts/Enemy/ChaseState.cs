using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IBaseState
{
    public void EnterState(EnemyAI enemy)
    {
        Debug.Log("Enter Chase");
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log("Exit Chase");
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (enemy.player == null) return;

        enemy.agent.SetDestination(enemy.player.position);
    }
}
