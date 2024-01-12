using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectPlayer))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackRange;

    EnemyAI enemyAI;
    Animator animator;
    Transform player;
    DetectPlayer detectPlayer;

    const string AttackKey = "EnemyAttack";

    bool _isAttack = false;
    public bool IsAttack
    {
        get { return _isAttack; }
        set
        {
            if (_isAttack == value) return;

            _isAttack = value;

            animator.SetBool(AttackKey, _isAttack);
        }
    }

    bool _canDetectPlayer = true;
    bool CanDetectPlayer
    {
        get { return _canDetectPlayer; }
        set
        {
            if (_canDetectPlayer == value) return;

            _canDetectPlayer = value;

            if (!_canDetectPlayer)
            {
                IsAttack = false;
            }
        }
    }

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        detectPlayer = GetComponent<DetectPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = enemyAI.player;
    }

    // Update is called once per frame
    void Update()
    {
        CanDetectPlayer = detectPlayer.CanDetectPlayer;
        if (!CanDetectPlayer) return;
        Attack();
    }

    void Attack()
    {
        if (player == null) return;

        IsAttack = Vector3.Distance(transform.position, player.position) < attackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
