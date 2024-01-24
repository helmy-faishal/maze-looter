using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    [Header("Skill Detail Settings")]
    [SerializeField] RectTransform scrollContent;
    [SerializeField] RectTransform skillGroup;
    [SerializeField] GameObject skillDetail;
    [SerializeField] Sprite defaultSkillSprite;
    [SerializeField] Sprite selectedSkillSprite;
    [SerializeField] List<PlayerSkillDetailSO> skillList;


    [Header("Timer")]
    [SerializeField] Image timerFill;
    [SerializeField] float maxTimer;
    float timer;

    SkillType playerSkillType = SkillType.None;
    GameObject skillObject = null;

    private Action OnSkillSelected;

    void Start()
    {
        timer = maxTimer;

        if (skillList == null) return;
        if (skillList.Count <= 0) return;

        SetMaxContentHeight();
        InstantiateSkillDetail();
    }

    private void SetMaxContentHeight()
    {
        VerticalLayoutGroup skillLayout = skillGroup.GetComponent<VerticalLayoutGroup>();
        RectTransform skillDetailRect = skillDetail.GetComponent<RectTransform>();

        float maxHeight = skillList.Count * (skillLayout.spacing + skillDetailRect.rect.height);
        Vector2 maxSize = new Vector2(skillGroup.rect.width, maxHeight);

        skillGroup.sizeDelta = maxSize;

        maxSize.x = 0;
        scrollContent.sizeDelta = maxSize;
    }

    private void InstantiateSkillDetail()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            GameObject obj = Instantiate(skillDetail, skillGroup.transform);

            PlayerSkillDetailSO playerSkillDetail = skillList[i];

            Transform skillName = obj.transform.GetChild(0);
            skillName.GetComponent<TMP_Text>().text = playerSkillDetail.SkillName;

            Transform skillDescription = skillName.transform.GetChild(0);
            skillDescription.GetComponent<TMP_Text>().text = playerSkillDetail.SkillDescription;

            Transform skillIcon = obj.transform.GetChild(1);
            if (playerSkillDetail.SkillIcon != null)
            {
                skillIcon.GetComponent<Image>().overrideSprite = playerSkillDetail.SkillIcon.sprite;
            }

            SkillType skillType = playerSkillDetail.PlayerSkillType;
            GameObject skillPrefab = playerSkillDetail.SkillPrefab;

            AddButtonListener(obj,skillType, skillPrefab);
        }
    }

    void AddButtonListener(GameObject obj, SkillType skillType, GameObject skillObj)
    {
        Button button = obj.GetComponent<Button>();
        Image image = obj.GetComponent<Image>();

        OnSkillSelected += () =>
        {
            ResetButtonImage(image);
        };

        button.onClick.AddListener(() =>
        {
            OnSkillSelected?.Invoke();
            image.sprite = selectedSkillSprite;
            playerSkillType = skillType;
            skillObject = skillObj;
        });
    }

    void ResetButtonImage(Image image)
    {
        image.sprite = defaultSkillSprite;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);
        timerFill.fillAmount = timer / maxTimer;

        if (timer <= 0)
        {
            if (GameSession.Instance == null || SceneSwitching.Instance == null)
            {
                Debug.LogWarning("GameSession or SceneSwitching not found");
                return;
            }

            GameSession.Instance.playerSkillType = playerSkillType;
            GameSession.Instance.skillObject = skillObject;
            SceneSwitching.Instance.PlayGame();
        }
    }

    private void OnDisable()
    {
        OnSkillSelected = null;
    }
}
