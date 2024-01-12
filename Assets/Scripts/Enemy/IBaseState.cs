using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseState
{
    public void EnterState(EnemyAI enemy);
    public void UpdateState(EnemyAI enemy);
    public void ExitState(EnemyAI enemy);
}
