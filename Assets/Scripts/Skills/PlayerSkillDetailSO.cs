using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Skill Detail",fileName = "New Skill Detail")]
public class PlayerSkillDetailSO : ScriptableObject
{
    [SerializeField] Image skillIcon;
    [SerializeField] string skillName;

    [TextAreaAttribute(2,10)]
    [SerializeField] string skillDescription;
    [SerializeField] SkillType skillType = SkillType.None;
    [SerializeField] GameObject skillPrefab;

    public Image SkillIcon { get { return skillIcon; } }
    public string SkillName { get {  return skillName; } }
    public string SkillDescription { get {  return skillDescription; } }

    public SkillType PlayerSkillType { get {  return skillType; } }

    public GameObject SkillPrefab { get {  return skillPrefab; } }
}
