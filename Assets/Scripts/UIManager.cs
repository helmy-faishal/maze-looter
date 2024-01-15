using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text skillText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text objectiveText;
    [SerializeField] GameObject warningObject;
    [SerializeField] float warningDelay = 2f;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
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

    public void SetWarningActive()
    {
        StopAllCoroutines();
        StartCoroutine(NotificationCoroutine(warningObject));
    }

    IEnumerator NotificationCoroutine(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(warningDelay);
        obj.SetActive(false);
    }
}
