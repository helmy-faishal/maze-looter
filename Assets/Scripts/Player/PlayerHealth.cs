using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;

    public ReviveSkill reviveSkill;
    bool IsPlayerCanRevive
    {
        get { return reviveSkill != null; }
    }

    int health;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        UIManager.Instance?.SetHealthText(health);
    }

    public void RevivePlayer()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayerDead();
        }

        UIManager.Instance?.SetHealthText(health);
    }

    void PlayerDead()
    {
        if (IsPlayerCanRevive)
        {
            reviveSkill.StartRevive(RevivePlayer);
            reviveSkill = null;
        }
        else
        {
            SceneSwitching.Instance?.LoseScene();
        }
    }
}
