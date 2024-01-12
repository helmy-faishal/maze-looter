using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarning : MonoBehaviour
{
    [SerializeField] GameObject warningObject;
    Animator animator;

    const string WarningBlinking = "Blinking";

    int totalEnemy = 0;

    public static PlayerWarning Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = warningObject.GetComponentInChildren<Animator>();
    }

    public void SetActiveWarning(bool active)
    {
        if (warningObject == null) return;

        totalEnemy += (active ? 1 : -1);
        totalEnemy = Mathf.Max(0, totalEnemy);

        if (totalEnemy > 0)
        {
            warningObject.SetActive(true);
        }
        else
        {
            warningObject.SetActive(false);
        }
    }

    public void SetEnemyChasing(bool chase)
    {
        if (animator == null) return;

        animator.SetBool(WarningBlinking, chase);
    }
}
