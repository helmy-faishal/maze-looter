using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class DetectPlayer : MonoBehaviour
{
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float rangeMultiplier = 1.25f;
    [SerializeField] float delayToChase = 4f;
    [SerializeField] float delayToAwarePlayer = 2f;

    Transform player;
    EnemyAI enemyAI;
    Treasure treasure;

    [SerializeField] bool _isPlayerDetected = false;
    bool IsPlayerDetected
    {
        get { return _isPlayerDetected; }
        set 
        {
            if (_isPlayerDetected == value) return;

            _isPlayerDetected = value; 
            PlayerWarning.Instance?.SetActiveWarning(_isPlayerDetected);
        }
    }
    [SerializeField] float enemyAwareness = 0;

    [SerializeField] bool _isChasing = false;
    public bool IsChasing
    {
        get { return _isChasing; }
        set
        {
            if (_isChasing == value) return;

            _isChasing = value;

            StopAllCoroutines();

            if (_isChasing)
            {
                enemyAI.SwitchToChase();
            }
            else
            {
                enemyAI.SwitchToPatrol();
            }
            PlayerWarning.Instance?.SetEnemyChasing(_isChasing);
        }
    }

    bool _canDetectPlayer = true;
    public bool CanDetectPlayer
    {
        get { return _canDetectPlayer; }
        set
        {
            if (_canDetectPlayer == value) return;

            _canDetectPlayer = value;

            if (!_canDetectPlayer)
            {
                enemyAwareness = 0;
                IsChasing = false;
                IsPlayerDetected = false;
            }
        }
    }

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAwareness = 0;
    }

    private void Start()
    {
        player = enemyAI.player;
    }

    private void OnEnable()
    {
        treasure = FindObjectOfType<Treasure>();

        if (treasure == null)
        {
            Debug.LogWarning("Treasure not found");
            return;
        }

        treasure.OnTreasurePickedUp += GoToTreasure;
    }

    private void OnDisable()
    {
        if (treasure != null)
        {
            treasure.OnTreasurePickedUp -= GoToTreasure;
        }
    }

    public void SetPlayerDetectable()
    {
        CanDetectPlayer = true;
    }

    public void SetPlayerUndetectable()
    {
        CanDetectPlayer = false;
    }

    void GoToTreasure()
    {
        StopAllCoroutines();
        StartCoroutine(GoToTreasureCoroutine());
    }

    IEnumerator GoToTreasureCoroutine()
    {
        yield return new WaitForSeconds(delayToAwarePlayer);
        UIManager.Instance?.SetWarningActive("Enemies start moving to the treasure room");
        enemyAI.MoveToAssemblyWaypoint();
        detectionRange *= rangeMultiplier;
    }

    void Update()
    {
        if (!CanDetectPlayer) return;

        DetectingPlayer();
    }

    private void FixedUpdate()
    {
        if (!CanDetectPlayer) return;

        CastingRaycast();
    }

    private void CastingRaycast()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionRange))
        {
            if (!hit.transform.CompareTag("Player")) return;

            IsChasing = true;
            enemyAwareness = delayToChase;
        }
    }

    void DetectingPlayer()
    {
        if (player == null) return;

        if (Vector3.Distance(transform.position,player.position) > detectionRange)
        {
            PlayerKeepAway();
        }
        else
        {
            PlayerDetected();
        }
    }

    private void PlayerDetected()
    {
        if (!IsPlayerDetected)
        {
            IsPlayerDetected = true;
        }

        enemyAwareness = Mathf.Min(delayToChase, enemyAwareness + Time.deltaTime);

        if (enemyAwareness >= delayToChase && !IsChasing)
        {
            IsChasing = true;
        }
    }

    void PlayerKeepAway()
    {
        enemyAwareness = Mathf.Max(0,enemyAwareness - Time.deltaTime);

        if (enemyAwareness <= 0)
        {
            IsPlayerDetected = false;
            IsChasing = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,transform.forward * detectionRange);
    }
}
