using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 1;
    [SerializeField] float skillDuration = 2f;
    [SerializeField] float startDelay = 5f;
    [SerializeField] float holdDelay = 2f;
    [SerializeField] float spawnDelay = 0.5f;

    public GameObject teleportPointObject;

    TeleportPoint teleportTarget;
    bool HasTarget { get { return teleportTarget != null; } }

    ParticleSystem teleportEffect;
    PlayerMovement playerMovement;

    float holdValue = 0f;
    bool canSpawn = true;
    bool isHolding = false;

    public override void SetAwake()
    {
        base.SetAwake();
        this.skillType = SkillType.Teleport;
        this.skillDelay = startDelay + skillDuration;
        this.maxSkillUsage = skillUsage;
    }

    public override void SetStart()
    {
        base.SetStart();

        holdValue = 0f;
        canSpawn = true;
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);

        teleportEffect = player.GetComponentInChildren<ParticleSystem>();
        playerMovement = player.GetComponent<PlayerMovement>();

        this.OnSkillActive += playerMovement.PlayerCannotMove;
        this.OnSkillDeactivate += playerMovement.PlayerCanMove;

        this.OnSkillDeactivate += () => { PlayTeleportEffect(false); };

        this.OnPerformedTap += () => { ProcessTapSkill(); };
        this.OnPerformedHold += () => { SetHoldingSkill(true); };

        this.OnCanceledHold += () => { SetHoldingSkill(false); };
    }

    public override void SetDisable()
    {
        base.SetDisable();
        this.OnSkillActive = null;
        this.OnSkillDeactivate = null;
        this.OnPerformedTap = null;
        this.OnPerformedHold = null;
        this.OnCanceledHold = null;
    }

    void Update()
    {
        if (isHolding && this.CanUseSkill)
        {
            ProcessHoldKey();
        }
    }

    void ProcessTapSkill()
    {
        if (canSpawn)
        {
            this.UsingSkillNow(SpawnTeleportPoint, false);
            canSpawn = false;
        }
        else
        {
            bool showWarning = false;

            if (HasTarget)
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

    void SetHoldingSkill(bool value)
    {
        holdValue = 0;
        SetUsingSkillInfo(0);
        isHolding = value;
    }

    private void ProcessHoldKey()
    {
        holdValue = Mathf.Clamp(holdValue + Time.deltaTime, 0, holdDelay);

        if (HasTarget)
        {
            SetUsingSkillInfo(holdValue / holdDelay);
        }

        if (holdValue < holdDelay) return;

        if (HasTarget)
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
        if (value > 0 && HasTarget)
        {
            InteractionUI.Instance?.SetSkillIndicator(true);
            InteractionUI.Instance?.SetSkillFill(value);
        }
        else
        {
            InteractionUI.Instance?.SetSkillIndicator(false);
        }
    }

    void SpawnTeleportPoint()
    {
        if (HasTarget)
        {
            return;
        }

        Vector3 spawnPoint = player.position;
        spawnPoint.y = 0.01f;
        GameObject point = Instantiate(teleportPointObject, spawnPoint, Quaternion.identity);
        TeleportPoint teleportPoint = point.GetComponent<TeleportPoint>();
        teleportPoint.OnObjectInteracted += DestroyTeleportPoint;
        teleportTarget = teleportPoint;
    }

    void StartTeleport()
    {
        isHolding = false;
        StartCoroutine(StartTeleportCoroutine());
    }

    IEnumerator StartTeleportCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        Vector3 teleportCoordinate = teleportTarget.transform.position;
        teleportCoordinate.y = player.position.y;

        player.position = teleportCoordinate;

        DestroyTeleportPoint();
    }

    void DestroyTeleportPoint()
    {
        if (HasTarget)
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
