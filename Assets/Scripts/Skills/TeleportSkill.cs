using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class TeleportSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 1;
    [SerializeField] float skillDuration = 2f;
    [SerializeField] float startDelay = 5f;
    [SerializeField] float holdDelay = 2f;
    [SerializeField] float spawnDelay = 0.5f;

    public GameObject teleportPointObject;

    TeleportPoint teleportTarget;
    ParticleSystem teleportEffect;
    PlayerMovement playerMovement;

    float holdValue = 0f;
    bool canSpawn = true;

    private void Awake()
    {
        this.skillType = SkillType.Teleport;
        this.skillDelay = startDelay + skillDuration;
        this.maxSkillUsage = skillUsage;
    }

    void Start()
    {
        holdValue = 0f;
        canSpawn = true;
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);

        teleportEffect = gameObject.GetComponentInChildren<ParticleSystem>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        this.OnSkillActive += playerMovement.PlayerCannotMove;
        this.OnSkillDeactivate += playerMovement.PlayerCanMove;

        this.OnSkillDeactivate += () => { PlayTeleportEffect(false); };
    }

    private void OnDisable()
    {
        this.OnSkillActive = null;
        this.OnSkillDeactivate = null;
    }

    void Update()
    {
        if (Input.GetKey(this.skillKey) && this.CanUseSkill)
        {
            ProcessHoldKey();
        }
        else
        {
            ProcessPressKey();
        }
    }

    private void ProcessPressKey()
    {
        if (holdValue <= 0) return;

        if (canSpawn)
        {
            this.UsingSkillNow(SpawnTeleportPoint, false);
            canSpawn = false;
        }
        else
        {
            bool showWarning = false;

            if (teleportTarget != null)
            {
                showWarning = !teleportTarget.IsPlayerNearObject;
            }

            if (showWarning && this.CanUseSkill)
            {
                UIManager.Instance?.SetWarningActive("You have placed a teleportation point");
            }
        }

        SetUsingSkillInfo(0);
        holdValue = 0f;
    }

    private void ProcessHoldKey()
    {
        holdValue = Mathf.Clamp(holdValue + Time.deltaTime, 0, holdDelay);

        if (teleportTarget != null)
        {
            SetUsingSkillInfo(holdValue / holdDelay);
        }

        if (holdValue < holdDelay) return;

        if (teleportTarget != null)
        {
            this.UsingSkillNow(StartTeleport);
            PlayTeleportEffect(true);
            SetUsingSkillInfo(0);
        }
        else
        {
            UIManager.Instance?.SetWarningActive("You haven't placed the teleportation point!");
        }
    }

    void PlayTeleportEffect(bool active)
    {
        if (active)
        {
            teleportEffect.Play();
        }
        else
        {
            teleportEffect.Stop();
        }
    }

    void SetUsingSkillInfo(float value)
    {
        if (value <= 0)
        {
            InteractionUI.Instance?.SetSkillIndicator(false);
            InteractionUI.Instance?.SetSkillFill(0);
        }
        else
        {
            InteractionUI.Instance?.SetSkillIndicator(true);
            InteractionUI.Instance?.SetSkillFill(value);
        }
    }

    void SpawnTeleportPoint()
    {
        if (teleportTarget != null)
        {
            return;
        }

        Vector3 spawnPoint = transform.position;
        spawnPoint.y = 0.01f;
        GameObject point = Instantiate(teleportPointObject, spawnPoint, Quaternion.identity);
        TeleportPoint teleportPoint = point.GetComponent<TeleportPoint>();
        teleportPoint.OnObjectInteracted += DestroyTeleportPoint;
        teleportTarget = teleportPoint;
    }

    void StartTeleport()
    {
        StartCoroutine(StartTeleportCoroutine());
    }

    IEnumerator StartTeleportCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        Vector3 teleportCoordinate = teleportTarget.transform.position;
        teleportCoordinate.y = transform.position.y;

        transform.position = teleportCoordinate;

        DestroyTeleportPoint();
    }

    void DestroyTeleportPoint()
    {
        if (teleportTarget != null)
        {
            Destroy(teleportTarget.gameObject);
            teleportTarget = null;
            StartCoroutine(SetSpawnDelay());
        }
    }

    IEnumerator SetSpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = !canSpawn;
    }
}
