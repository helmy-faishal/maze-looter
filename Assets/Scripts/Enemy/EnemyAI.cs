using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] bool isTestScene = false;
    public List<Transform> waypoints;
    public Transform assemblyWaypoints;
    public NavMeshAgent agent;
    public Transform player;

    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    IBaseState currentState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentState = patrolState;

    }

    void LateUpdate()
    {
        if (isTestScene) return;

        if (currentState == null) return;

        currentState.UpdateState(this);
    }

    public void SwitchState(IBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void SwitchToChase()
    {
        SwitchState(chaseState);
    }

    public void SwitchToPatrol()
    {
        SwitchState(patrolState);
    }

    public void MoveToAssemblyWaypoint()
    {
        patrolState.specificDestination = assemblyWaypoints;
        SwitchState(patrolState);
    }

    public void SpeedUpEnemy(float speedMultiplier)
    {
        agent.speed *= speedMultiplier;
    }
    
    public Transform GetRandomWaypoints()
    {
        if (waypoints.Count == 0) return null;

        int index = Random.Range(0, waypoints.Count);
        return waypoints[index];
    }
}
