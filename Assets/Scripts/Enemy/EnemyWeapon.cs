using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] int weaponDamage = 1;

    EnemyAttack enemyAttack;
    bool canHit = true;

    private void Awake()
    {
        enemyAttack = transform.parent.GetComponent<EnemyAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        canHit = true;
        if (enemyAttack != null)
        {
            canHit = enemyAttack.IsAttack;
        }

        if (canHit)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(weaponDamage);
        }
    }
}
