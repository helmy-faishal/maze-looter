using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Skill")]
    [SerializeField] TMP_Text skillText;
    [SerializeField] Image skillFill;

    [Header("Health")]
    [SerializeField] TMP_Text healthText;

    [Header("Information")]
    [SerializeField] TMP_Text objectiveText;

    [Header("Warning")]
    [SerializeField] GameObject warningObject;
    [SerializeField] TMP_Text warningText;
    [SerializeField] float warningDelay = 2f;

    [Header("Touch Input UI")]
    [SerializeField] GameObject moveInput;
    [SerializeField] GameObject skillInput;
    [SerializeField] GameObject interactionInput;
    bool isTouchActive = true;

    bool _isFilling = false;
    float _maxFill = 0;
    float fillValue = 0;

    PlayerInputAction actions;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        actions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        actions.Enable();
        actions.UI.HideTouchInput.performed += SetTouchActive;
    }

    private void OnDisable()
    {
        actions.Disable();
        actions.UI.HideTouchInput.performed += SetTouchActive;
    }

    void SetTouchActive(InputAction.CallbackContext context)
    {
        isTouchActive = !isTouchActive;
        moveInput.SetActive(isTouchActive);
        skillInput.SetActive(isTouchActive);
        interactionInput.SetActive(isTouchActive);
    }

    private void Start()
    {
        warningObject.SetActive(false);
        SetObjectiveText("- Picking up Treasure");
    }

    public void SetSkillText(int value)
    {
        skillText.text = $"x{value}";
    }

    public void SetHealthText(int value)
    {
        healthText.text = $"x{value}";
    }

    public void SetObjectiveText(string text)
    {
        objectiveText.text = text;
    }

    public void SetWarningActive(string text="Warning !")
    {
        StopAllCoroutines();
        warningText.text = text;
        StartCoroutine(NotificationCoroutine(warningObject));
    }

    IEnumerator NotificationCoroutine(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(warningDelay);
        obj.SetActive(false);
    }

    private void Update()
    {
        if (_isFilling)
        {
            fillValue += Time.deltaTime;

            skillFill.fillAmount = Mathf.Clamp01(fillValue/_maxFill);

            if (fillValue >= _maxFill)
            {
                _isFilling = false;
            }
        }
    }

    public void StartCooldownSkill(float cooldown)
    {
        _isFilling = true;
        _maxFill = cooldown;
        fillValue = 0;
    }
}
