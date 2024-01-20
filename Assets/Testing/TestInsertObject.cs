using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestInsertObject : MonoBehaviour
{
    [SerializeField] RectTransform scrollContent;
    [SerializeField] RectTransform skillGroup;
    [SerializeField] GameObject skillDetail;
    [SerializeField] int numberOfChild = 5;

    void Start()
    {
        VerticalLayoutGroup skillLayout = skillGroup.GetComponent<VerticalLayoutGroup>();
        RectTransform skillDetailRect = skillDetail.GetComponent<RectTransform>();

        float maxHeight = numberOfChild * (skillLayout.spacing + skillDetailRect.rect.height);
        Vector2 maxSize = new Vector2(skillGroup.rect.width, maxHeight);

        skillGroup.sizeDelta = maxSize;

        maxSize.x = 0;
        scrollContent.sizeDelta = maxSize;

        for (int i = 0; i < numberOfChild; i++)
        {
            GameObject obj = Instantiate(skillDetail, skillGroup.transform);
            Transform skillTitle = obj.transform.GetChild(0);
            skillTitle.GetComponent<TMP_Text>().text = $"Skill - {i}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
